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
    public partial class PopularRecipesPage : ContentPage
    {
        private readonly PopularRecipesPageViewModel _popularRecipesPageViewModel;
        public PopularRecipesPage()
        {
            _popularRecipesPageViewModel = new PopularRecipesPageViewModel();
            BindingContext = _popularRecipesPageViewModel;
            InitializeComponent();
        }
    }
}