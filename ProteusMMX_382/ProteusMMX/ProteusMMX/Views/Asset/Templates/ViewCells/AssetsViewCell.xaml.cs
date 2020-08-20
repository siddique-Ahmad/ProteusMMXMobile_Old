using ProteusMMX.Helpers;
using ProteusMMX.ViewModel.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Asset.Templates.ViewCells
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class AssetsViewCell : ViewCell
	{
		public AssetsViewCell (ref object ParentContext)
		{
			InitializeComponent ();
            string DescriptionTabKey = "";
            this.AssetName.Text = WebControlTitle.GetTargetNameByTitleName("AssetName");
            this.AssetNumber.Text = WebControlTitle.GetTargetNameByTitleName("AssetNumber");
            if (Application.Current.Properties.ContainsKey("DescriptionTabKey"))
            {
                DescriptionTabKey = Application.Current.Properties["DescriptionTabKey"].ToString();
            }
            if (DescriptionTabKey == "E" || DescriptionTabKey == "V")
            {
                this.Description.Text = WebControlTitle.GetTargetNameByTitleName("Description");
                this.DescriptionColon.IsVisible = true;
            }

        }
        public static readonly BindableProperty ParentContextProperty =
         BindableProperty.Create("ParentContext", typeof(object), typeof(AssetsViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as AssetsViewCell).ParentContext = newValue;
            }
        }
    }
}