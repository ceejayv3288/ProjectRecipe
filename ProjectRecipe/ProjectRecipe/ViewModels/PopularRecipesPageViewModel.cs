﻿using ProjectRecipe.Converters;
using ProjectRecipe.Interfaces.Commands;
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
        IRecipeService recipeService;
        private ObservableCollection<RecipeModel> popularRecipesList;
        ByteArrayToImageConverter byteArrayToImageConverter;
        ImageToByteArrayConverter imageToByteArrayConverter;
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
            byteArrayToImageConverter = new ByteArrayToImageConverter();
            imageToByteArrayConverter = new ImageToByteArrayConverter();

            OpenFlyoutMenuCommand = new OpenFlyoutMenuCommand(this);
            PopularRecipesList = new ObservableCollection<RecipeModel>();

            recipeService = DependencyService.Get<IRecipeService>();

            PopulateList();

            RetrivePopularRecipes();
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

            PopularRecipesList.Add(new RecipeModel { Name = "test", Description = "test", Image = convertedImage });
            PopularRecipesList.Add(new RecipeModel { Name = "test2", Description = "test2", Image = convertedImage });

            //PopularRecipesList.Add(new RecipeModel { Name = "test", Description = "test", Image = embeddedImage.Source });
            //PopularRecipesList.Add(new RecipeModel { Name = "test2", Description = "test2", Image = embeddedImage.Source });
        }

        public async void RetrivePopularRecipes()
        {
            var samp = await recipeService.GetAllRecipes();

            await recipeService.UpdateRecipe(samp[0]);
        }
    }
}
