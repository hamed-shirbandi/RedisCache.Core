using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RedisCache.Core.Example.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IRedisCacheService _redisCacheService;

        public ValuesController(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }


        // GET api/values/5
        [HttpGet()]
        public ValueModel Get( )
        {
            ValueModel value = default(ValueModel);
            if (!_redisCacheService.TryGetValue(key: "MyKey", result: out value))
            {
                value = new ValueModel {Prop1= "Prop 1", Prop2= "Prop 2" };//get data from db instead
                _redisCacheService.Set(key: "MyKey", data: value,cacheTimeInMinutes:60);
            }

            return value;
        }

    }

    public class ValueModel
    {
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
    }
}
