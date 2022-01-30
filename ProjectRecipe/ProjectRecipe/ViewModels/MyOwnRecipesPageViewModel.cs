using ProjectRecipe.Commands;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class MyOwnRecipesPageViewModel
    {
        private readonly IRecipeService recipeService;

        public DragStartingCommand DragStartingCommand { get; set; }
        public DropOverCommand DropOverCommand { get; set; }
        public ObservableCollection<RecipeModel> MyRecipes { get; set; }

        public RecipeModel dragRecipe { get; set; }

        public MyOwnRecipesPageViewModel()
        {
            DragStartingCommand = new DragStartingCommand(this);
            DropOverCommand = new DropOverCommand(this);
            MyRecipes = new ObservableCollection<RecipeModel>();

            recipeService = DependencyService.Get<IRecipeService>();
            Initialize();
        }

        public async void Initialize()
        {
            var allRecipes = await recipeService.GetAllRecipes();
            //MyRecipes = new ObservableCollection<RecipeModel>(allRecipes);
            foreach (var recipe in allRecipes)
            {
                MyRecipes.Add(recipe);
            }
        }

         public async void ExecuteDragOverDeleteCommand()
        {
            if (MyRecipes.Contains(dragRecipe))
            {
                MyRecipes.Remove(dragRecipe);
                var recipeDeleted = await recipeService.DeleteRecipe(dragRecipe.Id);
                if (!recipeDeleted.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Failed!", "An error has occured.", "Ok");
                }
            }
        }
    }
}
