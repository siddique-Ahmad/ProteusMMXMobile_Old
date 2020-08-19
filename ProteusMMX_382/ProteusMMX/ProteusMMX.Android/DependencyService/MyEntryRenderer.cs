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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(ProteusMMX.Droid.DependencyService.MyEntryRenderer))]
namespace ProteusMMX.Droid.DependencyService
{
    class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                this.Control.SetBackgroundResource(Resource.Drawable.RoundedEntry);
            }
        }
    }
}