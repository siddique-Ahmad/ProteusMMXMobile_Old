using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Attachment;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Helpers.Validation;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.Attachments;
using ProteusMMX.Services.Workorder.Inspection;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace ProteusMMX.ViewModel.Workorder
{
    class InspectionPageViewModel : ViewModelBase
    {
        #region Fields

        public readonly IAuthenticationService _authenticationService;
        public readonly IInspectionService _inspectionService;
        public readonly IWorkorderService _workorderService;
        protected readonly IFormLoadInputService _formLoadInputService;

        string CreateWorkorderRights;
        #endregion

        #region Properties

        #region Page Properties

        


        bool _createWorkorderIsVisible = true;
        public bool CreateWorkorderIsVisible
        {
            get
            {
                return _createWorkorderIsVisible;
            }

            set
            {
                if (value != _createWorkorderIsVisible)
                {
                    _createWorkorderIsVisible = value;
                    OnPropertyChanged(nameof(CreateWorkorderIsVisible));
                }
            }
        }

        bool _createWorkorderIsEnable = true;
        public bool CreateWorkorderIsEnable
        {
            get
            {
                return _createWorkorderIsEnable;
            }

            set
            {
                if (value != _createWorkorderIsEnable)
                {
                    _createWorkorderIsEnable = value;
                    OnPropertyChanged(nameof(CreateWorkorderIsEnable));
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

        ServiceOutput _formLoadInputForWorkorder;
        public ServiceOutput FormLoadInputForWorkorder //Use For Only translation purposes
        {
            get { return _formLoadInputForWorkorder; }
            set
            {
                if (value != _formLoadInputForWorkorder)
                {
                    _formLoadInputForWorkorder = value;
                    OnPropertyChanged(nameof(FormLoadInputForWorkorder));
                }
            }
        }

        ServiceOutput _formControlsAndRights;
        public ServiceOutput FormControlsAndRights
        {
            get { return _formControlsAndRights; }
            set
            {
                if (value != _formControlsAndRights)
                {
                    _formControlsAndRights = value;
                    OnPropertyChanged(nameof(FormControlsAndRights));
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


        #region Inspection Page Properties


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



        string _serverTimeZone = AppSettings.User.ServerIANATimeZone;
        public string ServerTimeZone
        {
            get { return _serverTimeZone; }
        }


        bool? _inspectionUser = AppSettings.User.IsInspectionUser;
        public bool? InspectionUser
        {
            get { return _inspectionUser; }
        }




        bool _isDataRequested;
        public bool IsDataRequested
        {
            get { return _isDataRequested; }
            set
            {
                if (value != _isDataRequested)
                {
                    _isDataRequested = value;
                    OnPropertyChanged(nameof(IsDataRequested));
                }
            }
        }

        bool _isPickerDataSubscribed;
        public bool IsPickerDataSubscribed
        {
            get { return _isPickerDataSubscribed; }
            set
            {
                if (value != _isPickerDataSubscribed)
                {
                    _isPickerDataSubscribed = value;
                    OnPropertyChanged(nameof(IsPickerDataSubscribed));
                }
            }
        }





        #endregion


        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;

              

                if (navigationData != null)
                {

                    var navigationParams = navigationData as PageParameters;
                    var workorder = navigationParams.Parameter as workOrders;
                    this.WorkorderID = workorder.WorkOrderID;

                }

                await SetTitlesPropertiesForPage();
                if (Application.Current.Properties.ContainsKey("CreateworkorderRights"))
                {
                    CreateWorkorderRights = Application.Current.Properties["CreateworkorderRights"].ToString();
                    if (CreateWorkorderRights == "E")
                    {
                        CreateWorkorderIsVisible = true;
                    }
                    else if (CreateWorkorderRights == "V")
                    {

                        CreateWorkorderIsEnable = false;

                    }
                    else
                    {
                        CreateWorkorderIsVisible = false;
                    }

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

        public InspectionPageViewModel(IAuthenticationService authenticationService, IInspectionService inspectionService, IWorkorderService workorderService,IFormLoadInputService formLoadInputService)
        {
            _authenticationService = authenticationService;
            _inspectionService = inspectionService;
            _workorderService = workorderService;
            _formLoadInputService = formLoadInputService;

        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {

                {
                    PageTitle = WebControlTitle.GetTargetNameByTitleName("Attachments");
                    WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                    LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                    CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                    SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                    //SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName(titles, "SelectOptionsTitles"); 

                    SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");







                }
            }
            catch (Exception ex)
            {


            }

            finally
            {

            }
        }


        public async Task ShowActions()
        {
            try
            {
                var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

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
                OperationInProgress = true;

                if (IsDataRequested)
                {
                    IsDataRequested = false;
                    return;
                }

                try
                {
                 
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
