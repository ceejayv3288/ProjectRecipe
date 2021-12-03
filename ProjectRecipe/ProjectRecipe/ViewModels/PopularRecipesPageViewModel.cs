using ProjectRecipe.Converters;
using ProjectRecipe.Interfaces.Commands;
using ProjectRecipe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class PopularRecipesPageViewModel : BaseViewModel
    {
        private double scale;
        public double Scale { get => scale; set => SetProperty(ref scale, value); }

        private ObservableCollection<RecipeModel> popularRecipesList;
        ByteArrayToImageConverter arrayToImageConverter;
        public ObservableCollection<RecipeModel> PopularRecipesList
        {
            get { return popularRecipesList; }
            set
            {
                popularRecipesList = value;
                OnPropertyChanged(nameof(PopularRecipesList));
            }
        }

        public PopularRecipesPageViewModel()
        {
            OpenFlyoutMenuCommand = new OpenFlyoutMenuCommand(this);
            PopularRecipesList = new ObservableCollection<RecipeModel>();
            PopulateList();
        }

        public void PopulateList()
        {

            arrayToImageConverter = new ByteArrayToImageConverter();
            byte[] fileByte = new byte[] {};
            ImageSource convertedImage = arrayToImageConverter.Convert(fileByte, null, null, null) as ImageSource;

            PopularRecipesList.Add(new RecipeModel { Name = "test", Description = "test", Image = convertedImage });
            PopularRecipesList.Add(new RecipeModel { Name = "test2", Description = "test2", Image = convertedImage });
        }
    }
}
