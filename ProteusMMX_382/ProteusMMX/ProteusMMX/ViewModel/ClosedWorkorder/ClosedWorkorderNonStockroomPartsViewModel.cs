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
    public class ClosedWorkorderNonStockroomPartsViewModel:ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected  ICloseWorkorderService _closeWorkorderService;


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
        string _createNonStockroomParts = "";
        public string CreateNonStockroomParts
        {
            get
            {
                return _createNonStockroomParts;
            }

            set
            {
                if (value != _createNonStockroomParts)
                {
                    _createNonStockroomParts = value;
                    OnPropertyChanged(nameof(_createNonStockroomParts));
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


        ObservableCollection<ClosedWorkOrderNonStockroomParts> _nonstockroomPartsCollection = new ObservableCollection<ClosedWorkOrderNonStockroomParts>();

        public ObservableCollection<ClosedWorkOrderNonStockroomParts> NonStockroomPartsCollection
        {
            get
            {
                return _nonstockroomPartsCollection;
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

                    var navigationParams = navigationData as TargetNavigationData;
                    this.ClosedWorkorderID = navigationParams.ClosedWorkorderID;




                }

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                await GetWorkorderNonStockRoomParts();

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

        public ClosedWorkorderNonStockroomPartsViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, ICloseWorkorderService closeWorkorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _closeWorkorderService = closeWorkorderService;
        }
        public ClosedWorkorderNonStockroomPartsViewModel()
        {

        }
        public async Task SetTitlesPropertiesForPage()
        {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("NonStockParts");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                PartName = WebControlTitle.GetTargetNameByTitleName("PartName");
                PartNumber = WebControlTitle.GetTargetNameByTitleName("PartNumber");
                QuantityRequired = WebControlTitle.GetTargetNameByTitleName("QuantityRequired");
                CreateNonStockroomParts = WebControlTitle.GetTargetNameByTitleName("CreateNonStockroomParts");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
        }
        public async Task ShowActions()
        {
            try
            {
                var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() {LogoutTitle });

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




        //public async Task GetWorkorderToolsAuto()
        //{
        //    // PageNumber++;
        //    await GetWorkorderTools();
        //}
        async Task GetWorkorderNonStockRoomParts()
        {
            try
            {
                if (Application.Current.Properties.ContainsKey("ClosedWorkorderID"))
                {

                    var ClosedWorkorderid = Application.Current.Properties["ClosedWorkorderID"].ToString();
                    if (ClosedWorkorderid != null)
                    {
                        this.ClosedWorkorderID = Convert.ToInt32(ClosedWorkorderid);

                    }
                }

                if (Application.Current.Properties.ContainsKey("CloseWorkorderService"))
                {
                    ICloseWorkorderService CloseWorkorderService = Application.Current.Properties["CloseWorkorderService"] as ICloseWorkorderService;
                    if (CloseWorkorderService != null)
                    {
                        _closeWorkorderService = CloseWorkorderService;

                    }
                }
                OperationInProgress = true;
                var workordersResponse = await _closeWorkorderService.GetClosedWorkOrdersNonStockroomPartsByClosedWorkorderID(this.ClosedWorkorderID.ToString());
                if (workordersResponse != null && workordersResponse.clWorkOrderWrapper != null
                    && workordersResponse.clWorkOrderWrapper.clworkOrderNonStockroomParts != null && workordersResponse.clWorkOrderWrapper.clworkOrderNonStockroomParts.Count > 0)
                {

                    var workordernonstkparts = workordersResponse.clWorkOrderWrapper.clworkOrderNonStockroomParts;
                    await AddWorkorderNonStockroomPartsInWorkorderCollection(workordernonstkparts);

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

        private async Task AddWorkorderNonStockroomPartsInWorkorderCollection(List<ClosedWorkOrderNonStockroomParts> nonstkparts)
        {
            if (nonstkparts != null && nonstkparts.Count > 0)
            {
                foreach (var item in nonstkparts)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _nonstockroomPartsCollection.Add(item);
                        OnPropertyChanged(nameof(NonStockroomPartsCollection));
                    });



                }

            }
        }


        public async Task OnViewAppearingAsync(VisualElement view)
        {
            await SetTitlesPropertiesForPage();

            NonStockroomPartsCollection.Clear();

            await GetWorkorderNonStockRoomParts();
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {

        }






        #endregion
    }
}
