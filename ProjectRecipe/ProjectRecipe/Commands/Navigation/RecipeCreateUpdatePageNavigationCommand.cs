using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands.Navigation
{
    public class RecipeCreateUpdatePageNavigationCommand : ICommand
    {
        MyOwnRecipesPageViewModel _viewModel;
        public RecipeCreateUpdatePageNavigationCommand(MyOwnRecipesPageViewModel viewmodel)
        {
            _viewModel = viewmodel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
           return !_viewModel.isBusy;
        }

        public void Execute(object parameter)
        {
            _viewModel.ExecuteRecipeCreateUpdatePageNavigationCommand();
        }
    }
}
