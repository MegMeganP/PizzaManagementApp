using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApplication.Models
{
    public class Topping
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Toppings")]
        public string ToppingName { get; set; }
        public List<PizzaTopping> PizzaTopping { get; set; }

    }
}
