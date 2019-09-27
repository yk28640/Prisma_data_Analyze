using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
    public class Wheather
    {
        public long Id { get; set; }
        public string Degreen { get; set; }
        public bool HotCold { get; set; }
    }
}
