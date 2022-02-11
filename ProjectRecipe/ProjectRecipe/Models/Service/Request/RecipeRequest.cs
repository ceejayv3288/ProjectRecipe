using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models.Service.Request
{
    public class RecipeRequest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public byte[] image { get; set; }
        public int durationInMin { get; set; }
    }
}
