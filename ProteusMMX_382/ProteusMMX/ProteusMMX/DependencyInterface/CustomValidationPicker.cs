using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.DependencyInterface
{
    public class CustomValidationPicker : Picker
    {
		public static readonly BindableProperty ImageProperty =
			BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

		public string Image
		{
			get { return (string)GetValue(ImageProperty); }
			set { SetValue(ImageProperty, value); }
		}

		public static readonly BindableProperty BorderProperty =
			BindableProperty.Create(nameof(Border), typeof(string), typeof(CustomPicker), string.Empty);

		public string Border
		{
			get { return (string)GetValue(BorderProperty); }
			set { SetValue(BorderProperty, value); }
		}
	}
}
