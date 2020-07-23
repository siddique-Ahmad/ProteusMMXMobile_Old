using ProteusMMX.DependencyInterface;
using Syncfusion.UI.Xaml.Controls.Input;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.UWP.ComboBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MyPicker), typeof(ProteusMMX.UWP.DependencyService.CustomPickerRenderer))]
namespace ProteusMMX.UWP.DependencyService
{

    internal class CustomPickerRenderer : PickerRenderer
    {
        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (null != Control)
            {
                Control.Style = (Windows.UI.Xaml.Style)App.Current.Resources["pickerstyle"];

            }

            //Control.Style = new Windows.UI.Xaml.CornerRadius(5);

            //Windows.UI.Xaml.ResourceDictionary dic = new Windows.UI.Xaml.ResourceDictionary();
            //dic.Source = new Uri("ms-appx:///RoundedEditorRes.xaml");
            //Control.Style = dic["ComboBoxTextBoxStyle"] as Windows.UI.Xaml.Style;

        }

    }

}