using Newtonsoft.Json;
using ProjectRecipe.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectRecipe
{
    public partial class App : Application
    {
        public static JsonSerializer JsonSerializer { get; private set; }

        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RecipeCreateUpdatePage),
                typeof(RecipeCreateUpdatePage));

            JsonSerializer = new JsonSerializer();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
