using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomSwitch), typeof(ProteusMMX.UWP.DependencyService.CustomSwitchRenderer))]
namespace ProteusMMX.UWP.DependencyService
{

    internal class CustomSwitchRenderer : SwitchRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (null != Control)
            {
                if (null != Control.OnContent)
                {
                    Control.OnContent = "Yes";
                    Control.OffContent = "No";
                }
            }
        }
    }
}
