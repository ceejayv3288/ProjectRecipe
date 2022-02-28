using ProjectRecipe.Commands;
using ProjectRecipe.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class AppShellViewModel
    {
        public LogoutCommand LogoutCommand { get; set; }

        public AppShellViewModel()
        {
            LogoutCommand = new LogoutCommand(this);
        }

        public async void ExecuteLogoutCommand()
        {
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
