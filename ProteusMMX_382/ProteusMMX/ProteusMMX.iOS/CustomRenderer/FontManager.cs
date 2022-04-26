using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace ProteusMMX.iOS.CustomRenderer
{
    public static class FontManager
    {
        /// <summary>
        /// This method should be executed during program launch to configure the rendering.
        /// </summary>
        public static void ConfigureFonts()
        {
            var titleAttribute = new UITextAttributes { Font = GetFont(UIFont.LabelFontSize) };
            // Updates the ToolbarItem appearance.
            UIBarButtonItem.Appearance.SetTitleTextAttributes(titleAttribute, UIControlState.Normal);
            // Sets the NavigationBar title items. Also sets the format for TabbedPage tab items for some reason.
            UINavigationBar.Appearance.SetTitleTextAttributes(titleAttribute);
            // Just being thorough...
            UILabel.Appearance.Font = GetFont(UIFont.LabelFontSize);
        }

        /// <summary>
        /// Gets a <see cref="UIFont"/> object for the standard font with the desired size.
        /// </summary>
        /// <returns>UIFont object for use.</returns>
        /// <param name="fontSize">Font size.</param>
        public static UIFont GetFont(nfloat fontSize)
        {
            return UIFont.FromName(StandardFontName, fontSize);
        }

        /// <summary>
        /// Gets the iOS font name for the standard font for this application.
        /// </summary>
        /// <value>The name of the standard font.</value>
        public static string StandardFontName => "AvenirNextCondensed-Regular";
    }
}