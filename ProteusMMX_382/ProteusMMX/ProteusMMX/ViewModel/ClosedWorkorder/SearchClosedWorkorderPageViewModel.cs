using ProteusMMX.Helpers;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.CloseWorkorder;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using ProteusMMX.Helpers.DateTime;

namespace ProteusMMX.ViewModel.ClosedWorkorder
{
    public class SearchClosedWorkorderPageViewModel : ViewModelBase
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly ICloseWorkorderService _closeWorkorderService;


        #endregion


        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand SearchCloseWorkorderByAssetNumberCommand => new AsyncCommand(SearchCloseWorkorderByAssetNumber);
        public ICommand SearchCloseWorkorderByWorkorderNumberCommand => new AsyncCommand(SearchCloseWorkorderByWorkorderNumber);
        public ICommand SearchCloseWorkorderByLocationCommand => new AsyncCommand(SearchCloseWorkorderByLocation);
        public ICommand SearchCloseWorkorderByPartNumberCommand => new AsyncCommand(SearchCloseWorkorderByPartNumber);
        public ICommand SearchCloseWorkorderByDateCommand => new AsyncCommand(SearchCloseWorkorderByDate);

        #endregion

        #region Properties
        bool _locationPlaceholderLicensingVisibility = true;
        public bool LocationPlaceholderLicensingVisibility
        {
            get
            {
                return _locationPlaceholderLicensingVisibility;
            }

            set
            {
                if (value != _locationPlaceholderLicensingVisibility)
                {
                    _locationPlaceholderLicensingVisibility = value;
                    OnPropertyChanged(nameof(LocationPlaceholderLicensingVisibility));
                }
            }
        }

        bool _partPlaceholderLicensingVisibility = true;
        public bool PartPlaceholderLicensingVisibility
        {
            get
            {
                return _partPlaceholderLicensingVisibility;
            }

            set
            {
                if (value != _partPlaceholderLicensingVisibility)
                {
                    _partPlaceholderLicensingVisibility = value;
                    OnPropertyChanged(nameof(PartPlaceholderLicensingVisibility));
                }
            }
        }
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

        
   
        string _startDateTitle;
        public string StartDateTitle
        {
            get
            {
                return _startDateTitle;
            }

            set
            {
                if (value != _startDateTitle)
                {
                    _startDateTitle = value;
                    OnPropertyChanged(nameof(StartDateTitle));
                }
            }
        }
        string _endDateTitle;
        public string EndDateTitle
        {
            get
            {
                return _endDateTitle;
            }

            set
            {
                if (value != _endDateTitle)
                {
                    _endDateTitle = value;
                    OnPropertyChanged(nameof(EndDateTitle));
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
                    OnPropertyChanged(nameof(WelcomeTextTitle));
                }
            }
        }


        #endregion

        #region Dialog Actions Titles

        string _logoutTitle = "";
        public string LogoutTitle
        {
            get
            {
                return _logoutTitle;
            }

            set
            {
                if (value != _logoutTitle)
                {
                    _logoutTitle = value;
                    OnPropertyChanged("LogoutTitle");
                }
            }
        }

        string _selectTitle = "";
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

        string _cancelTitle = "";
        public string CancelTitle
        {
            get
            {
                return _cancelTitle;
            }

            set
            {
                if (value != _cancelTitle)
                {
                    _cancelTitle = value;
                    OnPropertyChanged("CancelTitle");
                }
            }
        }

        string _selectOptionsTitle ;
        public string SelectOptionsTitle
        {
            get
            {
                return _selectOptionsTitle;
            }

            set
            {
                if (value != _selectOptionsTitle)
                {
                    _selectOptionsTitle = value;
                    OnPropertyChanged("SelectOptionsTitle");
                }
            }
        }

        #endregion


        #endregion

        #region Closed Workorder Properties

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
                    OnPropertyChanged(nameof(GoTitle));
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
                    OnPropertyChanged(nameof(ScanTitle));
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
                    OnPropertyChanged(nameof(SearchButtonTitle));


                }
            }
        }

        string _searchButtonTitle1;
        public string SearchButtonTitle1
        {
            get
            {
                return _searchButtonTitle1;
            }

            set
            {
                if (value != _searchButtonTitle1)
                {
                    _searchButtonTitle1 = value;
                    OnPropertyChanged(nameof(SearchButtonTitle1));

                  
                }
            }
        }

         string _searchButtonTitle2;
        public string SearchButtonTitle2
        {
            get
            {
                return _searchButtonTitle2;
            }

            set
            {
                if (value != _searchButtonTitle2)
                {
                    _searchButtonTitle2 = value;
                    OnPropertyChanged(nameof(SearchButtonTitle2));
                }
            }
        }

        string _searchButtonTitle3;
        public string SearchButtonTitle3
        {
            get
            {
                return _searchButtonTitle3;
            }

            set
            {
                if (value != _searchButtonTitle3)
                {
                    _searchButtonTitle3 = value;
                    OnPropertyChanged(nameof(SearchButtonTitle3));
                }
            }
        }


        string _searchButtonTitle4;
        public string SearchButtonTitle4
        {
            get
            {
                return _searchButtonTitle4;
            }

            set
            {
                if (value != _searchButtonTitle4)
                {
                    _searchButtonTitle4 = value;
                    OnPropertyChanged(nameof(SearchButtonTitle4));
                }
            }
        }

        DateTime _startDate=DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).AddDays(-15);
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }



        DateTime _endDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }

            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }




        string _closeWorkorderByAssetNumberText;
        public string CloseWorkorderByAssetNumberText
        {
            get
            {
                return _closeWorkorderByAssetNumberText;
            }

            set
            {
                if (value != _closeWorkorderByAssetNumberText)
                {
                    _closeWorkorderByAssetNumberText = value;
                    OnPropertyChanged(nameof(CloseWorkorderByAssetNumberText));

                    try
                    {
                        if (CloseWorkorderByAssetNumberText == null || CloseWorkorderByAssetNumberText.Length == 0)
                        {
                            SearchButtonTitle1 = ScanTitle;
                        }

                        else if (CloseWorkorderByAssetNumberText.Length > 0)
                        {
                            SearchButtonTitle1 = GoTitle;
                        }
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
        }


        string _closeWorkorderByAssetNumberPlaceholderText;
        public string CloseWorkorderByAssetNumberPlaceholderText
        {
            get
            {
                return _closeWorkorderByAssetNumberPlaceholderText;
            }

            set
            {
                if (value != _closeWorkorderByAssetNumberPlaceholderText)
                {
                    _closeWorkorderByAssetNumberPlaceholderText = value;
                    OnPropertyChanged(nameof(CloseWorkorderByAssetNumberPlaceholderText));
                }
            }
        }


        string _closeWorkorderByWorkorderNumberText;
        public string CloseWorkorderByWorkorderNumberText
        {
            get
            {
                return _closeWorkorderByWorkorderNumberText;
            }

            set
            {
                if (value != _closeWorkorderByWorkorderNumberText)
                {
                    _closeWorkorderByWorkorderNumberText = value;
                    OnPropertyChanged(nameof(CloseWorkorderByWorkorderNumberText));

                    try
                    {
                        if (CloseWorkorderByWorkorderNumberText == null || CloseWorkorderByWorkorderNumberText.Length == 0)
                        {
                            SearchButtonTitle2 = ScanTitle;
                        }

                        else if (CloseWorkorderByWorkorderNumberText.Length > 0)
                        {
                            SearchButtonTitle2 = GoTitle;
                        }
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
        }


        string _closeWorkorderByWorkorderNumberPlaceholderText;
        public string CloseWorkorderByWorkorderNumberPlaceholderText
        {
            get
            {
                return _closeWorkorderByWorkorderNumberPlaceholderText;
            }

            set
            {
                if (value != _closeWorkorderByWorkorderNumberPlaceholderText)
                {
                    _closeWorkorderByWorkorderNumberPlaceholderText = value;
                    OnPropertyChanged(nameof(CloseWorkorderByWorkorderNumberPlaceholderText));
                }
            }
        }



        string _closeWorkorderByLocationText;
        public string CloseWorkorderByLocationText
        {
            get
            {
                return _closeWorkorderByLocationText;
            }

            set
            {
                if (value != _closeWorkorderByLocationText)
                {
                    _closeWorkorderByLocationText = value;
                    OnPropertyChanged(nameof(CloseWorkorderByLocationText));

                    try
                    {
                        if (CloseWorkorderByLocationText == null || CloseWorkorderByLocationText.Length == 0)
                        {
                            SearchButtonTitle3 = ScanTitle;
                        }

                        else if (CloseWorkorderByLocationText.Length > 0)
                        {
                            SearchButtonTitle3 = GoTitle;
                        }
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
        }


        string _closeWorkorderByLocationPlaceholderText;
        public string CloseWorkorderByLocationPlaceholderText
        {
            get
            {
                return _closeWorkorderByLocationPlaceholderText;
            }

            set
            {
                if (value != _closeWorkorderByLocationPlaceholderText)
                {
                    _closeWorkorderByLocationPlaceholderText = value;
                    OnPropertyChanged(nameof(CloseWorkorderByLocationPlaceholderText));
                }
            }
        }



        string _closeWorkorderByPartNumberText;
        public string CloseWorkorderByPartNumberText
        {
            get
            {
                return _closeWorkorderByPartNumberText;
            }

            set
            {
                if (value != _closeWorkorderByPartNumberText)
                {
                    _closeWorkorderByPartNumberText = value;
                    OnPropertyChanged(nameof(CloseWorkorderByPartNumberText));

                    try
                    {
                        if (CloseWorkorderByPartNumberText == null || CloseWorkorderByPartNumberText.Length == 0)
                        {
                            SearchButtonTitle4 = ScanTitle;
                        }

                        else if (CloseWorkorderByPartNumberText.Length > 0)
                        {
                            SearchButtonTitle4 = GoTitle;
                        }
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }
        }


        string _closeWorkorderByPartNumberPlaceholderText;
        public string CloseWorkorderByPartNumberPlaceholderText
        {
            get
            {
                return _closeWorkorderByPartNumberPlaceholderText;
            }

            set
            {
                if (value != _closeWorkorderByPartNumberPlaceholderText)
                {
                    _closeWorkorderByPartNumberPlaceholderText = value;
                    OnPropertyChanged(nameof(CloseWorkorderByPartNumberPlaceholderText));
                }
            }
        }





        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;

                //if (ConnectivityService.IsConnected == false)
                //{
                //    await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
                //    return;

                //}

                if (navigationData != null)
                {

                    var navigationParams = navigationData as PageParameters;

                  



                }
                if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                {
                    this.LocationPlaceholderLicensingVisibility = false;
                    this.PartPlaceholderLicensingVisibility = false;


                }
                //FormLoadInputForWorkorder = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                await SetTitlesPropertiesForPage();




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

        public SearchClosedWorkorderPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService , ICloseWorkorderService closeWorkorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _closeWorkorderService = closeWorkorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("ClosedWorkOrder");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                StartDateTitle = WebControlTitle.GetTargetNameByTitleName("StartDate");
                EndDateTitle = WebControlTitle.GetTargetNameByTitleName("EndDate");


                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Search");
               
                SearchButtonTitle1 = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle2 = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle3 = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle4 = WebControlTitle.GetTargetNameByTitleName("Scan");
                CloseWorkorderByAssetNumberPlaceholderText = WebControlTitle.GetTargetNameByTitleName("SearchOrScanClosedWorkOrderByAssetNumber");
                CloseWorkorderByWorkorderNumberPlaceholderText = WebControlTitle.GetTargetNameByTitleName("SearchOrScanClosedWorkOrderByWorkOrderNumber");
                CloseWorkorderByPartNumberPlaceholderText = WebControlTitle.GetTargetNameByTitleName("SearchClosedWorkOrderByPartNumber");
                CloseWorkorderByLocationPlaceholderText = WebControlTitle.GetTargetNameByTitleName("SearchOrScanClosedWorkOrderByLocation");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");


            }
            catch (Exception ex)
            {


            }

            finally
            {

            }
        }


        public async Task SearchCloseWorkorderByAssetNumber()
        {
            #region Barcode Section and Search Section

            if (SearchButtonTitle1 == ScanTitle)
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
                _scanner.OnScanResult += ScannerResultCloseWorkorderByAssetNumber;
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PushAsync(_scanner);
            }

            else
            {
                await GetCloseWorkorderByAssetNumber();

            }

            #endregion
        }

        public async Task SearchCloseWorkorderByWorkorderNumber()
        {
            #region Barcode Section and Search Section

            if (SearchButtonTitle2 == ScanTitle)
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

                _scanner.OnScanResult += ScannerResultCloseWorkorderByWorkorderNumber;
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PushAsync(_scanner);
            }

            else
            {
                await GetCloseWorkorderByWorkorderNumber();

            }

            #endregion
        }

        public async Task SearchCloseWorkorderByLocation()
        {
            #region Barcode Section and Search Section

            if (SearchButtonTitle3 == ScanTitle)
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

                _scanner.OnScanResult += ScannerResultCloseWorkorderByLocation;
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PushAsync(_scanner);
            }

            else
            {
                await GetCloseWorkorderByLocation();

            }

            #endregion
        }

        public async Task SearchCloseWorkorderByPartNumber()
        {
            #region Barcode Section and Search Section

            if (SearchButtonTitle4 == ScanTitle)
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

                _scanner.OnScanResult += ScannerResultCloseWorkorderByPartNumber;
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PushAsync(_scanner);
            }

            else
            {
                await GetCloseWorkorderByPartNumber();

            }

            #endregion
        }

        public async Task SearchCloseWorkorderByDate()
        {
            #region Barcode Section and Search Section

            {
                await GetCloseWorkorderByDate();

            }

            #endregion
        }

        private async void ScannerResultCloseWorkorderByAssetNumber(ZXing.Result result)
        {
            //Set the text property
            this.CloseWorkorderByAssetNumberText = result.Text;

            ///Pop the scanner page
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                await GetCloseWorkorderByAssetNumber();


            });

        }

        private async void ScannerResultCloseWorkorderByWorkorderNumber(ZXing.Result result)
        {
            //Set the text property
            this.CloseWorkorderByWorkorderNumberText = result.Text;

            ///Pop the scanner page
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                await GetCloseWorkorderByWorkorderNumber();


            });

        }

        private async void ScannerResultCloseWorkorderByLocation(ZXing.Result result)
        {
            //Set the text property
            this.CloseWorkorderByLocationText = result.Text;

            ///Pop the scanner page
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                await GetCloseWorkorderByLocation();


            });

        }

        private async void ScannerResultCloseWorkorderByPartNumber(ZXing.Result result)
        {
            //Set the text property
            this.CloseWorkorderByPartNumberText = result.Text;

            ///Pop the scanner page
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                await GetCloseWorkorderByPartNumber();


            });

        }




        public async Task GetCloseWorkorderByAssetNumber()
        {

            try
            {
                OperationInProgress = true;

                //var PartToSearch = new StockroomPartsSearch();
                //PartToSearch.AssetNumber = CloseWorkorderByAssetNumberText;
                //PartToSearch.PageNumber = PageNumber;
                //PartToSearch.RowspPage = RowCount;

                //var closedWorkorders = await _closeWorkorderService.GetClosedWorkOrdersByAssetNumber(PartToSearch);

                //List<ClosedWorkOrder> closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;

                // await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(closedWorkorder);
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.SearchText = CloseWorkorderByAssetNumberText;
                tnobj.SearchCriteria = "AssetNumber";
                await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(tnobj);
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

        public async Task GetCloseWorkorderByWorkorderNumber()
        {

            try
            {
                OperationInProgress = true;


                //var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByWorkOrderNumber(CloseWorkorderByWorkorderNumberText, PageNumber.ToString(), RowCount.ToString());

                //List<ClosedWorkOrder> closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;

                //await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(closedWorkorder);
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.SearchText = CloseWorkorderByWorkorderNumberText;
                tnobj.SearchCriteria = "WorkorderNumber";
                await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(tnobj);
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

        public async Task GetCloseWorkorderByLocation()
        {

            try
            {
                OperationInProgress = true;


                //var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByLocation(CloseWorkorderByLocationText,PageNumber.ToString(),RowCount.ToString());

                //List<ClosedWorkOrder> closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;

                //await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(closedWorkorder);
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.SearchText = CloseWorkorderByLocationText;
                tnobj.SearchCriteria = "LocationName";
                await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(tnobj);
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

        public async Task GetCloseWorkorderByPartNumber()
        {

            try
            {
                OperationInProgress = true;

                //var PartToSearch = new StockroomPartsSearch();
                //PartToSearch.PartNumber = CloseWorkorderByPartNumberText;
                //PartToSearch.PageNumber = PageNumber;
                //PartToSearch.RowspPage = RowCount;

                //var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByPartNumber(PartToSearch);

                //List<ClosedWorkOrder> closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;

                //await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(closedWorkorder);

                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.SearchText = CloseWorkorderByPartNumberText;
                tnobj.SearchCriteria = "PartNumber";
                await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(tnobj);
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


        public async Task GetCloseWorkorderByDate()
        {

            try
            {
                OperationInProgress = true;
                if (StartDate.Date > EndDate.Date)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Startdatecannotbegreaterthanenddate"), 2000);
                    return;

                }
                //var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByClosedWorkOrderDate(this.UserID, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"), PageNumber.ToString(), RowCount.ToString());

                //List<ClosedWorkOrder> closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;


                //await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(closedWorkorder);
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.SearchText = "";
                tnobj.Startdate = StartDate.ToString();
                tnobj.EndDate = EndDate.ToString();

                tnobj.SearchCriteria = "ClosedDate";
                await NavigationService.NavigateToAsync<ClosedWorkorderListingPageViewModel>(tnobj);
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



        public async Task ShowActions()
        {
            try
            {
                var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

                if (response == LogoutTitle)
                {
                    await _authenticationService.LogoutAsync();
                    await NavigationService.NavigateToAsync<LoginPageViewModel>();
                    await NavigationService.RemoveBackStackAsync();
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

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {

                try
                {


                    OperationInProgress = true;

                    /// here perform tasks
                    /// 





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
            catch (Exception ex)
            {
                OperationInProgress = false;
            }
            finally
            {
                OperationInProgress = false;
            }
        }

        public Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }


        #endregion

    }
}
