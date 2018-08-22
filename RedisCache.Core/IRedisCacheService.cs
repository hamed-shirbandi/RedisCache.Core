using System;
using System.Threading.Tasks;

namespace RedisCache.Core
{

    /// <summary>
    /// IRedisCacheService Encapsulates IDistributedCache functionality.
    /// </summary>
    public interface IRedisCacheService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTimeInMinutes"></param>
        void Set(string key, object data, int cacheTimeInMinutes);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTimeInMinutes"></param>
        /// <returns></returns>
        Task SetAsync(string key, object data, int cacheTimeInMinutes);



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="factory"></param>
        /// <param name="cacheTimeInMinutes"></param>
        /// <returns></returns>
        T GetOrSet<T>(string key, Func<T> factory, int cacheTimeInMinutes);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool TryGetValue<T>(string key, out T result);

    }
}
