using ProjectRecipe.Commands;
using ProjectRecipe.Commands.Navigation;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using ProjectRecipe.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class MyOwnRecipesPageViewModel : BaseViewModel
    {
        private readonly IRecipeService recipeService;

        public DragStartingCommand DragStartingCommand { get; set; }
        public DropOverCommand DropOverCommand { get; set; }
        public MyOwnRecipesPageNavigationCommand MyOwnRecipesPageNavigationCommand { get; set; }
        public ObservableCollection<RecipeModel> myRecipes { get; set; }

        public RecipeModel dragRecipe { get; set; }

        public MyOwnRecipesPageViewModel()
        {
            DragStartingCommand = new DragStartingCommand(this);
            DropOverCommand = new DropOverCommand(this);
            MyOwnRecipesPageNavigationCommand = new MyOwnRecipesPageNavigationCommand(this);
            OpenFlyoutMenuCommand = new OpenFlyoutMenuCommand(this);
            myRecipes = new ObservableCollection<RecipeModel>();

            recipeService = DependencyService.Get<IRecipeService>();
            Initialize();
        }

        public async void Initialize()
        {
            var allRecipes = await recipeService.GetAllRecipes();
            //MyRecipes = new ObservableCollection<RecipeModel>(allRecipes);
            foreach (var recipe in allRecipes)
            {
                myRecipes.Add(recipe);
            }
        }

        public async void ExecuteDragOverDeleteCommand()
        {
            if (myRecipes.Contains(dragRecipe))
            {
                myRecipes.Remove(dragRecipe);
                var recipeDeleted = await recipeService.DeleteRecipe(dragRecipe.id);
                if (!recipeDeleted.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Failed!", "An error has occured.", "Ok");
                }
            }
        }

        public async void ExecuteMyOwnRecipesPageNavigationCommand()
        {
            isBusy = true;
            try
            {
                var route = $"{nameof(RecipeCreateUpdatePage)}";
                await Shell.Current.GoToAsync(route);
            }
            finally
            {
                isBusy = false;
            } 
        }
    }
}
