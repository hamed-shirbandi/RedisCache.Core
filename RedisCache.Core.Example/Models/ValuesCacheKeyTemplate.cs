using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.Core.Example.Models
{
    public static class ValuesCacheKeyTemplate
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : Value Id
        /// </remarks>
        public static string ValueByIdCacheKey => "Values.Id-{0}";



        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static string AllValuesCacheKey => "Values";

    }
}
