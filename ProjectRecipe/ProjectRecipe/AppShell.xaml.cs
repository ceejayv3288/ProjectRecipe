using ProjectRecipe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectRecipe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        private readonly AppShellViewModel _appShellViewModel;
        public AppShell()
        {
            _appShellViewModel = new AppShellViewModel();
            BindingContext = _appShellViewModel;
            InitializeComponent();
        }
    }
}