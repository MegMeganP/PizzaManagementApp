using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApplication.Models
{
    public class PizzaTopping
    {
        public long Id { get; set; }
        public Pizza Pizza { get; set; }
        public long PizzaId { get; set; }
        public Topping Topping { get; set; }
        public long ToppingId { get; set; }

    }
}
