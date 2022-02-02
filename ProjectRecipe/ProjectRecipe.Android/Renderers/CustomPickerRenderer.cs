using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProjectRecipe.Droid.Renderers;
using ProjectRecipe.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace ProjectRecipe.Droid.Renderers
{
    public  class CustomPickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                Control.TextAlignment = Android.Views.TextAlignment.TextEnd;
            }
        }
    }
}