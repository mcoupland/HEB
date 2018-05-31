using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIPHYPop.Models
{
    public class Gif
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}