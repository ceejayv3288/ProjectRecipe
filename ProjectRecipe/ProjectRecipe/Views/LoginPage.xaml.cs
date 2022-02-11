using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectRecipe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly LoginPageViewModel loginPageViewModel;
        public LoginPage()
        {
            loginPageViewModel = new LoginPageViewModel();
            BindingContext = loginPageViewModel;
            InitializeComponent();
        }
    }
}