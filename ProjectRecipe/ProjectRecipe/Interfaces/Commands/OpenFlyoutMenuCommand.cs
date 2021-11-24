using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Interfaces.Commands
{
    public class OpenFlyoutMenuCommand : ICommand
    {
        object _viewModel = new object();
        public OpenFlyoutMenuCommand(object viewModel)
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
            var isPresented = AppShell.Current.FlyoutIsPresented;
            if (isPresented)
                AppShell.Current.FlyoutIsPresented = false;
            else
                AppShell.Current.FlyoutIsPresented = true;
        }
    }
}
