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
using ProteusMMX.Droid;

[assembly: ExportRenderer(typeof(LoginSwitch), typeof(ProteusMMX.Droid.DependencyService.LoginSwitchRenderer))]
namespace ProteusMMX.Droid.DependencyService
{
    class LoginSwitchRenderer : SwitchRenderer
    {
        public LoginSwitchRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (null != Control)
            {
                Control.TextOn = "Remember Me";
                Control.TextOff = "Remember Me";
                Android.Graphics.Color textColor = Android.Graphics.Color.Black;

            }
        }
    }
}