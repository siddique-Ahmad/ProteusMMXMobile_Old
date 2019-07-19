using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.SelectionListPageServices;
using ProteusMMX.Services.SelectionListPageServices.Asset;
using ProteusMMX.Utils;
using ProteusMMX.Views.ClosedWorkorder;
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
    class ClosedWorkorderTabbedPageViewModel : ViewModelBase
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;
        protected readonly IFormLoadInputService _formLoadInputService;
        protected readonly INavigationService _navigationService;

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


   
        string _workOrderNumberTitle;
        public string WorkOrderNumberTitle
        {
            get
            {
                return _workOrderNumberTitle;
            }

            set
            {
                if (value != _workOrderNumberTitle)
                {
                    _workOrderNumberTitle = value;
                    OnPropertyChanged("WorkOrderNumberTitle");
                }
            }
        }


        string _descriptionTitle;
        public string DescriptionTitle
        {
            get
            {
                return _descriptionTitle;
            }

            set
            {
                if (value != _descriptionTitle)
                {
                    _descriptionTitle = value;
                    OnPropertyChanged("DescriptionTitle");
                }
            }
        }


        string _workOrderTypeTitle;
        public string WorkOrderTypeTitle
        {
            get
            {
                return _workOrderTypeTitle;
            }

            set
            {
                if (value != _workOrderTypeTitle)
                {
                    _workOrderTypeTitle = value;
                    OnPropertyChanged("WorkOrderTypeTitle");
                }
            }
        }


        string _targetNameTitle;
        public string TargetNameTitle
        {
            get
            {
                return _targetNameTitle;
            }

            set
            {
                if (value != _targetNameTitle)
                {
                    _targetNameTitle = value;
                    OnPropertyChanged("TargetNameTitle");
                }
            }
        }



        #endregion

      

        #endregion

        #region Commands
     
    


        #endregion

        #region Methods

        

    
 
        public ClosedWorkorderTabbedPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _navigationService = navigationService;
        }
        //public override async Task InitializeAsync(object navigationData)
        //{
        //    try
        //    {

        //        var closeWorkorder = navigationData as ClosedWorkOrder;
        //        this.ClosedWorkorderID = closeWorkorder.ClosedWorkOrderID;

        //        //var tabbedPage = this.Page as TabbedPage;

        //        //#region closedWorkorderDetailsPage
        //        //var closedWorkorderDetailsPage = new ClosedWorkorderDetailsPage();
        //        //var closedWorkorderDetailsPageViewModel = Locator.Instance.Resolve<ClosedWorkorderDetailsPageViewModel>() as ClosedWorkorderDetailsPageViewModel;
        //        //closedWorkorderDetailsPageViewModel.ClosedWorkorderID = this.ClosedWorkorderID;
        //        //// closedWorkorderDetailsPage.BindingContext = closedWorkorderDetailsPageViewModel;
        //        //await (closedWorkorderDetailsPage.BindingContext as ViewModelBase).InitializeAsync(closedWorkorderDetailsPageViewModel);



        //        //#endregion

        //        //#region closedWorkorderTaskAndLabourPage
        //        //var closedWorkorderTaskAndLabourPage = new ClosedWorkorderTaskAndLabourPage();
        //        //var closedWorkorderTaskAndLabourPageViewModel = Locator.Instance.Resolve<ClosedWorkorderTaskAndLabourPageViewModel>();
        //        //closedWorkorderTaskAndLabourPageViewModel.ClosedWorkorderID = this.ClosedWorkorderID;
        //        //closedWorkorderTaskAndLabourPage.BindingContext = closedWorkorderTaskAndLabourPageViewModel;

        //        //#endregion


        //        //tabbedPage.Children.Add(closedWorkorderDetailsPage);
        //        //tabbedPage.Children.Add(closedWorkorderTaskAndLabourPage);

        //        // in tabbed page children add other page with setting its data context 

        //        if (ConnectivityService.IsConnected == false)
        //        {
        //            await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
        //            return;

        //        }
        //        OperationInProgress = true;






        //        await SetTitlesPropertiesForPage();


        //    }
        //    catch (Exception ex)
        //    {
        //        OperationInProgress = false;

        //    }

        //    finally
        //    {
        //        OperationInProgress = false;
        //    }
        //}

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

                //if (ConnectivityService.IsConnected == false)
                //{
                //    await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
                //    return;

                //}

                // PoDetailPageTitle = WebControlTitle.GetTargetNameByTitleName("PurchaseOrderDetails");


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

            //var titles = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
            //if (titles != null && titles.CFLI.Count > 0 && titles.listWebControlTitles.Count > 0)
            {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("WorkOrders");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName("TotalRecords");
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchscanAssetforAttachments");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");


                WorkOrderNumberTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderNumber");
                DescriptionTitle = WebControlTitle.GetTargetNameByTitleName("Description");
                WorkOrderTypeTitle = WebControlTitle.GetTargetNameByTitleName("WorkOrderType");
                TargetNameTitle = WebControlTitle.GetTargetNameByTitleName("Target") + " " + WebControlTitle.GetTargetNameByTitleName("Name");


            }
        }
 
       
        #endregion

    }
}
