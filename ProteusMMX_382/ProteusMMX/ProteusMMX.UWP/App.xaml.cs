using ProteusMMX.UWP.DependencyService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Push;
using Syncfusion.SfCalendar.XForms.UWP;
using Syncfusion.SfPicker.XForms.UWP;
using Syncfusion.XForms.UWP.Buttons;
using Syncfusion.XForms.UWP.Border;
using Syncfusion.ListView.XForms.UWP;
using Syncfusion.XForms.UWP.PopupLayout;
using Acr.UserDialogs;
using Popup = Rg.Plugins.Popup.Popup;
namespace ProteusMMX.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc1MzM3QDMxMzgyZTMxMmUzMEZUNkMzUExDNUxsMHA5WGZLdVVPWktYUkl1UTN0NGNFajdBMFB0RUoyd1k9");
            this.InitializeComponent();
#if WINDOWS_UWP
            Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue("Assets", "uwp");
#endif
            this.Suspending += OnSuspending;
          //  AppCenter.Start("7529a611-c100-4257-872f-a9315b2161dc", typeof(Analytics));
           
            //AppCenter.Start("4556bc5f-b45d-41f9-ace8-ecbcba330909", typeof(Analytics));
            //AppCenter.Start("4556bc5f-b45d-41f9-ace8-ecbcba330909", typeof(Push));
            //Analytics.TrackEvent("My custom event");


        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {


            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                // Xamarin.Forms.Forms.Init(e);



                List<Assembly> assembliesToInclude = new List<Assembly>();

                //assembliesToInclude = Rg.Plugins.Popup.Popup.GetExtraAssemblies().ToList();
                assembliesToInclude.Add(typeof(SfPopupLayoutRenderer).GetTypeInfo().Assembly);

                assembliesToInclude.Add(typeof(SignaturePad.Forms.SignaturePadCanvasRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(ZXing.Net.Mobile.Forms.WindowsUniversal.ZXingScannerViewRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(ZXing.Net.Mobile.Forms.ZXingScannerPage).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(ZXing.Net.Mobile.Forms.WindowsUniversal.ZXingBarcodeImageViewRenderer).GetTypeInfo().Assembly);
                // assembliesToInclude.Add(typeof(CarouselView.FormsPlugin.UWP.CarouselViewRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Rg.Plugins.Popup.Windows.Renderers.PopupPageRenderer).GetTypeInfo().Assembly);
                ///Syncfusion Assembly include/////////////////
                assembliesToInclude.Add(typeof(SfButtonRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(SfBorderRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Expander.SfExpanderRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(SfCalendarRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.ComboBox.SfComboBoxRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(SfListViewRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(SfPickerRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.RichTextEditor.SfRichTextEditorRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.TabView.SfTabViewRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Graphics.SfGradientViewRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Accordion.SfAccordionRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.EffectsView.SfEffectsViewRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(SfListViewRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(SfPopupLayoutRenderer).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(UserDialogs).GetTypeInfo().Assembly);

                //IUserDialogs.Init();
                //assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Buttons.SfButtonRenderer).GetTypeInfo().Assembly);
                //assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Buttons.SfCheckBoxRenderer).GetTypeInfo().Assembly);
                //assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Buttons.SfRadioButtonRenderer).GetTypeInfo().Assembly);
                // assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Buttons.SfSegmentedControlRenderer).GetTypeInfo().Assembly);

                // assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Expander.SfExpanderRenderer).GetTypeInfo().Assembly);
                //  assembliesToInclude.Add(typeof(SfCalendarRenderer).GetTypeInfo().Assembly);
                //  assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.ComboBox.SfComboBoxRenderer).GetTypeInfo().Assembly);
                // assembliesToInclude.Add(typeof(Syncfusion.ListView.XForms.UWP.SfListViewRenderer).GetTypeInfo().Assembly);
                //assembliesToInclude.Add(typeof(SfPickerRenderer).GetTypeInfo().Assembly);
                //  assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.RichTextEditor.SfRichTextEditorRenderer).GetTypeInfo().Assembly);
                // assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.TabView.SfTabViewRenderer).GetTypeInfo().Assembly);

                //assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Border.SfBorderRenderer).GetTypeInfo().Assembly);
                // assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Graphics.SfGradientViewRenderer).GetTypeInfo().Assembly);






                // assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.Accordion.SfAccordionRenderer).GetTypeInfo().Assembly);
                //assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.EffectsView.SfEffectsViewRenderer).GetTypeInfo().Assembly);
                //assembliesToInclude.Add(typeof(Syncfusion.XForms.UWP.PopupLayout.SfPopupLayoutRenderer).GetTypeInfo().Assembly);

                Rg.Plugins.Popup.Popup.Init();
                Xamarin.Forms.Forms.Init(e, Rg.Plugins.Popup.Popup.GetExtraAssemblies());

                //Popup.Init();


                //Rg.Plugins.Popup.Popup.Init();

                //Xamarin.Forms.Forms.Init(e, assembliesToInclude);

                Xamarin.Forms.DependencyService.Register<PDFViewer>();
               
                //Xamarin.Forms.Forms.Init(e, Rg.Plugins.Popup.Popup.GetExtraAssemblies());


                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
