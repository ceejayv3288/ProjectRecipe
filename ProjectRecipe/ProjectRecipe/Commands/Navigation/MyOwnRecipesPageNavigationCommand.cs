using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands.Navigation
{
    public class MyOwnRecipesPageNavigationCommand : ICommand
    {
        MyOwnRecipesPageViewModel _viewModel;
        public MyOwnRecipesPageNavigationCommand(MyOwnRecipesPageViewModel viewmodel)
        {
            _viewModel = viewmodel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
           return !_viewModel.IsBusy;
        }

        public void Execute(object parameter)
        {
            _viewModel.ExecuteMyOwnRecipesPageNavigationCommand();
        }
    }
}
