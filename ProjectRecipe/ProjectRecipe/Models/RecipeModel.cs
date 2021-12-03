using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.Models
{
    public class RecipeModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource Image { get; set; }
        public DateTime Duration { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
    }
}
