using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Droid.DependencyService;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyPicker), typeof(ProteusMMX.Droid.DependencyService.CustomPickerRenderer))]
namespace ProteusMMX.Droid.DependencyService
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                this.Control.SetBackgroundResource(Resource.Drawable.RoundedEntry);
            }
        }
    }
}