using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using ProteusMMX.DependencyInterface;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSwitch), typeof(ProteusMMX.Droid.DependencyService.CustomSwitchRenderer))]

namespace ProteusMMX.Droid.DependencyService
{
    internal class CustomSwitchRenderer: SwitchRenderer
    {
        public CustomSwitchRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (null != Control)
            {
                
                    Control.TextOn = "Yes";
                    Control.TextOff = "No";
              
            }
        }
    }
}