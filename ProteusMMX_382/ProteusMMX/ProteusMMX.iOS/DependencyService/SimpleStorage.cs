using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.iOS.DependencyService;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SimpleStorage))]
namespace ProteusMMX.iOS.DependencyService
{
    public class SimpleStorage : ISimpleStorage
    {
        public void Delete(string key)
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject(key);
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        public string Get(string key)
        {
            var prefs = NSUserDefaults.StandardUserDefaults.StringForKey(key);
            if (prefs == null)
                return "";
            else
                return prefs;
        }

        public void Set(string key, string value)
        {
            NSUserDefaults.StandardUserDefaults.SetString(value, key);
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }
    }
}