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
    [QueryProperty(nameof(RecipeId), "recipeId")]
    public partial class RecipeDetailsPage : ContentPage
    {
        RecipeDetailsPageViewModel recipeDetailsPageViewModel;
        public string RecipeId { get; set; }

        public RecipeDetailsPage()
        {
            recipeDetailsPageViewModel = new RecipeDetailsPageViewModel();
            BindingContext = recipeDetailsPageViewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(RecipeId, out var recipeId);
            recipeDetailsPageViewModel.InitializeRecipe(recipeId);
        }
    }
}