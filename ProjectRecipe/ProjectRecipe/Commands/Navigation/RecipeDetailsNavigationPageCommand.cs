using ProjectRecipe.Models;
using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands.Navigation
{
    public class RecipeDetailsNavigationPageCommand : ICommand
    {
        object _viewModel = new object();
        public RecipeDetailsNavigationPageCommand(object viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_viewModel is MyOwnRecipesPageViewModel myOwnRecipesPageViewModel)
            {
                if (parameter != null)
                {
                    if (parameter is RecipeModel recipe)
                    {
                        myOwnRecipesPageViewModel.ExecuteRecipeDetailsNavigationPageCommand(recipe.id);
                    }
                }
            }
            else if (_viewModel is PopularRecipesPageViewModel popularRecipesPageViewModel)
            {
                if (parameter != null)
                {
                    if (parameter is RecipeModel recipe)
                    {
                        popularRecipesPageViewModel.ExecuteRecipeDetailsNavigationPageCommand(recipe.id);
                    }
                }
            }
        }
    }
}
