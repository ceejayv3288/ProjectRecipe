using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models
{
    public class RecipeCreateUpdateModel
    {
        public string name { get; set; }

        public string description { get; set; }

        public byte[] image { get; set; }

        public int durationInMin { get; set; }

        public int courseType { get; set; }
    }
}
