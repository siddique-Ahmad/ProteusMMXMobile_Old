//using PdfViewer.Helpers;
using Foundation;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.iOS;
using ProteusMMX.iOS.DependencyService;
using QuickLook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using WebKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(PDFViewer))]
namespace ProteusMMX.iOS.DependencyService
{
    public class PDFViewer : IPDFViewer
    {
        //public void OpenPDF(string path)
        //{
        //    throw new NotImplementedException();
        //}


        public string FileUrl { get; set; }

        public string FileName { get; set; }
        public async void OpenPDF(string fileUrl)
        {
            #region old Code
            //// Test URl
            ////fileUrl = "http://www.iiswc.org/iiswc2009/sample.doc";

            //try
            //{

            //    var uri = new System.Uri(fileUrl);
            //    NSUrl nsURL = new NSUrl(uri.AbsoluteUri);  
            //    UIApplication.SharedApplication.OpenUrl(nsURL);


            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message, ex.StackTrace);
            //} 
            #endregion


            #region NewCode

            try
            {
                //Test URl
                //fileUrl = "http://www.iiswc.org/iiswc2009/sample.doc";

                this.FileUrl = fileUrl;
                var uri = new System.Uri(fileUrl);
                this.FileName = Path.GetFileName(uri.AbsoluteUri);
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                webClient.DownloadDataAsync(uri);
                webClient.DownloadDataCompleted += WebClient_DownloadDataCompleted;


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ex.StackTrace);
            }

            #endregion


        }

        private void WebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string localFilename = this.FileName;
                string localFilePath = Path.Combine(documentsPath, localFilename);

                if (File.Exists(localFilePath))
                {
                    File.Delete(localFilePath);
                }

                File.WriteAllBytes(localFilePath, e.Result);

                ViewDocument(localFilePath, this.FileName);
            }
            catch (Exception ex)
            {

            }



        }

        private async Task ViewDocument(string localFilePath, string fileName)
        {
            try
            {
                var navcontroller = Platform.GetRenderer(App.Current.MainPage).ViewController as UINavigationController;
                QLPreviewItemBundle prevItem = new QLPreviewItemBundle(fileName, localFilePath);
                QLPreviewController previewController = new QLPreviewController();
                previewController.DataSource = new PreviewControllerDS(prevItem);
                navcontroller.PushViewController(previewController, true);
            }
            catch (Exception ex)
            {


            }
        }
    }


    public class PreviewControllerDS : QLPreviewControllerDataSource
    {
        private QLPreviewItem _item;

        public PreviewControllerDS(QLPreviewItem item)
        {
            _item = item;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return _item;
        }

        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return 1;
        }
    }


    public class QLPreviewItemBundle : QLPreviewItem
    {
        string _fileName;
        string _filePath;
        public QLPreviewItemBundle(string fileName, string filePath)
        {
            _fileName = fileName;
            _filePath = filePath;
        }

        public override string ItemTitle
        {
            get
            {
                return _fileName;
            }
        }
        public override NSUrl ItemUrl
        {
            get
            {
                return NSUrl.FromFilename(_filePath);
            }
        }
    }
}