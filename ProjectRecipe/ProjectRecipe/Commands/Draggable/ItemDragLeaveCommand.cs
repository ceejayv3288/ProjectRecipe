using ProjectRecipe.Models;
using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands.Draggable
{
    public class ItemDragLeaveCommand : ICommand
    {
        private readonly object _viewModel;
        public ItemDragLeaveCommand(object viewModel)
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
            
        }
    }
}
