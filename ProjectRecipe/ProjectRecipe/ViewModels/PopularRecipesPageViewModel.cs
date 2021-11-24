using ProjectRecipe.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.ViewModels
{
    public class PopularRecipesPageViewModel : BaseViewModel
    {
        public PopularRecipesPageViewModel()
        {
            OpenFlyoutMenuCommand = new OpenFlyoutMenuCommand(this);
        }
    }
}
