using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
using ProteusMMX.Views.Workorder;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMore1 : PopupPage
    {

        public readonly INavigationService _navigationService;
        protected readonly IAuthenticationService _authenticationService;
        public readonly IWorkorderService _workorderService;
        WorkorderListingPageViewModel ViewModel;
        string ShiftSelectedText;
        string PrioritySelectedText;
        public ShowMore1(object navigationData)
        {
            InitializeComponent();
            var navigationParams = navigationData as TargetNavigationData;
            ViewModel = navigationParams.ViewModel;
            //_workorderService = navigationParams.WorkorderService;

            AscendingTitle = WebControlTitle.GetTargetNameByTitleName("Ascending");
            DescendingTitle = WebControlTitle.GetTargetNameByTitleName("Descending");

            List<SortingOrder> list = new List<SortingOrder>();
            //lst.ItemsSource = new List<string>() { AscendingTitle, DescendingTitle, };
            string response = string.Empty;
            if (Application.Current.Properties.ContainsKey("SortingTypeKye"))
            {
                response = Application.Current.Properties["SortingTypeKye"].ToString();
                string SelectIcon = string.Empty;
                if (Device.RuntimePlatform == Device.UWP)
                {
                    SelectIcon = "Assets/check.png";
                }
                else
                {
                    SelectIcon = "check.png";
                }
                if (response == AscendingTitle)
                {
                    list.Add(new SortingOrder { Sortings = AscendingTitle, Images = SelectIcon });
                    list.Add(new SortingOrder { Sortings = DescendingTitle, Images = "" });
                }
                else
                {
                    list.Add(new SortingOrder { Sortings = AscendingTitle, Images = "" });
                    list.Add(new SortingOrder { Sortings = DescendingTitle, Images = SelectIcon });
                }
                lst.ItemsSource = list;
            }
            else
            {
                list.Add(new SortingOrder { Sortings = AscendingTitle, Images = "" });
                list.Add(new SortingOrder { Sortings = DescendingTitle, Images = "" });
                lst.ItemsSource = list;
            }

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
        }


        public async Task OnViewAppearingAsync(VisualElement view)
        {
            //IsGetWorkorderCallFromRequiredDate = false;





        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            //    ////Clear priority///
            //    sortByPrioritypickerTitlesitems.Clear();
            //    SortByPriorityPickerTitles.Clear();
            //    PriorityNameFilterText = null;

            //    ////Clear Shift///
            //    sortByShiftpickerTitlesitems.Clear();
            //    SortByShiftpickerTitles.Clear();
            //    ShiftNameFilterText = null;


            //    ////Clear Location///
            //    sortByLocationpickerTitlesitems.Clear();
            //    SortByLocationpickerTitles.Clear();
            //    LocationNameFilterText = null;

            //    ///Clear RequiredDate////
            //    SortByDateText = null;
        }


        string _ascendingTitle;
        public string AscendingTitle
        {
            get
            {
                return _ascendingTitle;
            }

            set
            {
                if (value != _ascendingTitle)
                {
                    _ascendingTitle = value;
                    OnPropertyChanged(nameof(AscendingTitle));
                }
            }
        }

        string _descendingTitle;
        public string DescendingTitle
        {
            get
            {
                return _descendingTitle;
            }

            set
            {
                if (value != _descendingTitle)
                {
                    _descendingTitle = value;
                    OnPropertyChanged(nameof(DescendingTitle));
                }
            }
        }


        //private async void lst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    string item = e.SelectedItem.ToString();
        //    Application.Current.Properties["Sorting"] = item;
        //  // await PopupNavigation.PopAsync();
        //   // await ViewModel.OnViewAppearingAsync(null);
        //}

        //public WorkorderListingPageViewModel ViewModel
        //{
        //    get
        //    {
        //        return this.BindingContext as WorkorderListingPageViewModel;
        //    }
        //}
        private async void lst_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var data = e.Item as SortingOrder;
            if (data != null && data.Sortings != null)
            {
                string item = data.Sortings;
                Application.Current.Properties["SortingTypeKye"] = item;
                await PopupNavigation.PopAllAsync();
                await ViewModel.OnViewAppearingAsync(null);
            }
            else
            {
                await PopupNavigation.PopAllAsync();
            }
        }
    }


    public class SortingOrder
    {
        public string Sortings { get; set; }
        public string Images { get; set; }
    }
}








