using ProjectRecipe.Models;
using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ProjectRecipe.Commands.Draggable
{
    public class ItemDraggedCommand : ICommand
    {
        private readonly object _viewModel;
        public ItemDraggedCommand(object viewModel)
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
