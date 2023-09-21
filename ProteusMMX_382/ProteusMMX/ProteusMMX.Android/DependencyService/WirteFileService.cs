using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Droid.DependencyService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(WirteFileService))]
namespace ProteusMMX.Droid.DependencyService
{
    public class WirteFileService : IWirteService
    {


        void IWirteService.wirteFile(string FileName, string message)
        {

            string text = message;
            byte[] data = Encoding.ASCII.GetBytes(text);
          

            string DownloadsPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
            string filePath = Path.Combine(DownloadsPath, FileName);
            bool doesExist = File.Exists(filePath);
            if (doesExist==false)
            {
               
                File.WriteAllBytes(filePath, data);

            }
            else
            {
                using (var newdata=System.IO.File.CreateText(filePath))
                {
                    newdata.WriteLine(message);
                }
               
            }
           
        }
    }
}