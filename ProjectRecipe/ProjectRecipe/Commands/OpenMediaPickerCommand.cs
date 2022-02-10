using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands
{
    public class OpenMediaPickerCommand : ICommand
    {
        private readonly object _viewModel;
        public OpenMediaPickerCommand(object viewModel)
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
            if (_viewModel is RecipeCreateUpdatePageViewModel recipeCreateUpdatePageViewModel)
            {
                await recipeCreateUpdatePageViewModel.ExecuteOpenMediaPickerCommand();
            }
            else if (_viewModel is RegistrationPageViewModel registrationPageViewModel)
            {
                await registrationPageViewModel.ExecuteOpenMediaPickerCommand();
            }
        }
    }
}
