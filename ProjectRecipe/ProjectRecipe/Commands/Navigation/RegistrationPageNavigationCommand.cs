using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands.Navigation
{
    public class RegistrationPageNavigationCommand : ICommand
    {
        LoginPageViewModel _viewModel;
        public RegistrationPageNavigationCommand(LoginPageViewModel viewmodel)
        {
            _viewModel = viewmodel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.ExecuteRegistrationPageNavigationCommand();
        }
    }
}
