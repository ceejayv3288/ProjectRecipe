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
    public partial class RecipeCreateUpdatePage : ContentPage
    {
        private readonly RecipeCreateUpdatePageViewModel _recipeCreateUpdatePageViewModel;

        public RecipeCreateUpdatePage()
        {
            _recipeCreateUpdatePageViewModel = new RecipeCreateUpdatePageViewModel();
            BindingContext = _recipeCreateUpdatePageViewModel;
            InitializeComponent();

            //var samp = BindingContext as RecipeCreateUpdatePageViewModel;
        }

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    var viewModel = BindingContext as RecipeCreateUpdatePageViewModel;
        //    viewModel.image = null;
        //}
    }
}