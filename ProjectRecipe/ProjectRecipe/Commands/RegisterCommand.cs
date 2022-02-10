using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands
{
    public class RegisterCommand : ICommand
    {
        RegistrationPageViewModel _registrationPageViewModel;
        public RegisterCommand(RegistrationPageViewModel registrationPageViewModel)
        {
            _registrationPageViewModel = registrationPageViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _registrationPageViewModel.ExecuteRegisterCommand();
        }
    }
}
