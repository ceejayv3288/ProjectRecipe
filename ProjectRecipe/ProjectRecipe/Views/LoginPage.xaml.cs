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
        private readonly LoginPageViewModel _loginPageViewModel;
        public LoginPage()
        {
            _loginPageViewModel = new LoginPageViewModel();
            BindingContext = _loginPageViewModel;
            InitializeComponent();
        }
    }
}