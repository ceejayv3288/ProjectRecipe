using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectRecipe.Controls.Headers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderWithAddIcon : ContentView
    {
        public static readonly BindableProperty TitleTextProperty =
            BindableProperty.Create(nameof(TitleText),
                typeof(string),
                typeof(HeaderWithSearchIcon),
                defaultValue: string.Empty,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: TitleTextPropertyText);

        private static void TitleTextPropertyText(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (HeaderWithAddIcon)bindable;
            control.Title.Text = newValue?.ToString();
        }

        public string TitleText
        {
            get
            {
                return base.GetValue(TitleTextProperty)?.ToString();
            }
            set
            {
                SetValue(TitleTextProperty, value);
            }
        }
        
        public HeaderWithAddIcon()
        {
            InitializeComponent();
        }
    }
}