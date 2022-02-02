using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands
{
    public class LoginCommand : ICommand
    {
        LoginPageViewModel _loginPageViewModel;
        public LoginCommand(LoginPageViewModel loginPageViewModel)
        {
            _loginPageViewModel = loginPageViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _loginPageViewModel.ExecuteLoginCommand();
        }
    }
}
