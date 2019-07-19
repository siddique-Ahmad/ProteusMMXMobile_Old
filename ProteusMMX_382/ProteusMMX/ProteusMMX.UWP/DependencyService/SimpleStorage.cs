using ProteusMMX.Helpers.Storage;
using ProteusMMX.UWP.DependencyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SimpleStorage))]
namespace ProteusMMX.UWP.DependencyService
{
    public class SimpleStorage : ISimpleStorage
    {

        public void Set(string key, string value)
        {
            // var prefs = Forms.Init("ProteusMMX", FileCreationMode.Private);
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.CreateContainer("ProteusMMX", Windows.Storage.ApplicationDataCreateDisposition.Always);
            localSettings.Values[key] = value;




        }

        public string Get(string key)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.CreateContainer("ProteusMMX", Windows.Storage.ApplicationDataCreateDisposition.Always);
            object value = localSettings.Values[key];


            if (value == null)
            {
                value = null;
            }
            else
            {
                value.ToString();
            }
            // var prefs = Forms.Context.GetSharedPreferences("ProteusMMX", FileCreationMode.Private);

            return Convert.ToString(value);
        }

        public void Delete(string key)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.CreateContainer("ProteusMMX", Windows.Storage.ApplicationDataCreateDisposition.Always);
            localSettings.Values.Remove(key);

        }
    }
}
