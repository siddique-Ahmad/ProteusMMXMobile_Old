using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ProteusMMX.Controls;
using ProteusMMX.Droid.DependencyService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderedEntry), typeof(CustomBorderedEntry))]

namespace ProteusMMX.Droid.DependencyService
{
    class CustomBorderedEntry : EntryRenderer
    {
        public CustomBorderedEntry(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            //if (Control != null)
            //{
            //    this.Control.SetBackgroundResource(Resource.Drawable.RoundedEntry);
            //}

            if (e.NewElement != null)
            {
                var view = (BorderedEntry)Element;
                if (view.IsCurvedCornersEnabled)
                {
                    // creating gradient drawable for the curved background  
                    var _gradientBackground = new GradientDrawable();
                    _gradientBackground.SetShape(ShapeType.Rectangle);
                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                    // Thickness of the stroke line  
                    //_gradientBackground.SetStroke(view.BorderWidth, Xamarin.Forms.Color.Red.ToAndroid());
                    _gradientBackground.SetStroke(view.BorderWidth, Android.Graphics.Color.Red);
                    // Radius for the curves  
                    _gradientBackground.SetCornerRadius(
                        DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));

                    // set the background of the   
                    Control.SetBackground(_gradientBackground);
                }
                // Set padding for the internal text from border  
                Control.SetPadding(
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop,
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);
            }
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName != null)
            {
                var view = (BorderedEntry)Element;
                if (e.PropertyName == Entry.TextProperty.PropertyName)
                {
                    if (string.IsNullOrWhiteSpace(Control.Text))  //this is your condition(For example, here is the length of the text content)
                    {

                        if (view.IsCurvedCornersEnabled)
                        {
                            // creating gradient drawable for the curved background  
                            var _gradientBackground = new GradientDrawable();
                            _gradientBackground.SetShape(ShapeType.Rectangle);
                            _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                            // Thickness of the stroke line  
                            _gradientBackground.SetStroke(view.BorderWidth, Android.Graphics.Color.Red);

                            // Radius for the curves  
                            _gradientBackground.SetCornerRadius(
                                DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));

                            // set the background of the   
                            Control.SetBackground(_gradientBackground);
                        }
                        // Set padding for the internal text from border  
                        Control.SetPadding(
                            (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop,
                            (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);

                    }
                    else
                    {
                        if (view.IsCurvedCornersEnabled)
                        {
                            // creating gradient drawable for the curved background  
                            var _gradientBackground = new GradientDrawable();
                            _gradientBackground.SetShape(ShapeType.Rectangle);
                            _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                            // Thickness of the stroke line  
                            //_gradientBackground.SetStroke(view.BorderWidth, Xamarin.Forms.Color.Black.ToAndroid());
                            _gradientBackground.SetStroke(view.BorderWidth, Android.Graphics.Color.Red);
                            // Radius for the curves  
                            _gradientBackground.SetCornerRadius(
                                DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));

                            // set the background of the   
                            Control.SetBackground(_gradientBackground);
                        }
                        // Set padding for the internal text from border  
                        Control.SetPadding(
                            (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingTop,
                            (int)DpToPixels(this.Context, Convert.ToSingle(12)), Control.PaddingBottom);

                    }
                }
            }
        }

    }
}