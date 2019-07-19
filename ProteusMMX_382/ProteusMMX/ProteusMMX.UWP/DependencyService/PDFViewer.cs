using ProteusMMX.DependencyInterface;
using ProteusMMX.UWP.DependencyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(PDFViewer))]
namespace ProteusMMX.UWP.DependencyService
{
    
    public class PDFViewer : IPDFViewer
    {

        public async void OpenPDF(string fileUrl)
        {
            // Test URl
            //fileUrl = "http://www.iiswc.org/iiswc2009/sample.doc";

            Windows.System.LauncherOptions options = new Windows.System.LauncherOptions();
            options.DisplayApplicationPicker = true;
            options.DesiredRemainingView = Windows.UI.ViewManagement.ViewSizePreference.UseMore;

            //string extension = Path.GetExtension(fileUrl);

            //options.ContentType = "application/pdf";
            await Windows.System.Launcher.LaunchUriAsync(new Uri(fileUrl), options);
        }
    }
}
