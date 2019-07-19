using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.SelectionListPageServices.Tool;
using ProteusMMX.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Tools
{
    public class ToolCribListSelectionPageViewModel : ViewModelBase
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;
        protected readonly IFormLoadInputService _formLoadInputService;
        protected readonly IToolService _toolService;

        #endregion

        #region Properties

        #region Page Properties

        string _pageTitle = "";
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }

            set
            {
                if (value != _pageTitle)
                {
                    _pageTitle = value;
                    OnPropertyChanged("PageTitle");
                }
            }
        }

        string _userID = AppSettings.User.UserID.ToString();
        public string UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                if (value != _userID)
                {
                    _userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }


        #endregion

        #region Title Properties
        string _selectTitle;
        public string SelectTitle
        {
            get
            {
                return _selectTitle;
            }

            set
            {
                if (value != _selectTitle)
                {
                    _selectTitle = value;
                    OnPropertyChanged("SelectTitle");
                }
            }
        }
        string _welcomeTextTitle;
        public string WelcomeTextTitle
        {
            get
            {
                return _welcomeTextTitle;
            }

            set
            {
                if (value != _welcomeTextTitle)
                {
                    _welcomeTextTitle = value;
                    OnPropertyChanged("WelcomeTextTitle");
                }
            }
        }

        string _totalRecordTitle;
        public string TotalRecordTitle
        {
            get
            {
                return _totalRecordTitle;
            }

            set
            {
                if (value != _totalRecordTitle)
                {
                    _totalRecordTitle = value;
                    OnPropertyChanged("TotalRecordTitle");
                }
            }
        }


        int _totalRecordCount;
        public int TotalRecordCount
        {
            get
            {
                return _totalRecordCount;
            }

            set
            {
                if (value != _totalRecordCount)
                {
                    _totalRecordCount = value;
                    OnPropertyChanged("TotalRecordCount");
                }
            }
        }


        string _searchPlaceholder;
        public string SearchPlaceholder
        {
            get
            {
                return _searchPlaceholder;
            }

            set
            {
                if (value != _searchPlaceholder)
                {
                    _searchPlaceholder = value;
                    OnPropertyChanged("SearchPlaceholder");
                }
            }
        }

        string _goTitle;
        public string GoTitle
        {
            get
            {
                return _goTitle;
            }

            set
            {
                if (value != _goTitle)
                {
                    _goTitle = value;
                    OnPropertyChanged("GoTitle");
                }
            }
        }

        string _scanTitle;
        public string ScanTitle
        {
            get
            {
                return _scanTitle;
            }

            set
            {
                if (value != _scanTitle)
                {
                    _scanTitle = value;
                    OnPropertyChanged("ScanTitle");
                }
            }
        }

        string _searchButtonTitle;
        public string SearchButtonTitle
        {
            get
            {
                return _searchButtonTitle;
            }

            set
            {
                if (value != _searchButtonTitle)
                {
                    _searchButtonTitle = value;
                    OnPropertyChanged("SearchButtonTitle");
                }
            }
        }


        string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (value != _searchText)
                {
                    _searchText = value;
                    OnPropertyChanged("SearchText");
                    SearchText_TextChanged();
                }
            }
        }


        #endregion

        #region ListView Properties

        int _pageNumber = 1;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }

            set
            {
                if (value != _pageNumber)
                {
                    _pageNumber = value;
                    OnPropertyChanged(nameof(PageNumber));
                }
            }
        }

        int _rowCount = 10;
        public int RowCount
        {
            get
            {
                return _rowCount;
            }

            set
            {
                if (value != _rowCount)
                {
                    _rowCount = value;
                    OnPropertyChanged(nameof(RowCount));
                }
            }
        }


        ObservableCollection<ToolTipCrib> _pickerItemCollection = new ObservableCollection<ToolTipCrib>();

        public ObservableCollection<ToolTipCrib> PickerItemCollection
        {
            get
            {
                return _pickerItemCollection;
            }

        }

        ToolTipCrib _nullItem = new ToolTipCrib() { ToolCribID = null, ToolCribName = string.Empty };
        public ToolTipCrib NullItem
        {
            get
            {
                return _nullItem;
            }
        }

        #endregion

        #endregion

        #region Commands
        public ICommand ScanCommand => new AsyncCommand(SearchPickerItem);

        public ICommand ItemSelectedCommand => new Command<object>(OnItemSelectedAsync);
        public ICommand SelectNullCommand => new AsyncCommand(SelectNull);



        #endregion

        #region Methods

        public async Task SearchPickerItem()
        {

            try
            {
                OperationInProgress = true;


                #region Barcode Section and Search Section

                if (SearchButtonTitle == ScanTitle)
                {
                    var options = new MobileBarcodeScanningOptions()
                    {
                        AutoRotate = false,
                        TryHarder = true,

                    };

                    ZXingScannerPage _scanner = new ZXingScannerPage(options)
                    {
                        DefaultOverlayTopText = "Align the barcode within the frame",
                        DefaultOverlayBottomText = string.Empty,
                        DefaultOverlayShowFlashButton = true
                    };

                    _scanner.OnScanResult += _scanner_OnScanResult;
                    var navPage = App.Current.MainPage as NavigationPage;
                    await navPage.PushAsync(_scanner);
                }

                else
                {
                    //reset pageno. and start search again.
                    await RefillPickerItemsCollection();

                }

                #endregion

                //await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();
            }
            catch (Exception ex)
            {
                OperationInProgress = false;

            }

            finally
            {
                OperationInProgress = false;

            }
        }

        private void _scanner_OnScanResult(ZXing.Result result)
        {
            this.SearchText = result.Text;

            ///Pop the scanner page
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                //reset pageno. and start search again.
                await RefillPickerItemsCollection();


            });
        }

        private async void OnItemSelectedAsync(object item)
        {
            if (item != null)
            {
                MessagingCenter.Send(item, MessengerKeys.ToolCribRequested);
                await NavigationService.NavigateBackAsync();
               

            }
        }


        public async Task SelectNull()
        {
            OnItemSelectedAsync(NullItem);
        }
        public ToolCribListSelectionPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IToolService ToolService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _toolService = ToolService;
        }
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

               

                //var targetNavigationData = navigationData as TargetNavigationData;

                //this.FacilityID = targetNavigationData.FacilityID;
                //this.LocationID = targetNavigationData.LocationID;

              //  OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                await GetPickerItems();

            }
            catch (Exception ex)
            {
                OperationInProgress = false;

            }

            finally
            {
                OperationInProgress = false;
            }
        }
        public async Task SetTitlesPropertiesForPage()
        {

         
                PageTitle = WebControlTitle.GetTargetNameByTitleName("");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName( "TotalRecords");
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            
        }
        private void SearchText_TextChanged()
        {

            try
            {
                if (SearchText == null || SearchText.Length == 0)
                {
                    SearchButtonTitle = ScanTitle;
                }

                else if (SearchText.Length > 0)
                {
                    SearchButtonTitle = GoTitle;
                }
            }
            catch (Exception ex)
            {


            }
        }

        private async Task RefillPickerItemsCollection()
        {
            PageNumber = 1;
            await RemoveAllFromPickerItemCollection();
            await GetPickerItems();
        }

        private async Task RemoveAllFromPickerItemCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _pickerItemCollection.Clear();
                OnPropertyChanged(nameof(PickerItemCollection));
            });

        }

        async Task GetPickerItems()
        {
            try
            {
            //    OperationInProgress = true;
                ServiceOutput ToolCribResponse = null;
                ToolCribResponse = await _toolService.GetToolCrib(UserID, PageNumber.ToString(), RowCount.ToString(), SearchText);


                if (ToolCribResponse != null && ToolCribResponse.workOrderWrapper != null && ToolCribResponse.workOrderWrapper.ToolTipCribs != null)
                {
                    var toolcribs = ToolCribResponse.workOrderWrapper.ToolTipCribs;
                    AddtoolcribToPickerItemCollection(toolcribs);
                }


            }
            catch (Exception ex)
            {

                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }
        }

        public async Task GetPickerItemsAuto()
        {
            PageNumber++;
            await GetPickerItems();
        }

        private void AddtoolcribToPickerItemCollection(List<ToolTipCrib> toolcrib)
        {
            if (toolcrib != null && toolcrib.Count > 0)
            {
                foreach (var item in toolcrib)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _pickerItemCollection.Add(item);
                        OnPropertyChanged(nameof(PickerItemCollection));
                    });

                }
            }
        }
        #endregion
    }
}
