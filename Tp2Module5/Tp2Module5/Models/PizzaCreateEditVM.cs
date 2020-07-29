using Newtonsoft.Json;
using System.Collections.Generic;
using TPModule5_2_BO;

namespace Tp2Module5.Models
{
    public class PizzaCreateEditVM
    {
        public Pizza Pizza { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<Pate> Pates { get; set; } = new List<Pate>();
        public int IdSelectedPate { get; set; }
        public List<int> IdSelectedIngredients { get; set; } = new List<int>();
    }
}