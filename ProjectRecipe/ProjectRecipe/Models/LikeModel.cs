using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models
{
    public class LikeModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public bool IsLiked { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int RecipeId { get; set; }
    }
}
