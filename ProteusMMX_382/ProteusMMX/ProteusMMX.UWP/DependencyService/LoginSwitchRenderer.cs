using ProteusMMX.DependencyInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(LoginSwitch), typeof(ProteusMMX.UWP.DependencyService.LoginSwitchRenderer))]
namespace ProteusMMX.UWP.DependencyService
{
    internal class LoginSwitchRenderer : SwitchRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (null != Control)
            {
                if (null != Control.OnContent)
                {
                    //Control.OnContent = "Remember Me";
                    //Control.OffContent = "Remember Me";
                    Control.OnContent = string.Empty;
                    Control.OffContent = string.Empty;
                }

             
            }
        }
    }
}
