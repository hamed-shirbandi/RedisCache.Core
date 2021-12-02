
using Microsoft.AspNetCore.Mvc;
using RedisCache.Core.Example.Models;

namespace RedisCache.Core.Example.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly IEnumerable<ValueModel> _values;

        public ValuesController(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
            _values = GetSampleValues();
        }


        // GET api/values
        [HttpGet()]
        public IEnumerable<ValueModel> Get()
        {
            if (!_redisCacheService.TryGetValue(key: ValuesCacheKeyTemplate.AllValuesCacheKey, result: out IEnumerable<ValueModel> values))
            {
                values = _values;//get data from db instead
                _redisCacheService.Set(key: ValuesCacheKeyTemplate.AllValuesCacheKey, data: values, cacheTimeInMinutes: 1);
            }

            return values;
        }




        // GET api/values/1
        [HttpGet("{id}")]
        public ValueModel Get(int id)
        {
            var cacheKey = string.Format(ValuesCacheKeyTemplate.ValueByIdCacheKey, id);
            return _redisCacheService.GetOrSet(key: cacheKey, factory: () => _values.FirstOrDefault(v => v.Id == id), cacheTimeInMinutes: 60);

        }





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ValueModel> GetSampleValues()
        {
            return new List<ValueModel>
            {
                new ValueModel
                {
                    Id=1,
                    Meesage = "Data is cached for 1 minute. after 1 minitue you can get new RandomCode",
                    RandomCode=Guid.NewGuid().ToString(),
                    CreateTime= DateTime.Now.ToShortTimeString()
                }
            };
        }
    }


}
