# What is this ?

Simple library to Using [Redis](http://redis.io) Cache In .NET Core Projects

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
```

