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

namespace ProteusMMX.ViewModel.ClosedWorkorder
{
    public  class ClosedWorkorderToolsPageViewModel:ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly ICloseWorkorderService _closeWorkorderService;


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




        #endregion

        bool _disabledTextIsEnable = false;
        public bool DisabledTextIsEnable
        {
            get
            {
                return _disabledTextIsEnable;
            }

            set
            {
                if (value != _disabledTextIsEnable)
                {
                    _disabledTextIsEnable = value;
                    OnPropertyChanged(nameof(DisabledTextIsEnable));
                }
            }
        }


        string _disabledText = "";
        public string DisabledText
        {
            get
            {
                return _disabledText;
            }

            set
            {
                if (value != _disabledText)
                {
                    _disabledText = value;
                    OnPropertyChanged("DisabledText");
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

        string _toolName;
        public string ToolName
        {
            get
            {
                return _toolName;
            }

            set
            {
                if (value != _toolName)
                {
                    _toolName = value;
                    OnPropertyChanged("ToolName");
                }
            }
        }

        string _toolNumber;
        public string ToolNumber
        {
            get
            {
                return _toolNumber;
            }

            set
            {
                if (value != _toolNumber)
                {
                    _toolNumber = value;
                    OnPropertyChanged("ToolNumber");
                }
            }
        }

        string _toolCribName;
        public string ToolCribName
        {
            get
            {
                return _toolCribName;
            }

            set
            {
                if (value != _toolCribName)
                {
                    _toolCribName = value;
                    OnPropertyChanged("ToolCribName");
                }
            }
        }

        string _toolSize;
        public string ToolSize
        {
            get
            {
                return _toolSize;
            }

            set
            {
                if (value != _toolSize)
                {
                    _toolSize = value;
                    OnPropertyChanged("ToolSize");
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
        string _addTool = "";
        public string AddTool
        {
            get
            {
                return _addTool;
            }

            set
            {
                if (value != _addTool)
                {
                    _addTool = value;
                    OnPropertyChanged(nameof(AddTool));
                }
            }
        }

        string _toolSizeTitle = "";
        public string ToolSizeTitle
        {
            get
            {
                return _toolSizeTitle;
            }

            set
            {
                if (value != _toolSizeTitle)
                {
                    _toolSizeTitle = value;
                    OnPropertyChanged(nameof(ToolSizeTitle));
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

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

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


        ObservableCollection<ClosedWorkOrderTool> _toolsCollection = new ObservableCollection<ClosedWorkOrderTool>();

        public ObservableCollection<ClosedWorkOrderTool> ToolsCollection
        {
            get
            {
                return _toolsCollection;
            }

        }

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
               // await GetWorkorderTools();

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

        public ClosedWorkorderToolsPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, ICloseWorkorderService closeWorkorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _closeWorkorderService = closeWorkorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {

                PageTitle = WebControlTitle.GetTargetNameByTitleName("Tools");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                ToolName = WebControlTitle.GetTargetNameByTitleName("ToolName");
                ToolNumber = WebControlTitle.GetTargetNameByTitleName("ToolNumber");
                ToolCribName = WebControlTitle.GetTargetNameByTitleName("ToolCribName");
                AddTool = WebControlTitle.GetTargetNameByTitleName("AddTool");
                ToolSize = WebControlTitle.GetTargetNameByTitleName("ToolSize");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");

          
        }
        public async Task ShowActions()
        {
            try
            {
                var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() {  LogoutTitle });

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



        public async Task GetWorkorderToolsAuto()
        {
            // PageNumber++;
            await GetWorkorderTools();
        }
        async Task GetWorkorderTools()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _closeWorkorderService.GetClosedWorkOrdersToolsByClosedWorkorderID(this.ClosedWorkorderID.ToString());
                if (workordersResponse != null && workordersResponse.clWorkOrderWrapper != null
                    && workordersResponse.clWorkOrderWrapper.cltools != null && workordersResponse.clWorkOrderWrapper.cltools.Count > 0)
                {

                    var workordertools = workordersResponse.clWorkOrderWrapper.cltools;
                    await AddWorkorderToolsInWorkorderCollection(workordertools);

                }
                else
                {
                    DisabledText = "No record Found";//WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                    DisabledTextIsEnable = true;
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

        private async Task AddWorkorderToolsInWorkorderCollection(List<ClosedWorkOrderTool> tools)
        {
            if (tools != null && tools.Count > 0)
            {
                foreach (var item in tools)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _toolsCollection.Add(item);
                        OnPropertyChanged(nameof(ToolsCollection));
                    });



                }

            }
        }


        private async Task RemoveAllWorkorderToolsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ToolsCollection.Clear();
                OnPropertyChanged(nameof(ToolsCollection));
            });



        }


        public async Task OnViewAppearingAsync(VisualElement view)
        {

            await RemoveAllWorkorderToolsFromCollection();
            await GetWorkorderTools();
           
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {

        }



        #endregion
    }
}
