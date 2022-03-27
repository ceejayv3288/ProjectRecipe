using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands
{
    public class LikeRecipeCommand : ICommand
    {
        object _viewModel = new object();
        public LikeRecipeCommand(object viewModel)
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
            if (_viewModel is PopularRecipesPageViewModel popularRecipesPageViewModel)
            {
                if (parameter != null)
                {
                    if (parameter is int recipeId)
                    {
                        popularRecipesPageViewModel.ExecuteLikeRecipeCommand(recipeId);
                    }
                }
            }
            else if (_viewModel is RecipeDetailsPageViewModel recipeDetailsPageViewModel)
            {
                if (parameter != null)
                {
                    if (parameter is int recipeId)
                    {
                        recipeDetailsPageViewModel.ExecuteLikeRecipeCommand(recipeId);
                    }
                }
            }
        }
    }
}
