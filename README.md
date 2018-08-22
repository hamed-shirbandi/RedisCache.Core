# What is this ?

Simple library to Using [Redis](http://redis.io) Cache In .NET Core Projects

[Article in persian](http://www.codeblock.ir/article/44)

# Install via NuGet

To install RedisCache.Core, run the following command in the Package Manager Console
```code
pm> Install-Package RedisCache.Core
```
You can also view the [package page](https://www.nuget.org/packages/RedisCache.Core) on NuGet.

# How to use ?

First you must install redis server. in quickly way you can install chocolaty and [install redis](https://chocolatey.org/packages/redis-64/) through it. then run following command in cmd (run as administrator);

```code
C:\> redis-server 
```


Then :

1- install package from nuget

2- add required services to Startup class as below :
```code
  services.AddRedisCache(options =>
            {
                options.Configuration ="localhost:6379";
                options.InstanceName ="RedisCacheTestDB" ;
            });
```
3- Use IRedisCacheService in your app :
```code
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
                new ValueModel{Id=1,Prop1="Prop 1",Prop2="Prop 2"},
                new ValueModel{Id=2,Prop1="Prop 3",Prop2="Prop 4"},
                new ValueModel{Id=3,Prop1="Prop 5",Prop2="Prop 6"},
                new ValueModel{Id=4,Prop1="Prop 7",Prop2="Prop 8"},
                new ValueModel{Id=5,Prop1="Prop 9",Prop2="Prop 10"},
            };



        }
    }

```

