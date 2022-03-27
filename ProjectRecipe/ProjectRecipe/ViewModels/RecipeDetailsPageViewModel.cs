using ProjectRecipe.Commands;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class RecipeDetailsPageViewModel : BaseViewModel
    {
        private readonly IRecipeService recipeService;
        private readonly IRecipeIngredientService recipeIngredientService;
        private readonly IRecipeStepService recipeStepService;
        private readonly ILikeService likeService;

        public LikeRecipeCommand LikeRecipeCommand { get; set; }

        private RecipeModel _recipe;
        public RecipeModel recipe
        {
            get { return _recipe; }
            set
            {
                SetProperty(ref _recipe, value);
            }
        }

        private ObservableCollection<RecipeIngredientModel> _recipeIngredients;
        public ObservableCollection<RecipeIngredientModel> recipeIngredients
        {
            get { return _recipeIngredients; }
            set
            {
                SetProperty(ref _recipeIngredients, value);
            }
        }

        private ObservableCollection<RecipeStepModel> _recipeSteps;
        public ObservableCollection<RecipeStepModel> recipeSteps
        {
            get { return _recipeSteps; }
            set
            {
                SetProperty(ref _recipeSteps, value);
            }
        }

        public RecipeDetailsPageViewModel()
        {
            LikeRecipeCommand = new LikeRecipeCommand(this);

            recipeService = DependencyService.Get<IRecipeService>();
            recipeIngredientService = DependencyService.Get<IRecipeIngredientService>();
            recipeStepService = DependencyService.Get<IRecipeStepService>();
            likeService = DependencyService.Get<ILikeService>();
        }

        public async void InitializeRecipe(int recipeId)
        {
            recipe = await recipeService.GetRecipe(recipeId);
            recipeIngredients = new ObservableCollection<RecipeIngredientModel>(await recipeIngredientService.GetRecipeIngredientsByRecipeId(recipeId));
            recipeSteps = new ObservableCollection<RecipeStepModel>(await recipeStepService.GetRecipeStepsByRecipeId(recipeId));

            var isLikedResult = await likeService.GetLikesByRecipeAndUserId(recipeId, App.UserId);
            recipe.isLiked = isLikedResult.IsLiked;
        }

        public async void ExecuteLikeRecipeCommand(int recipeId)
        {
            var likeResult = await likeService.LikeUnlikeRecipe(recipeId, App.UserId);
            recipe.isLiked = likeResult.IsLiked;
        }

    }
}
