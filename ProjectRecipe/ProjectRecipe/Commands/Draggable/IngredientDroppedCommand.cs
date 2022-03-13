using ProjectRecipe.Models;
using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands.Draggable
{
    public class IngredientDroppedCommand : ICommand
    {
        private readonly object _viewModel;
        public IngredientDroppedCommand(object viewModel)
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
            if (_viewModel is RecipeCreateUpdatePageViewModel recipeCreateUpdatePageViewModel)
            {
                if (parameter is RecipeIngredientModel recipeIngredient)
                {
                    recipeCreateUpdatePageViewModel.ExecuteItemDroppedMoveRecipeIngredientCommand(recipeIngredient);
                }
                else
                {
                    recipeCreateUpdatePageViewModel.ExecuteItemDroppedDeleteRecipeIngredientCommand();
                }
            }
        }
    }
}
