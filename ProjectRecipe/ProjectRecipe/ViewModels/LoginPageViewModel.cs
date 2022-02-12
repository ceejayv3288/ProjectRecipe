using ProjectRecipe.Commands;
using ProjectRecipe.Commands.Navigation;
using ProjectRecipe.Constants;
using ProjectRecipe.Models.Service.Request;
using ProjectRecipe.Services.Interfaces;
using ProjectRecipe.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService authorizationService;
        private readonly IMemoryCacheService memoryCacheService;

        public LoginCommand LoginCommand { get; set; }
        public RegistrationPageNavigationCommand RegistrationPageNavigationCommand { get; set; }

        private string _userName;
        public string userName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value);
            }
        }

        private string _password;
        public string password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        public LoginPageViewModel()
        {
            LoginCommand = new LoginCommand(this);
            RegistrationPageNavigationCommand = new RegistrationPageNavigationCommand(this);

            authorizationService = DependencyService.Get<IAuthorizationService>();
            memoryCacheService = DependencyService.Get<IMemoryCacheService>();
        }

        public async void ExecuteLoginCommand()
        {
            AuthenticationRequest authenticationRequest = new AuthenticationRequest
            {
                username = this.userName,
                password = this.password
            };

            App.Current.MainPage.Navigation.ShowPopup(LoadingPopup);
            var result = await authorizationService.LoginUser(authenticationRequest);
            if (result.Item2 == null)
            {
                LoadingPopup.Dismiss(null);
                await Application.Current.MainPage.DisplayAlert("Alert", "Successfully Login.", "Ok");
                memoryCacheService.Set(Configurations.ClientTokenKey, result.Item1.token, new DateTimeOffset(DateTime.Now.AddDays(Configurations.ClientTokenLifetimeByDays)));
                await Shell.Current.GoToAsync($"//{nameof(PopularRecipesPage)}");
            }
            else
            {
                LoadingPopup.Dismiss(null);
                await Application.Current.MainPage.DisplayAlert("Alert", result.Item2.message, "Ok");
            }
        }

        public async void ExecuteRegistrationPageNavigationCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
        }
    }
}
