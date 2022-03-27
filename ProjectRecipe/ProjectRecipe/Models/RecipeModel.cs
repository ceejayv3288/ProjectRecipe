using ProjectRecipe.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.Models
{
    public class RecipeModel : BaseModel
    {
        public int id { get; set; }

        private string _name;
        public string name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private ImageSource _image;
        public ImageSource image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        public CourseTypeEnum courseType { get; set; }

        private int _durationInMin;
        public int durationInMin
        {
            get { return _durationInMin; }
            set { SetProperty(ref _durationInMin, value); }
        }

        private int _likesCount;
        public int likesCount
        {
            get { return _likesCount; }
            set { SetProperty(ref _likesCount, value); }
        }

        private int _commentsCount;
        public int commentsCount
        {
            get { return _commentsCount; }
            set { SetProperty(ref _commentsCount, value); }
        }

        private bool _isLiked;
        public bool isLiked
        {
            get { return _isLiked; }
            set { SetProperty(ref _isLiked, value); }
        }

        private bool _isBeingDragged;
        public bool isBeingDragged
        {
            get { return _isBeingDragged; }
            set { SetProperty(ref _isBeingDragged, value); }
        }
    }
}
