﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIPHYPop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public ICollection<Gif> Gifs { get; set; }
    }
}