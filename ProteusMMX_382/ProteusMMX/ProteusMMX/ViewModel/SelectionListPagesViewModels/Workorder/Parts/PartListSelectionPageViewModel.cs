﻿using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.InventoryModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.SelectionListPageServices.Parts;
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

namespace ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Parts
{
    public class PartListSelectionPageViewModel : ViewModelBase
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;
        protected readonly IFormLoadInputService _formLoadInputService;
        protected readonly IPartService _partService;
        public StockroomPartsSearch PartToSearch { get; set; }

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
        int _workorderID;
        public int WorkorderID
        {
            get
            {
                return _workorderID;
            }

            set
            {
                if (value != _workorderID)
                {
                    _workorderID = value;
                    OnPropertyChanged(nameof(WorkorderID));
                }
            }
        }

        int? _stockroomID;
        public int? StockroomID
        {
            get
            {
                return _stockroomID;
            }

            set
            {
                if (value != _stockroomID)
                {
                    _stockroomID = value;
                    OnPropertyChanged(nameof(StockroomID));
                }
            }
        }

        #endregion

        #region Title Properties

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


        ObservableCollection<StockroomPart> _pickerItemCollection = new ObservableCollection<StockroomPart>();

        public ObservableCollection<StockroomPart> PickerItemCollection
        {
            get
            {
                return _pickerItemCollection;
            }

        }

        StockroomPart _nullItem = new StockroomPart() { StockroomPartID = null, PartName = string.Empty };
        public StockroomPart NullItem
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

                    options.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.CODE_39, ZXing.BarcodeFormat.CODE_93, ZXing.BarcodeFormat.CODE_128, ZXing.BarcodeFormat.EAN_13, ZXing.BarcodeFormat.QR_CODE };
                    options.TryHarder = false; options.BuildBarcodeReader().Options.AllowedLengths = new[] { 44 };
                    ZXingScannerPage _scanner = new ZXingScannerPage(options)
                    {
                        DefaultOverlayTopText = "Align the barcode within the frame",
                        DefaultOverlayBottomText = string.Empty,
                        DefaultOverlayShowFlashButton = true
                    };
                    _scanner.AutoFocus();
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
                MessagingCenter.Send(item, MessengerKeys.PartRequested);
                await NavigationService.NavigateBackAsync();
               

            }
        }


        public async Task SelectNull()
        {
            OnItemSelectedAsync(NullItem);
        }
        public PartListSelectionPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IPartService PartService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _partService = PartService;
        }
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

              

                var targetNavigationData = navigationData as TargetNavigationData;

                this.StockroomID = targetNavigationData.StockroomID;
                this.WorkorderID = targetNavigationData.WorkOrderId;

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
                TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName("TotalRecords");
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
               // OperationInProgress = true;
                ServiceOutput PartResponse = null;
                PartToSearch = new StockroomPartsSearch();
                PartToSearch.WorkorderID = WorkorderID;
                PartToSearch.StockroomID = StockroomID.ToString();
                PartToSearch.PageNumber = PageNumber;
                PartToSearch.RowspPage = RowCount;
                if(string.IsNullOrWhiteSpace(this.SearchText))
                {
                    PartToSearch.PartNumber = string.Empty;
                }
                else
                {
                    PartToSearch.PartNumber = this.SearchText;
                }
             
                PartResponse = await _partService.GetParts(PartToSearch);


                if (PartResponse != null && PartResponse.inventoryWrapper != null && PartResponse.inventoryWrapper.stockroomparts != null)
                {
                    var partslist = PartResponse.inventoryWrapper.stockroomparts;
                    AddtoolcribToPickerItemCollection(partslist);
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

        private void AddtoolcribToPickerItemCollection(List<StockroomPart> partlist)
        {
            if (partlist != null && partlist.Count > 0)
            {
                foreach (var item in partlist)
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
