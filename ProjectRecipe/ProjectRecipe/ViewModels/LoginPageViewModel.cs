using ProjectRecipe.Commands;
using ProjectRecipe.Commands.Navigation;
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
        public RegistrationPageNavigationCommand RegistrationPageNavigationCommand { get; set; }
        public LoginPageViewModel()
        {
            LoginCommand = new LoginCommand(this);
            RegistrationPageNavigationCommand = new RegistrationPageNavigationCommand(this);
        }

        public async void ExecuteLoginCommand()
        {
            await Shell.Current.GoToAsync($"//{nameof(PopularRecipesPage)}");
        }

        public async void ExecuteRegistrationPageNavigationCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
        }
    }
}
