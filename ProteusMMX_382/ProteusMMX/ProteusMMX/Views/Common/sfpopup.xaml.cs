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
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.PopupLayout;
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
    public partial class sfpopup : SfPopupLayout
    {

        public readonly INavigationService _navigationService;
        protected readonly IAuthenticationService _authenticationService;
        public readonly IWorkorderService _workorderService;
        WorkorderListingPageViewModel ViewModel;
        string ShiftSelectedText;
        string PrioritySelectedText;

        public sfpopup()
        {
            InitializeComponent();

            List<string> lsts = new List<string>() { "item 1", "item 2", "Item 3" };

            //SfPopupLayout popupLayout = new SfPopupLayout();

            popupLayout.Show();
        }

        public readonly string[] strings = new[] {
        "One",
        "Two",
        "Three",
        "Four",
        "Five",
        "Six",
    };

        public string[] Strings
        {
            get
            {
                return strings;
            }
        }
        public async Task OnViewAppearingAsync(VisualElement view)
        {
            //lst.ItemsSource = new List<string>() { "item 1", "item 2", "Item 3" };
            //lst.SelectedItem = "item 2";
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

    }
}








