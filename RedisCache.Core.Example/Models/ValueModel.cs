using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.Core.Example.Models
{
    public class ValueModel
    {
        public int Id { get; set; }
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        public string[] Array { get; set; }
    }
}
