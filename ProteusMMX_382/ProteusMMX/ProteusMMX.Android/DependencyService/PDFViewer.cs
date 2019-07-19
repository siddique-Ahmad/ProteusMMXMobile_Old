
using System;
using Android.Content;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Droid.DependencyService;

[assembly: Xamarin.Forms.Dependency(typeof(PDFViewer))]
namespace ProteusMMX.Droid.DependencyService
{
    public class PDFViewer : IPDFViewer
    {

        public async void OpenPDF(string filePath)
        {
            try
            {
                string extension = Path.GetExtension(filePath);
                var filename = string.Format(@"{0}" + extension, DateTime.Now.Ticks);
                SavePdf(filename, filePath, extension);
                string documentsPath = Path.Combine((Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)).Path, filename);
                await ViewPDF(documentsPath, filename, extension);
            }
            catch (Exception ex)
            {


            }

        }

        private async void SavePdf(string filename, string link, string extension)
        {
            try
            {
                WebClient client = new WebClient();
                string documentsPath = Path.Combine((Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)).Path, filename);
                client.DownloadFile(link, documentsPath);

            }
            catch (Exception ex)
            {

            }

        }

        private void WriteFileBytes(string externalPath, byte[] bytes)
        {
            File.WriteAllBytes(externalPath, bytes);
        }

        private async Task ViewPDF(string path, string filename, string extension)
        {

            // get mimeTye
            string application = "";
            switch (extension.ToLower())
            {
                case ".txt":
                    application = "text/plain";
                    break;
                case ".doc":
                case ".docx":
                    application = "application/msword";
                    break;
                case ".pdf":
                    application = "application/pdf";
                    break;
                case ".xls":
                case ".xlsx":
                    application = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                    application = "image/jpeg";
                    break;
                default:
                    application = "*/*";
                    break;
            }

            Java.IO.File file = new Java.IO.File(path);
            Android.Net.Uri pdfPath = Android.Net.Uri.FromFile(file);//(externalPath);
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(pdfPath, application);
            intent.SetFlags(ActivityFlags.NewTask);
            Xamarin.Forms.Forms.Context.StartActivity(intent);
            await Task.FromResult(true);

        }

    }
}