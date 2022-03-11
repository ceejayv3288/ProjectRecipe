using ProjectRecipe.Models;
using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands.Draggable
{
    public class ItemDraggedCommand : ICommand
    {
        private readonly object _viewModel;
        public ItemDraggedCommand(object viewModel)
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
                        myOwnRecipesPageViewModel.ExecuteItemDraggedCommand(recipe);
                    }
                }
            }
            else if (_viewModel is RecipeCreateUpdatePageViewModel recipeCreateUpdatePageViewModel)
            {
                if (parameter != null)
                {
                    if (parameter is RecipeStepModel recipeStep)
                    {
                        recipeCreateUpdatePageViewModel.ExecuteItemDraggedCommand(recipeStep);
                    }
                }
            }
        }
    }
}
