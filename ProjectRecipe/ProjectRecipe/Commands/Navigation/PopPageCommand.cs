using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectRecipe.Commands.Navigation
{
    public class PopPageCommand : ICommand
    {
        object _viewModel;
        public PopPageCommand(object viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
