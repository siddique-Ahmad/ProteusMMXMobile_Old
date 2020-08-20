using ProteusMMX.Helpers;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.CommonModels;
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
        List<FormControl> _assetControlsNew = new List<FormControl>();
        public List<FormControl> AssetControlsNew
        {
            get
            {
                return _assetControlsNew;
            }

            set
            {
                _assetControlsNew = value;
            }
        }
        ViewCell theViewCell = new ViewCell();
        public AssetsViewCell(ref object ParentContext)
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("AssetsDetailsControls"))
            {
                SubModule AssetSubModule = Application.Current.Properties["AssetsDetailsControls"] as SubModule;
                AssetControlsNew = AssetSubModule.listControls;
            }
            string DescriptionTabKey = "";
            this.ParentContext = ParentContext;
            var assetName = AssetControlsNew.FirstOrDefault(x => x.ControlName == "AssetName");
            if (assetName != null)
            {
                this.AssetName.Text = assetName.TargetName;
                
            }
            else
            {
                this.AssetName.Text = WebControlTitle.GetTargetNameByTitleName("AssetName");
            }
           
            this.ShowAssetSystem.Text = WebControlTitle.GetTargetNameByTitleName("show")+ " "  + WebControlTitle.GetTargetNameByTitleName("AssetSystem");

            var assetNumber = AssetControlsNew.FirstOrDefault(x => x.ControlName == "AssetNumber");
            if (assetNumber != null)
            {
                this.AssetNumber.Text = assetNumber.TargetName;

            }
            else
            {
                this.AssetNumber.Text = WebControlTitle.GetTargetNameByTitleName("AssetNumber");
            }
            
            if (Application.Current.Properties.ContainsKey("DescriptionTabKey"))
            {
                DescriptionTabKey = Application.Current.Properties["DescriptionTabKey"].ToString();
            }
            if (DescriptionTabKey == "E" || DescriptionTabKey == "V")
            {
                var description = AssetControlsNew.FirstOrDefault(x => x.ControlName == "Description");
                if (description != null)
                {
                    this.Description.Text = description.TargetName;

                }
                else
                {
                    this.Description.Text = WebControlTitle.GetTargetNameByTitleName("Description");
                }
               
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
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = theViewCell.BindingContext as Assets;

                await (ParentContext as AssetListingPageViewModel).NavigationFromAssetListing(item.AssetSystemID.ToString(), item.AssetSystemName, item.AssetSystemNumber);

            }
            catch (Exception ex)
            {


            }
        }
        private void OnBindingContextChanged(object sender, EventArgs e)
        {
             theViewCell = ((ViewCell)sender);

            
        }
    }
}