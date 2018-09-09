using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                _redisCacheService.Set(key: ValuesCacheKeyTemplate.AllValuesCacheKey, data: values, cacheTimeInMinutes: 60);
            }

            return values;
        }



        // GET api/values/1
        [HttpGet("{id}")]
        public ValueModel Get(int id)
        {
            var cacheKey = string.Format(ValuesCacheKeyTemplate.ValueByIdCacheKey, id);
            return _redisCacheService.GetOrSet(key: cacheKey, factory:()=> _values.FirstOrDefault(v => v.Id == id), cacheTimeInMinutes: 60);

        }




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ValueModel> GetSampleValues()
        {
            return new List<ValueModel>
            {
                new ValueModel{Id=1,Prop1="Prop 1",Prop2="Prop 2",Array= new string[] {"arr1","arr2"} },
                new ValueModel{Id=2,Prop1="Prop 3",Prop2="Prop 4",Array= new string[] {"arr1","arr2"}},
                new ValueModel{Id=3,Prop1="Prop 5",Prop2="Prop 6",Array= new string[] {"arr1","arr2"}},
                new ValueModel{Id=4,Prop1="Prop 7",Prop2="Prop 8",Array= new string[] {"arr1","arr2"}},
                new ValueModel{Id=5,Prop1="Prop 9",Prop2="Prop 10",Array= new string[] {"arr1","arr2"}},
            };



        }
    }


}
