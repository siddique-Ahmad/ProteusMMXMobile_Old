using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProteusMMX.Controls;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Droid.DependencyService;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSwitchColor), typeof(CustomSwitchRenderer))]
namespace ProteusMMX.Droid.DependencyService
{
    [Obsolete]
    public class CustomSwitchRenderer : SwitchRenderer
    {
        private CustomSwitchColor view;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;
            view = (CustomSwitchColor)Element;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
            {
                if (this.Control != null)
                {
                    if (this.Control.Checked)
                    {
                        this.Control.TrackDrawable.SetColorFilter(view.SwitchOnColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                    }
                    else
                    {
                        this.Control.TrackDrawable.SetColorFilter(view.SwitchOffColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                    }
                    this.Control.CheckedChange += this.OnCheckedChange;
                    UpdateSwitchThumbImage(view);
                }
                //Control.TrackDrawable.SetColorFilter(view.SwitchBGColor.ToAndroid(), PorterDuff.Mode.Multiply);  
            }
        }

        private void UpdateSwitchThumbImage(CustomSwitchColor view)
        {
            if (!string.IsNullOrEmpty(view.SwitchThumbImage))
            {
                view.SwitchThumbImage = view.SwitchThumbImage.Replace(".jpg", "").Replace(".png", "");
                int imgid = (int)typeof(Resource.Drawable).GetField(view.SwitchThumbImage).GetValue(null);
                Control.SetThumbResource(Resource.Drawable.icon);
            }
            else
            {
                Control.ThumbDrawable.SetColorFilter(view.SwitchThumbColor.ToAndroid(), PorterDuff.Mode.Multiply);
                // Control.SetTrackResource(Resource.Drawable.track);  
            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (this.Control.Checked)
            {
                this.Control.TrackDrawable.SetColorFilter(view.SwitchOnColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
            }
            else
            {
                this.Control.TrackDrawable.SetColorFilter(view.SwitchOffColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
            }
        }
        protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }
    }
}