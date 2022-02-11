using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.Models
{
    public class RecipeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ImageSource image { get; set; }
        public int durationInMin { get; set; }
        public int likesCount { get; set; }
        public int commentsCount { get; set; }
    }
}
