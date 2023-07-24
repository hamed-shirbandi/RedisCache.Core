using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace RedisCache.Core
{

    /// <summary>
    /// Encapsulates IDistributedCache functionality.
    /// </summary>
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _redisCache;


        /// <summary>
        /// 
        /// </summary>
        public RedisCacheService(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }




        /// <summary>
        /// 
        /// </summary>
        public T Get<T>(string key)
        {
            var json = _redisCache.GetString(key);
            if (json == null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }





        /// <summary>
        /// 
        /// </summary>
        public async Task<T> GetAsync<T>(string key)
        {
            var json = await _redisCache.GetStringAsync(key);
            if (json == null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }




        /// <summary>
        /// 
        /// </summary>
        public void Remove(string key)
        {
            _redisCache.Remove(key);
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task RemoveAsync(string key)
        {
            await _redisCache.RemoveAsync(key);
        }




        /// <summary>
        /// 
        /// </summary>
        public void Set(string key, object data, int cacheTimeInMinutes)
        {
            var json = JsonConvert.SerializeObject(data);
            //set cache time
            var expiresIn = TimeSpan.FromMinutes(cacheTimeInMinutes);

            //set options
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(expiresIn);

            _redisCache.SetString(key, json, options);

        }



        /// <summary>
        /// 
        /// </summary>
        public async Task SetAsync(string key, object data, int cacheTimeInMinutes)
        {
            var json = JsonConvert.SerializeObject(data);
            //set cache time
            var expiresIn = TimeSpan.FromMinutes(cacheTimeInMinutes);

            //set options
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(expiresIn);

            await _redisCache.SetStringAsync(key, json, options);
        }




        /// <summary>
        /// 
        /// </summary>
        public T GetOrSet<T>(string key, Func<T> factory, int cacheTimeInMinutes)
        {
            if (TryGetValue<T>(key, out var result))
            {
                return result;
            }

            lock (TypeLock<T>.Lock)
            {
                if (TryGetValue(key, out result))
                {
                    return result;
                }

                result = factory();
                Set(key, result, cacheTimeInMinutes);

                return result;
            }
        }





        /// <summary>
        /// 
        /// </summary>
        public bool TryGetValue<T>(string key, out T result)
        {
            var json = _redisCache.GetString(key);
            if (json == null)
            {
                result = default(T);
                return false;
            }

            result = JsonConvert.DeserializeObject<T>(json);
            return true;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static class TypeLock<T>
        {
            public static object Lock { get; } = new object();
        }
    }
}
