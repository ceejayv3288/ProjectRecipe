using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models
{
    public class RecipeCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int DurationInMin { get; set; }
    }
}
