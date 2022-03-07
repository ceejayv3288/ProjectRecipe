using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models
{
    public class RecipeStepModel : BaseModel
    {
        public int id { get; set; }
        public Guid guid { get; set; }
        public string description { get; set; }
        public byte[] image { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateUpdated { get; set; }

        private int _order;
        public int order
        {
            get { return _order; }
            set
            {
                SetProperty(ref _order, value);
            }
        }
    }
}
