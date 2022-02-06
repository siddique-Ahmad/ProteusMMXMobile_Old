using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.Content;
using Android.Views;
using Android.Widget;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Droid.DependencyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomValidationPicker), typeof(CustomValidationPickerRenderer))]
namespace ProteusMMX.Droid.DependencyService
{
    class CustomValidationPickerRenderer : PickerRenderer
    {
        CustomValidationPicker element;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (CustomValidationPicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                Control.Background = AddPickerStyles(element.Image, "Black");

        }

        public LayerDrawable AddPickerStyles(string imagePath, string bordercolor)
        {
            ShapeDrawable border = new ShapeDrawable();
            border.Paint.Color = Android.Graphics.Color.ParseColor(bordercolor);
            border.SetPadding(10, 10, 10, 10);
            border.Paint.SetStyle(Paint.Style.Stroke);
            Drawable[] layers = { border, GetDrawable(imagePath) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);
            layerDrawable.SetPadding(5, 0, 70, 0); // Add code here
            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 30, 30, true));
            result.Gravity = Android.Views.GravityFlags.Right;

            return result;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Picker data = sender as Picker;

            if (data.SelectedItem == null)
            {
                Control.Background = AddPickerStyles(element.Image, "Red");

            }
            else
            {
                Control.Background = AddPickerStyles(element.Image, "Black");
            }

        }

    }
}