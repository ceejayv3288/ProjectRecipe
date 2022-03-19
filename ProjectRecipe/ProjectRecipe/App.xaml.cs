using Newtonsoft.Json;
using ProjectRecipe.Constants;
using ProjectRecipe.Views;
using System;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectRecipe
{
    public partial class App : Application
    {
        public static JsonSerializer JsonSerializer { get; private set; }
        public static HttpClientHandler HttpClientHandler { get; private set; }
        public static string ClientToken { get; private set; }
        public static string UserId { get; private set; }

        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RecipeCreateUpdatePage),
                typeof(RecipeCreateUpdatePage));
            Routing.RegisterRoute(nameof(RegistrationPage),
                typeof(RegistrationPage));

            JsonSerializer = new JsonSerializer();

        #if DEBUG
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    HttpClientHandler = new HttpClientHandler();
                    HttpClientHandler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => { return true; };
                    break;
            }
        #endif

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            var token = await SecureStorage.GetAsync(Configurations.ClientTokenKey);
            var userId = await SecureStorage.GetAsync(Configurations.UserIdKey);
            ClientToken = token;
            UserId = userId;

            if (token != null)
            {
                await Shell.Current.GoToAsync($"//{nameof(PopularRecipesPage)}");
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
