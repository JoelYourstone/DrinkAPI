using System;
using System.Collections.Generic;

namespace ContactList.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public DateTime ModifiedUtc { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}