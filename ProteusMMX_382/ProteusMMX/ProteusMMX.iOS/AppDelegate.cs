﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ProteusMMX.Controls;
using ProteusMMX.iOS.DependencyService;
using UIKit;
using Xamarin.Forms;

namespace ProteusMMX.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
       
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // The background color of the tab bar
            UITabBar.Appearance.BarTintColor = UIColor.FromRGB(0, 109, 224);
            // Change the tint of the selected image and text
            UITabBar.Appearance.SelectedImageTintColor = UIColor.White;
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            Syncfusion.XForms.iOS.Core.SfAvatarViewRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfSwitchRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer.Init();
            Syncfusion.XForms.iOS.Expander.SfExpanderRenderer.Init();
            Syncfusion.XForms.iOS.RichTextEditor.SfRichTextEditorRenderer.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            Syncfusion.XForms.iOS.TextInputLayout.SfTextInputLayoutRenderer.Init();
            LoadApplication(new App());
            #region MyRegion

            #endregion
            //WireUpLongRunningTask();
            app.RegisterUserNotificationSettings(UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert
                        | UIUserNotificationType.Badge
                        | UIUserNotificationType.Sound,
                        new NSSet()));

            nint taskID = 111;
            app.BeginBackgroundTask("showNotification", expirationHandler: () => {
                UIApplication.SharedApplication.EndBackgroundTask(taskID);
            });
            return base.FinishedLaunching(app, options);
        }
       
    }
}
