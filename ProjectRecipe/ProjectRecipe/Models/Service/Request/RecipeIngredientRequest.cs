using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models.Service.Request
{
    public class RecipeIngredientRequest
    {
        public int id { get; set; }

        public Guid guid { get; set; }

        public string quantity { get; set; }

        public string description { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateUpdated { get; set; }
    }
}
