using ProjectRecipe.Models;
using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands
{
    public class DragStartingCommand : ICommand
    {
        private readonly object _viewModel;
        public DragStartingCommand(object viewModel)
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
                        myOwnRecipesPageViewModel.dragRecipe = recipe;
                    }
                }
            }
            else if (_viewModel is RecipeCreateUpdatePageViewModel recipeCreateUpdatePageViewModel)
            {
                if (parameter != null)
                {
                    if (parameter is RecipeStepModel recipeStep)
                    {
                        recipeCreateUpdatePageViewModel.dragStep = recipeStep;
                    }
                }
            }
        }
    }
}
