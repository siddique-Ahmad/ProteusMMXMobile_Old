using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProteusMMX.Droid.DependencyService;
using ProteusMMX.Helpers.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SimpleStorage))]
namespace ProteusMMX.Droid.DependencyService
{
    public class SimpleStorage : ISimpleStorage
    {
        public void Set(string key, string value)
        {
            var prefs = Forms.Context.GetSharedPreferences("ProteusMMX", FileCreationMode.Private);
            var prefEditor = prefs.Edit();

            prefEditor.PutString(key, value);
            prefEditor.Commit();
        }

        public string Get(string key)
        {
            var prefs = Forms.Context.GetSharedPreferences("ProteusMMX", FileCreationMode.Private);

            return prefs.GetString(key, null);
        }

        public void Delete(string key)
        {
            var prefs = Forms.Context.GetSharedPreferences("ProteusMMX", FileCreationMode.Private);
            var prefEditor = prefs.Edit();

            prefEditor.Remove(key);
            prefEditor.Commit();
        }
    }
}