using ProjectRecipe.Interfaces.Commands;
using ProjectRecipe.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class LoginPageViewModel
    {
        public LoginCommand LoginCommand { get; set; }
        public LoginPageViewModel()
        {
            LoginCommand = new LoginCommand(this);
        }

        public void ExecuteLoginCommand()
        {
            App.Current.MainPage = new AppShell();
        }
    }
}
