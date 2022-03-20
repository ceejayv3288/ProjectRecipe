using ProjectRecipe.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models.Service.Response
{
    public class RecipeResponse
    {
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public byte[] image { get; set; }

        public CourseTypeEnum courseType { get; set; }

        public int durationInMin { get; set; }

        public int likesCount { get; set; }

        public int commentsCount { get; set; }
    }
}
