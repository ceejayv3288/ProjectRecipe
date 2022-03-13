using ProjectRecipe.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectRecipe.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeIngredientsCollectionView : ContentView
    {
        public static readonly BindableProperty CollectionItemsSourceProperty =
           BindableProperty.Create(nameof(CollectionItemsSource),
               typeof(ObservableCollection<RecipeIngredientModel>),
               typeof(RecipeIngredientsCollectionView),
               defaultValue: null,
               defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: ItemsSourcePropertySource);

        public static readonly BindableProperty CollectionHeightRequestProperty =
          BindableProperty.Create(nameof(CollectionHeightRequest),
              typeof(int),
              typeof(RecipeIngredientsCollectionView),
              defaultValue: null,
              defaultBindingMode: BindingMode.TwoWay,
              propertyChanged: HeightRequestPropertyHeight);

        private static void ItemsSourcePropertySource(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RecipeIngredientsCollectionView)bindable;
            control.DraggableCollection.ItemsSource = (ObservableCollection<RecipeIngredientModel>)newValue;
        }

        private static void HeightRequestPropertyHeight(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RecipeIngredientsCollectionView)bindable;
            control.DraggableCollection.HeightRequest = (int)newValue;
        }

        public ObservableCollection<RecipeIngredientModel> CollectionItemsSource
        {
            get
            {
                return (ObservableCollection<RecipeIngredientModel>)base.GetValue(CollectionItemsSourceProperty);
            }
            set
            {
                SetValue(CollectionItemsSourceProperty, value);
            }
        }

        public double CollectionHeightRequest
        {
            get
            {
                return (int)base.GetValue(CollectionHeightRequestProperty);
            }
            set
            {
                SetValue(CollectionHeightRequestProperty, value);
            }
        }

        public RecipeIngredientsCollectionView()
        {
            InitializeComponent();
        }
    }
}