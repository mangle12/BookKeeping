using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookKeeping.Models.DB
{
    public class Categorys
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public int Sort { get; set; }
        public string Color { get; set; }
    }
}
