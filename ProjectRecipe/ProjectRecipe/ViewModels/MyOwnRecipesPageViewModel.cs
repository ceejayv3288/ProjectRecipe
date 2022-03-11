using ProjectRecipe.Commands;
using ProjectRecipe.Commands.Draggable;
using ProjectRecipe.Commands.Navigation;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using ProjectRecipe.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class MyOwnRecipesPageViewModel : BaseViewModel
    {
        private readonly IRecipeService recipeService;

        public ItemDraggedCommand ItemDraggedCommand { get; set; }
        public ItemDroppedCommand ItemDroppedCommand { get; set; }
        public MyOwnRecipesPageNavigationCommand MyOwnRecipesPageNavigationCommand { get; set; }
        public ObservableCollection<RecipeModel> myRecipes { get; set; }

        public MyOwnRecipesPageViewModel()
        {
            ItemDraggedCommand= new ItemDraggedCommand(this);
            ItemDroppedCommand = new ItemDroppedCommand(this);
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

        public void ExecuteItemDraggedCommand(RecipeModel recipe)
        {
            myRecipes.ToList().ForEach(i => i.isBeingDragged = recipe == i);
        }

        public async void ExecuteItemDroppedDeleteRecipeCommand()
        {
            var itemToDelete = myRecipes.First(i => i.isBeingDragged);
            myRecipes.Remove(itemToDelete);
            var recipeDeleted = await recipeService.DeleteRecipe(itemToDelete.id);
            if (!recipeDeleted.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Failed!", "An error has occured.", "Ok");
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
