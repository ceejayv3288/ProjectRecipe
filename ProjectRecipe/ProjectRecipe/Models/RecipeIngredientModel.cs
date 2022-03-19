using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models
{
    public class RecipeIngredientModel : BaseModel
    {
        public int id { get; set; }

        public Guid guid { get; set; }

        public string quantity { get; set; }

        public string description { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateUpdated { get; set; }

        public int RecipeId { get; set; }

        private bool _isBeingDragged;
        public bool isBeingDragged
        {
            get { return _isBeingDragged; }
            set { SetProperty(ref _isBeingDragged, value); }
        }

        private bool _isBeingDraggedOver;
        public bool isBeingDraggedOver
        {
            get { return _isBeingDraggedOver; }
            set { SetProperty(ref _isBeingDraggedOver, value); }
        }
    }
}
