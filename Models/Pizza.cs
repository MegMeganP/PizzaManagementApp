using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApplication.Models
{
    public class Pizza
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Pizza Name")]
        public string Name { get; set; }
        public List<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();

        [NotMapped]
        public List<SelectedTopping> SelectedToppings { get; set; } = new List<SelectedTopping>();
    }

    public class SelectedTopping
    {
        public long Id { get; set; }
        public string ToppingName { get; set; }
        public bool Selected { get; set; }
    }
}
