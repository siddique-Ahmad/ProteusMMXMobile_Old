using ProteusMMX.Helpers;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.CloseWorkorder;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
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

namespace ProteusMMX.ViewModel.ClosedWorkorder
{
    public class ClosedWorkorderStockroomPartsViewModelForIOS : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly ICloseWorkorderService _closeWorkorderService;


        #endregion

        #region Properties

        #region Page Properties
        string _getNonStockroomParts = "";
        public string GetNonStockroomParts
        {
            get
            {
                return _getNonStockroomParts;
            }

            set
            {
                if (value != _getNonStockroomParts)
                {
                    _getNonStockroomParts = value;
                    OnPropertyChanged(nameof(GetNonStockroomParts));
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

                }
            }
        }
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

        Page _page;
        public Page Page
        {
            get
            {
                return _page;
            }

            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));
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

        string _partName;
        public string PartName
        {
            get
            {
                return _partName;
            }

            set
            {
                if (value != _partName)
                {
                    _partName = value;
                    OnPropertyChanged("PartName");
                }
            }
        }

        string _partNumber;
        public string PartNumber
        {
            get
            {
                return _partNumber;
            }

            set
            {
                if (value != _partNumber)
                {
                    _partNumber = value;
                    OnPropertyChanged("PartNumber");
                }
            }
        }

        string _quantityRequired;
        public string QuantityRequired
        {
            get
            {
                return _quantityRequired;
            }

            set
            {
                if (value != _quantityRequired)
                {
                    _quantityRequired = value;
                    OnPropertyChanged("QuantityRequired");
                }
            }
        }

        string _quantityAllocated;
        public string QuantityAllocated
        {
            get
            {
                return _quantityAllocated;
            }

            set
            {
                if (value != _quantityAllocated)
                {
                    _quantityAllocated = value;
                    OnPropertyChanged("QuantityAllocated");
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

        string _addStockroompartTitle = "";
        public string AddStockroompartTitle
        {
            get
            {
                return _addStockroompartTitle;
            }

            set
            {
                if (value != _addStockroompartTitle)
                {
                    _addStockroompartTitle = value;
                    OnPropertyChanged("AddStockroompartTitle");
                }
            }
        }
        string _nonStockPartsTitle = "";
        public string NonStockPartsTitle
        {
            get
            {
                return _nonStockPartsTitle;
            }

            set
            {
                if (value != _nonStockPartsTitle)
                {
                    _nonStockPartsTitle = value;
                    OnPropertyChanged("NonStockPartsTitle");
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

        string _selectOptionsTitle;
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
        int? _closedWorkorderID;
        public int? ClosedWorkorderID
        {
            get
            {
                return _closedWorkorderID;
            }

            set
            {
                if (value != _closedWorkorderID)
                {
                    _closedWorkorderID = value;
                    OnPropertyChanged(nameof(ClosedWorkorderID));
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


        ObservableCollection<ClosedWorkOrderStockroomPart> _stockroomPartsCollection = new ObservableCollection<ClosedWorkOrderStockroomPart>();

        public ObservableCollection<ClosedWorkOrderStockroomPart> StockroomPartsCollection
        {
            get
            {
                return _stockroomPartsCollection;
            }

        }

        #endregion
        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);






        #endregion
        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

                //if (ConnectivityService.IsConnected == false)
                //{
                //    await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
                //    return;

                //}

                if (navigationData != null)
                {

                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                    var workorder = navigationParams.Parameter as ClosedWorkOrder;
                    this.ClosedWorkorderID = workorder.ClosedWorkOrderID;


                }

                OperationInProgress = true;
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

        public ClosedWorkorderStockroomPartsViewModelForIOS(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, ICloseWorkorderService closeWorkorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _closeWorkorderService = closeWorkorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            
                PageTitle = WebControlTitle.GetTargetNameByTitleName("Parts");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                PartName = WebControlTitle.GetTargetNameByTitleName("PartName");
                PartNumber = WebControlTitle.GetTargetNameByTitleName("PartNumber");
                QuantityRequired = WebControlTitle.GetTargetNameByTitleName("QuantityRequired");
                QuantityAllocated = WebControlTitle.GetTargetNameByTitleName("QuantityAllocated");
                AddStockroompartTitle = WebControlTitle.GetTargetNameByTitleName("Addstockroompart");
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchOrScanpartsNo");
                NonStockPartsTitle = WebControlTitle.GetTargetNameByTitleName("NonStockParts");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                GetNonStockroomParts = WebControlTitle.GetTargetNameByTitleName("NonStockParts");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
        }
        public async Task ShowActions()
        {
            try
            {
                var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { GetNonStockroomParts, LogoutTitle });

                if (response == LogoutTitle)
                {
                    await _authenticationService.LogoutAsync();
                    await NavigationService.NavigateToAsync<LoginPageViewModel>();
                    await NavigationService.RemoveBackStackAsync();
                }
                if (response == GetNonStockroomParts)
                {
                    TargetNavigationData tnobj = new TargetNavigationData();
                    tnobj.ClosedWorkorderID = this.ClosedWorkorderID;
                    await NavigationService.NavigateToAsync<ClosedWorkorderNonStockroomPartsViewModel>(tnobj);

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

        async Task GetWorkorderStockRoomParts()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _closeWorkorderService.GetClosedWorkOrdersStockroomPartsByClosedWorkorderID(this.ClosedWorkorderID.ToString());
                if (workordersResponse != null && workordersResponse.clWorkOrderWrapper != null
                    && workordersResponse.clWorkOrderWrapper.clworkOrderStockroomParts != null && workordersResponse.clWorkOrderWrapper.clworkOrderStockroomParts.Count > 0)
                {

                    var workorderstkparts = workordersResponse.clWorkOrderWrapper.clworkOrderStockroomParts;
                    await AddWorkorderStockroomPartsInWorkorderCollection(workorderstkparts);

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



        private async Task AddWorkorderStockroomPartsInWorkorderCollection(List<ClosedWorkOrderStockroomPart> stkparts)
        {
            if (stkparts != null && stkparts.Count > 0)
            {
                foreach (var item in stkparts)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _stockroomPartsCollection.Add(item);
                        OnPropertyChanged(nameof(StockroomPartsCollection));
                    });



                }

            }
        }








        private async Task RefillPartsCollection()
        {
            PageNumber = 1;
            await RemoveAllPartsFromCollection();
            await GetWorkorderStockRoomParts();
        }



        private async Task RemoveAllPartsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                StockroomPartsCollection.Clear();
                OnPropertyChanged(nameof(StockroomPartsCollection));
            });



        }

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await RemoveAllPartsFromCollection();
                await GetWorkorderStockRoomParts();
            }

        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {

        }
        #endregion
    }
}
