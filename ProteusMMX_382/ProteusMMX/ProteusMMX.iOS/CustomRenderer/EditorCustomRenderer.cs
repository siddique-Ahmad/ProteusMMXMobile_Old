
using ProteusMMX.DependencyInterface;
using ProteusMMX.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CustomEditor), typeof(EditorCustomRenderer))]

namespace ProteusMMX.iOS.CustomRenderer
{
    class EditorCustomRenderer: EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Layer.CornerRadius = 4;
                Control.Layer.BorderColor = UIColor.FromRGB(0, 0, 0).CGColor;
                Control.Layer.BorderWidth = 1;
            }
        }
    }
}