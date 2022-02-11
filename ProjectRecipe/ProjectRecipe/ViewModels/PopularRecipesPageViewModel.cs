using ProjectRecipe.Commands;
using ProjectRecipe.Converters;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class PopularRecipesPageViewModel : BaseViewModel
    {
        private double scale;
        public double Scale { get => scale; set => SetProperty(ref scale, value); }
        private readonly IRecipeService recipeService;
        private ObservableCollection<RecipeModel> _popularRecipesList;
        ByteArrayToImageConverter byteArrayToImageConverter;
        ImageToByteArrayConverter imageToByteArrayConverter;
        public ObservableCollection<RecipeModel> popularRecipesList
        {
            get { return _popularRecipesList; }
            set
            {
                _popularRecipesList = value;
                OnPropertyChanged(nameof(popularRecipesList));
            }
        }

        public PopularRecipesPageViewModel()
        {
            byteArrayToImageConverter = new ByteArrayToImageConverter();
            imageToByteArrayConverter = new ImageToByteArrayConverter();

            OpenFlyoutMenuCommand = new OpenFlyoutMenuCommand(this);
            popularRecipesList = new ObservableCollection<RecipeModel>();

            recipeService = DependencyService.Get<IRecipeService>();

            PopulateList();
        }

        public void PopulateList()
        {
            Image embeddedImage = new Image
            {
                Source = ImageSource.FromResource("ProjectRecipe.Assets.Images.recipe-placeholder.png", typeof(ImageResourceExtension).GetTypeInfo().Assembly)
            };
            var sampleByte = imageToByteArrayConverter.Convert(embeddedImage.Source, null, null, null);

            byte[] fileByte = new byte[] { };
            //ImageSource convertedImage = byteArrayToImageConverter.Convert(fileByte, null, null, null) as ImageSource;

            ImageSource convertedImage = byteArrayToImageConverter.Convert(sampleByte, null, null, null) as ImageSource;

            popularRecipesList.Add(new RecipeModel { name = "test", description = "test", image = convertedImage });
            popularRecipesList.Add(new RecipeModel { name = "test2", description = "test2", image = convertedImage });

            //PopularRecipesList.Add(new RecipeModel { Name = "test", Description = "test", Image = embeddedImage.Source });
            //PopularRecipesList.Add(new RecipeModel { Name = "test2", Description = "test2", Image = embeddedImage.Source });
        }


    }
}
