
using System;
using Android.Content;

using System.Net;
using System.Threading.Tasks;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Droid.DependencyService;
using Android.Support.V4.Content;
using Xamarin.Forms;
using Android.OS;
using Java.IO;
using Plugin.Permissions;
using static Android.Manifest;
using System.IO;
using Plugin.Permissions.Abstractions;
using Permission = Plugin.Permissions.Abstractions.Permission;

[assembly: Xamarin.Forms.Dependency(typeof(PDFViewer))]
namespace ProteusMMX.Droid.DependencyService
{
    public class PDFViewer : IPDFViewer
    {
      
        public async void OpenPDF(string filePath)
        {
            try
            {
                string extension = System.IO.Path.GetExtension(filePath);
                var filename = string.Format(@"{0}" + extension, DateTime.Now.Ticks);
                SavePdf(filename, filePath, extension);
                string documentsPath = System.IO.Path.Combine((Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)).Path, filename);
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
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status == PermissionStatus.Denied)
                {
                    var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Permission was granted
                }
                WebClient client = new WebClient();
                string documentsPath = System.IO.Path.Combine((Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)).Path, filename);
                client.DownloadFile(link, documentsPath);


            }
            catch (Exception ex)
            {

            }

            

        }

        private void WriteFileBytes(string externalPath, byte[] bytes)
        {
            System.IO.File.WriteAllBytes(externalPath, bytes);
        }

        private async Task ViewPDF(string path, string filename, string extension)
        {
            try
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
                file.SetReadable(true);
                Android.Net.Uri uri = Android.Support.V4.Content.FileProvider.GetUriForFile(Xamarin.Forms.Forms.Context, "com.proteusMMX.MM.fileprovider", file);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, application);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.AddFlags(ActivityFlags.NoHistory);
                //intent.AddFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

                try
                {
                    Xamarin.Forms.Forms.Context.StartActivity(intent);
                }
                catch (Exception)
                {
                    // Toast.MakeText(Xamarin.Forms.Forms.Context, "No Application Available to View this file.", ToastLength.Short).Show();
                }
                // Show(file, application);
                await Task.FromResult(true);



            }
            catch (Exception ex)
            {

                throw;
            }
            

        }
        private void Show(Java.IO.File file, string application)
        {
            try
            {
                Intent intent = new Intent(Intent.ActionView);

                Android.Net.Uri uri = GetURI_FileProvider(file);
                if (uri != null)
                {
                    //this is the stuff that was missing - but only if you get the uri from FileProvider
                    intent.SetFlags(Android.Content.ActivityFlags.GrantReadUriPermission);

                }
                else
                {
                    uri = GetUriSimple(file);
                }

                if (uri == null)
                {
                    // throw new InvalidOperationException($"Couldn't show file - outputFilePath->{outputFilePath}");
                }
                intent.SetDataAndType(uri, application);
                intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

                //Intent intentChooser = Intent.CreateChooser(intent, "Open File");
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                //trace
                // uri = null;
            }
        }
        private Android.Net.Uri GetURI_FileProvider(Java.IO.File file)
        {
            Android.Net.Uri uri = null;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop || uri == null) //api >= 21
            {
                try
                {

                    uri = Android.Support.V4.Content.FileProvider.GetUriForFile(Xamarin.Forms.Forms.Context, "com.proteusMMX.MM.FileProvider", file);
                }
                catch (Exception ex)
                {
                    //trace
                }
            }

            return uri;
        }

        private static Android.Net.Uri GetUriSimple(Java.IO.File file)
        {
            Android.Net.Uri uri = null;

            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop) //api < 21
            {
                try
                {
                    // work with api 22
                    uri = Android.Net.Uri.FromFile(file);
                    //  CommonLogger.Instance.Trace($"TsabarimActvityBase:GetURI -Android.Net.Uri.FromFile API Level ->{Build.VERSION.SdkInt} uri.Path->{uri.Path}");
                }
                catch (Exception ex)
                {
                    //trace
                }
            }

            return uri;
        }


    }
}