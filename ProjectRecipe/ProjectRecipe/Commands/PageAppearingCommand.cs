using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands
{
    public class PageAppearingCommand : ICommand
    {
        object _viewModel = new object();
        public PageAppearingCommand(object viewModel)
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
            if (_viewModel is MyOwnRecipesPageViewModel myOwnRecipesPageViewModel)
            {
                myOwnRecipesPageViewModel.ExecutePageAppearingCommand();
            }
            else if (_viewModel is PopularRecipesPageViewModel popularRecipesPageViewModel)
            {
                popularRecipesPageViewModel.ExecutePageAppearingCommand();
            }
        }
    }
}
