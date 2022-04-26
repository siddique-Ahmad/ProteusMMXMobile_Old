using Foundation;
using ProteusMMX.iOS.CustomRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace ProteusMMX.iOS.CustomRenderer
{
    public class ExtendedTabbedPageRenderer : TabbedRenderer
    {
        /// <summary>
        /// This method is overridden to allow the tab bar items to have their fonts updated appropriately.
        /// </summary>
        /// <param name="animated">Whether it's animated or not.</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            UpdateAllTabBarItems();
        }

        /// <summary>
        /// Updates the tab bar item for each page included in the TabbedPage.
        /// </summary>
        private void UpdateAllTabBarItems()
        {
            foreach (var controller in ViewControllers)
            {
                controller.TabBarItem.SetTitleTextAttributes(StandardAttributes, UIControlState.Normal);
                controller.TabBarController.TabBar.BackgroundColor= Color.FromHex("#006de0").ToUIColor();
                controller.TabBarController.TabBar.TintColor = UIColor.White;
            }
        }

        /// <summary>
        /// Stores the UITextAttributes for this class.
        /// </summary>
        /// <value>The standard attributes.</value>
        private static UITextAttributes StandardAttributes { get; } = new UITextAttributes { Font = FontManager.GetFont(16.0f) };
    }
}