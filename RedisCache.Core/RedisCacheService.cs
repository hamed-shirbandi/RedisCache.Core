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
        private readonly IDistributedCache _redisCashe;


        /// <summary>
        /// 
        /// </summary>
        public RedisCacheService(IDistributedCache redisCashe)
        {
            _redisCashe = redisCashe;
        }




        /// <summary>
        /// 
        /// </summary>
        public T Get<T>(string key)
        {
            var json = _redisCashe.GetString(key);
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
            var json = await _redisCashe.GetStringAsync(key);
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
            _redisCashe.Remove(key);
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task RemoveAsync(string key)
        {
            await _redisCashe.RemoveAsync(key);
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

            _redisCashe.SetString(key, json, options);

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

            await _redisCashe.SetStringAsync(key, json, options);
        }




        /// <summary>
        /// 
        /// </summary>
        public bool TryGetValue<T>(string key, out T result)
        {
            var json = _redisCashe.GetString(key);
            if (json == null)
            {
                result = default(T);
                return false;
            }

            result = JsonConvert.DeserializeObject<T>(json);
            return true;
        }
    }
}
