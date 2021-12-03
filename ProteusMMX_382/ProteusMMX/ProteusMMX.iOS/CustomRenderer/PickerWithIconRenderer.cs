using Foundation;
using ProteusMMX.DependencyInterface;
using ProteusMMX.iOS.CustomRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(PickerWithIconRenderer))]
namespace ProteusMMX.iOS.CustomRenderer
{
   public class PickerWithIconRenderer : PickerRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			var element = (CustomPicker)this.Element;

			if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
			{
				var downarrow = UIImage.FromBundle(element.Image);
				Control.RightViewMode = UITextFieldViewMode.Always;
				Control.RightView = new UIImageView(downarrow);
			}
		}
	}
}