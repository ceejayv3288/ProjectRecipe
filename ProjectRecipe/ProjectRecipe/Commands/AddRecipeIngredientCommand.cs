﻿using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands
{
    public class AddRecipeIngredientCommand : ICommand
    {

        private readonly object _viewModel;
        public AddRecipeIngredientCommand(object viewModel)
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
                recipeCreateUpdatePageViewModel.ExecuteAddRecipeIngredientCommand();
            }
        }
    }
}
