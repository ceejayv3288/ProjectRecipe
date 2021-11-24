using ProjectRecipe.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.ViewModels
{
    public abstract class BaseViewModel
    {
        public OpenFlyoutMenuCommand OpenFlyoutMenuCommand { get; set; }
    }
}
