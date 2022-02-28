using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands
{
    public class LogoutCommand : ICommand
    {
        AppShellViewModel _appShellViewModel;
        public LogoutCommand(AppShellViewModel appShellViewModel)
        {
            _appShellViewModel = appShellViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _appShellViewModel.ExecuteLogoutCommand();
        }
    }
}
