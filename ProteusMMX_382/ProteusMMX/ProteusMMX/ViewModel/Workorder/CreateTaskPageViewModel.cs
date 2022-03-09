using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.Controls;
using ProteusMMX.Converters;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Helpers.Validation;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.SelectionListPageServices.Workorder.TaskAndLabour;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.TaskAndLabour;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProteusMMX.ViewModel.Workorder
{

    public class CreateTaskPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;

        protected readonly ITaskAndLabourService _taskAndLabourService;

        protected readonly ITaskService _taskService;
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
        string _saveTitle;
        public string SaveTitle
        {
            get
            {
                return _saveTitle;
            }

            set
            {
                if (value != _saveTitle)
                {
                    _saveTitle = value;
                    OnPropertyChanged(nameof(SaveTitle));
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



        #region Create Task Page Properties


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



        string _addTaskTitle;
        public string AddTaskTitle
        {
            get
            {
                return _addTaskTitle;
            }

            set
            {
                if (value != _addTaskTitle)
                {
                    _addTaskTitle = value;
                    OnPropertyChanged(nameof(AddTaskTitle));
                }
            }
        }


        bool _isPickerDataRequested;
        public bool IsPickerDataRequested
        {
            get { return _isPickerDataRequested; }
            set
            {
                if (value != _isPickerDataRequested)
                {
                    _isPickerDataRequested = value;
                    OnPropertyChanged(nameof(IsPickerDataRequested));
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


        #region Normal Field Properties

        // Task
        int? _taskID;
        public int? TaskID
        {
            get
            {
                return _taskID;
            }

            set
            {
                if (value != _taskID)
                {
                    _taskID = value;
                    OnPropertyChanged(nameof(TaskID));
                }
            }
        }

        string _taskName;
        public string TaskName
        {
            get
            {
                return _taskName;
            }

            set
            {
                if (value != _taskName)
                {
                    _taskName = value;
                    OnPropertyChanged(nameof(TaskName));
                }
            }
        }

        string _taskTitle;
        public string TaskTitle
        {
            get
            {
                return _taskTitle;
            }

            set
            {
                if (value != _taskTitle)
                {
                    _taskTitle = value;
                    OnPropertyChanged(nameof(TaskTitle));
                }
            }
        }

        bool _taskIsEnable = true;
        public bool TaskIsEnable
        {
            get
            {
                return _taskIsEnable;
            }

            set
            {
                if (value != _taskIsEnable)
                {
                    _taskIsEnable = value;
                    OnPropertyChanged(nameof(TaskIsEnable));
                }
            }
        }



        //Description
        string _descriptionText;
        public string DescriptionText
        {
            get
            {
                return _descriptionText;
            }

            set
            {
                if (value != _descriptionText)
                {
                    _descriptionText = value;
                    OnPropertyChanged(nameof(DescriptionText));
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
                    OnPropertyChanged(nameof(DescriptionTitle));
                }
            }
        }

        bool _descriptionIsEnable = false;
        public bool DescriptionIsEnable
        {
            get
            {
                return _descriptionIsEnable;
            }

            set
            {
                if (value != _descriptionIsEnable)
                {
                    _descriptionIsEnable = value;
                    OnPropertyChanged(nameof(DescriptionIsEnable));
                }
            }
        }




        //Employee
        int? _employeeID;
        public int? EmployeeID
        {
            get
            {
                return _employeeID;
            }

            set
            {
                if (value != _employeeID)
                {
                    _employeeID = value;
                    OnPropertyChanged(nameof(EmployeeID));
                }
            }
        }

        string _employeeName;
        public string EmployeeName
        {
            get
            {
                return _employeeName;
            }

            set
            {
                if (value != _employeeName)
                {
                    _employeeName = value;
                    OnPropertyChanged(nameof(EmployeeName));
                }
            }
        }

        string _employeeTitle;
        public string EmployeeTitle
        {
            get
            {
                return _employeeTitle;
            }

            set
            {
                if (value != _employeeTitle)
                {
                    _employeeTitle = value;
                    OnPropertyChanged(nameof(EmployeeTitle));
                }
            }
        }

        bool _employeeIsEnable = true;
        public bool EmployeeIsEnable
        {
            get
            {
                return _employeeIsEnable;
            }

            set
            {
                if (value != _employeeIsEnable)
                {
                    _employeeIsEnable = value;
                    OnPropertyChanged(nameof(EmployeeIsEnable));
                }
            }
        }


        //Contractor
        int? _contractorID;
        public int? ContractorID
        {
            get
            {
                return _contractorID;
            }

            set
            {
                if (value != _contractorID)
                {
                    _contractorID = value;
                    OnPropertyChanged(nameof(ContractorID));
                }
            }
        }

        string _contractorName;
        public string ContractorName
        {
            get
            {
                return _contractorName;
            }

            set
            {
                if (value != _contractorName)
                {
                    _contractorName = value;
                    OnPropertyChanged(nameof(ContractorName));
                }
            }
        }

        string _contractorTitle;
        public string ContractorTitle
        {
            get
            {
                return _contractorTitle;
            }

            set
            {
                if (value != _contractorTitle)
                {
                    _contractorTitle = value;
                    OnPropertyChanged(nameof(ContractorTitle));
                }
            }
        }

        bool _contractorIsEnable = true;
        public bool ContractorIsEnable
        {
            get
            {
                return _contractorIsEnable;
            }

            set
            {
                if (value != _contractorIsEnable)
                {
                    _contractorIsEnable = value;
                    OnPropertyChanged(nameof(ContractorIsEnable));
                }
            }
        }
        bool _contractorIsVisible = true;
        public bool ContractorIsVisible
        {
            get
            {
                return _contractorIsVisible;
            }

            set
            {
                if (value != _contractorIsVisible)
                {
                    _contractorIsVisible = value;
                    OnPropertyChanged(nameof(ContractorIsVisible));
                }
            }
        }
        bool _employeeIsVisible = true;
        public bool EmployeeIsVisible
        {
            get
            {
                return _employeeIsVisible;
            }

            set
            {
                if (value != _employeeIsVisible)
                {
                    _employeeIsVisible = value;
                    OnPropertyChanged(nameof(EmployeeIsVisible));
                }
            }
        }
        bool _taskIsVisible = true;
        public bool TaskIsVisible
        {
            get
            {
                return _taskIsVisible;
            }

            set
            {
                if (value != _taskIsVisible)
                {
                    _taskIsVisible = value;
                    OnPropertyChanged(nameof(TaskIsVisible));
                }
            }
        }
        



        // Task started Date
        DateTime? _taskStartedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
        public DateTime? TaskStartedDate
        {
            get
            {
                return _taskStartedDate;
            }

            set
            {
                if (value != _taskStartedDate)
                {
                    _taskStartedDate = value;
                    OnPropertyChanged(nameof(TaskStartedDate));
                }
            }
        }

        DateTime? _minimumTaskStartedDate;
        public DateTime? MinimumTaskStartedDate
        {
            get
            {
                return _minimumTaskStartedDate;
            }

            set
            {
                if (value != _minimumTaskStartedDate)
                {
                    _minimumTaskStartedDate = value;
                    OnPropertyChanged(nameof(MinimumTaskStartedDate));
                }
            }
        }

        DateTime? _maximumTaskStartedDate= DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
        public DateTime? MaximumTaskStartedDate
        {
            get
            {
                return _maximumTaskStartedDate;
            }

            set
            {
                if (value != _maximumTaskStartedDate)
                {
                    _maximumTaskStartedDate = value;
                    OnPropertyChanged(nameof(MaximumTaskStartedDate));
                }
            }
        }

        string _taskStartedDateTitle;
        public string TaskStartedDateTitle
        {
            get
            {
                return _taskStartedDateTitle;
            }

            set
            {
                if (value != _taskStartedDateTitle)
                {
                    _taskStartedDateTitle = value;
                    OnPropertyChanged(nameof(TaskStartedDateTitle));
                }
            }
        }


        string _taskStartedDateWarningText;
        public string TaskStartedDateWarningText
        {
            get
            {
                return _taskStartedDateWarningText;
            }

            set
            {
                if (value != _taskStartedDateWarningText)
                {
                    _taskStartedDateWarningText = value;
                    OnPropertyChanged(nameof(TaskStartedDateWarningText));
                }
            }
        }


        bool _taskStartedDateIsEnable = true;
        public bool TaskStartedDateIsEnable
        {
            get
            {
                return _taskStartedDateIsEnable;
            }

            set
            {
                if (value != _taskStartedDateIsEnable)
                {
                    _taskStartedDateIsEnable = value;
                    OnPropertyChanged(nameof(TaskStartedDateIsEnable));
                }
            }
        }

        bool _taskStartedDateIsVisible = true;
        public bool TaskStartedDateIsVisible
        {
            get
            {
                return _taskStartedDateIsVisible;
            }

            set
            {
                if (value != _taskStartedDateIsVisible)
                {
                    _taskStartedDateIsVisible = value;
                    OnPropertyChanged(nameof(TaskStartedDateIsVisible));
                }
            }
        }
        bool _taskCompletionDateIsVisible = true;
        public bool TaskCompletionDateIsVisible
        {
            get
            {
                return _taskCompletionDateIsVisible;
            }

            set
            {
                if (value != _taskStartedDateIsVisible)
                {
                    _taskCompletionDateIsVisible = value;
                    OnPropertyChanged(nameof(TaskCompletionDateIsVisible));
                }
            }
        }

        // Task Completion Date
        DateTime? _taskCompletionDate;
        public DateTime? TaskCompletionDate
        {
            get
            {
                return _taskCompletionDate;
            }

            set
            {
                if (value != _taskCompletionDate)
                {
                    _taskCompletionDate = value;
                    OnPropertyChanged(nameof(TaskCompletionDate));
                }
            }
        }

        DateTime? _minimumTaskCompletionDate;
        public DateTime? MinimumTaskCompletionDate
        {
            get
            {
                return _minimumTaskCompletionDate;
            }

            set
            {
                if (value != _minimumTaskCompletionDate)
                {
                    _minimumTaskCompletionDate = value;
                    OnPropertyChanged(nameof(MinimumTaskCompletionDate));
                }
            }
        }

        DateTime? _maximumTaskCompletionDate= DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
        public DateTime? MaximumTaskCompletionDate
        {
            get
            {
                return _maximumTaskCompletionDate;
            }

            set
            {
                if (value != _maximumTaskCompletionDate)
                {
                    _maximumTaskCompletionDate = value;
                    OnPropertyChanged(nameof(MaximumTaskCompletionDate));
                }
            }
        }

        string _taskCompletionDateTitle;
        public string TaskCompletionDateTitle
        {
            get
            {
                return _taskCompletionDateTitle;
            }

            set
            {
                if (value != _taskCompletionDateTitle)
                {
                    _taskCompletionDateTitle = value;
                    OnPropertyChanged(nameof(TaskCompletionDateTitle));
                }
            }
        }


        string _taskCompletionDateWarningText;
        public string TaskCompletionDateWarningText
        {
            get
            {
                return _taskCompletionDateWarningText;
            }

            set
            {
                if (value != _taskCompletionDateWarningText)
                {
                    _taskCompletionDateWarningText = value;
                    OnPropertyChanged(nameof(TaskCompletionDateWarningText));
                }
            }
        }

        bool _taskCompletionDateIsEnable = true;
        public bool TaskCompletionDateIsEnable
        {
            get
            {
                return _taskCompletionDateIsEnable;
            }

            set
            {
                if (value != _taskCompletionDateIsEnable)
                {
                    _taskCompletionDateIsEnable = value;
                    OnPropertyChanged(nameof(TaskCompletionDateIsEnable));
                }
            }
        } 
        #endregion

        #endregion





        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand TaskCommand => new AsyncCommand(ShowTask);
        public ICommand EmployeeCommand => new AsyncCommand(ShowEmployee);
        public ICommand ContractorCommand => new AsyncCommand(ShowContractor);
        public ICommand SaveCommand => new AsyncCommand(CreateTask);

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

                    this.WorkorderID = Int32.Parse(navigationParams.Parameter.ToString());



                }
                await SetTitlesPropertiesForPage();
                OperationInProgress = false;

                    if (Application.Current.Properties.ContainsKey("TaskTabDetails"))
                    {
                        var TaskTabDetails = Application.Current.Properties["TaskTabDetails"].ToString();
                        if (TaskTabDetails != null && TaskTabDetails == "E")
                        {
                            this.TaskIsVisible = true;
                        }
                        else if (TaskTabDetails == "V")
                        {
                            this.TaskIsEnable = false;
                        }
                        else
                        {
                            this.TaskIsVisible = true;
                        }


                    }
                    if (Application.Current.Properties.ContainsKey("EmployeeTab"))
                    {
                        var EmployeeTab = Application.Current.Properties["EmployeeTab"].ToString();
                        if (EmployeeTab != null && EmployeeTab == "E")
                        {
                            this.EmployeeIsVisible = true;
                        }
                        else if (EmployeeTab == "V")
                        {
                            this.EmployeeIsEnable = false;
                        }
                        else
                        {
                            this.EmployeeIsVisible = false;
                        }


                    }
                    if (Application.Current.Properties.ContainsKey("ContractorTab"))
                    {
                        var ContractorTab = Application.Current.Properties["ContractorTab"].ToString();
                        if (ContractorTab != null && ContractorTab == "E")
                        {
                            this.ContractorIsVisible = true;
                        }
                        else if (ContractorTab == "V")
                        {
                            this.ContractorIsEnable = false;
                        }
                        else
                        {
                            this.ContractorIsVisible = false;
                        }


                    }

                    if (Application.Current.Properties.ContainsKey("StartdateTab"))
                    {
                        var StartdateTab = Application.Current.Properties["StartdateTab"].ToString();
                        if (StartdateTab != null && StartdateTab == "E")
                        {
                            this.TaskStartedDateIsVisible = true;
                        }
                        else if (StartdateTab == "V")
                        {
                            this.TaskStartedDateIsEnable = false;
                        }
                        else
                        {
                            this.TaskStartedDateIsVisible = false;
                        }


                    }

                    if (Application.Current.Properties.ContainsKey("CompletionDateTab"))
                    {
                        var CompletionDateTab = Application.Current.Properties["CompletionDateTab"].ToString();
                        if (CompletionDateTab != null && CompletionDateTab == "E")
                        {
                            this.TaskCompletionDateIsVisible = true;
                        }
                        else if (CompletionDateTab == "V")
                        {
                            this.TaskCompletionDateIsEnable = false;
                        }
                        else
                        {
                            this.TaskCompletionDateIsVisible = false;
                        }


                    }


                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                    {
                        this.ContractorIsVisible = false;

                    }
                

                                  
                ServiceOutput taskResponse = null;

                taskResponse = await _taskService.GetEmployee(UserID,"0","0", AppSettings.User.EmployeeName, "TaskAndLabor",this.WorkorderID);

                if (taskResponse != null && taskResponse.workOrderWrapper != null && taskResponse.workOrderWrapper.workOrderLabor != null && taskResponse.workOrderWrapper.workOrderLabor.Employees != null && taskResponse.workOrderWrapper.workOrderLabor.Employees.Count > 0)
                {
                    var assingedToEmployees = taskResponse.workOrderWrapper.workOrderLabor.Employees;
                    if (assingedToEmployees != null)
                    {
                        this.EmployeeID = assingedToEmployees.First().EmployeeLaborCraftID;
                        this.EmployeeName = ShortString.shorten(assingedToEmployees.First().EmployeeName)+"("+ assingedToEmployees.First().LaborCraftCode+")";
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

        public CreateTaskPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService , ITaskAndLabourService taskAndLabourService, ITaskService taskService )
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
            _taskAndLabourService = taskAndLabourService;
            _taskService = taskService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {

                //var titles = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                //if (titles != null && titles.listWebControlTitles != null && titles.listWebControlTitles.Count > 0)
                {
                    PageTitle = WebControlTitle.GetTargetNameByTitleName("AddTask");
                    WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                    LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                    CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                    SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                    //SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName(titles, "SelectOptionsTitles"); 
                    SaveTitle = WebControlTitle.GetTargetNameByTitleName("Save");


                    AddTaskTitle = WebControlTitle.GetTargetNameByTitleName("AddTask");


                    TaskTitle = WebControlTitle.GetTargetNameByTitleName("Task");
                    DescriptionTitle = WebControlTitle.GetTargetNameByTitleName("Description");
                    EmployeeTitle = WebControlTitle.GetTargetNameByTitleName("Employee");
                    ContractorTitle = WebControlTitle.GetTargetNameByTitleName("Contractor");
                    TaskStartedDateTitle = WebControlTitle.GetTargetNameByTitleName("StartDate");
                    TaskCompletionDateTitle = WebControlTitle.GetTargetNameByTitleName("CompletionDate");

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





        #region Show Selection List Pages Methods


        public async Task ShowTask()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.WORKORDERID = this.WorkorderID;
                tnobj.Type = "TaskAndLabor";
                await NavigationService.NavigateToAsync<TaskListSelectionPageViewModel>(tnobj); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;

            }
        }

        public async Task ShowEmployee()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;
                IsPickerDataRequested = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.WORKORDERID = this.WorkorderID;
                tnobj.Type = "TaskAndLabor";
                await NavigationService.NavigateToAsync<EmployeeListSelectionPageViewModel>(tnobj); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();
                //OperationInProgress = false;

            }
        }

        public async Task ShowContractor()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.WORKORDERID = this.WorkorderID;
                tnobj.Type = "TaskAndLabor";
                await NavigationService.NavigateToAsync<ContractorListSelectionPageViewModel>(tnobj); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

            }
        }

        public async Task CreateTask()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

               // OperationInProgress = true;

            



                ///TODO: Get Workorder data 
                var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());


                if (this.TaskStartedDate == null)
                {
                    UserDialogs.Instance.HideLoading();

                    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Pleasefillthestartdate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasefillthestartdate"), 2000);
                    return;
                }
                if (EmployeeID == null && ContractorID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "PleaseselecteitheremployeeOrcontractor").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleaseselecteitheremployeeOrcontractor"), 2000);
                    return;
                }
                if (EmployeeID == null && ContractorID == null && TaskID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "PleaseselectanyonefromthetaskemployeeOrcontractor").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleaseselectanyonefromthetaskemployeeOrcontractor"), 2000);
                    return;
                }


                if (workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate != null)
                {
                    var workorderStartedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                    var startPickerDate = DateTime.Parse(TaskStartedDate.Value.ToString("d"));

                    if (startPickerDate < DateTime.Parse(workorderStartedDate.ToString("d")))
                    {
                        UserDialogs.Instance.HideLoading();

                        // await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Startdatecannotbelessthanworkorderstartdate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Startdatecannotbelessthanworkorderstartdate"), 2000);
                        return;
                    }

                    if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null)
                    {
                        var workorderComletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                        if (startPickerDate > DateTime.Parse(workorderComletionDate.ToString("d")))
                        {
                            UserDialogs.Instance.HideLoading();

                            //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Startdatecannotbegreaterthanworkordercompletiondate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Startdatecannotbegreaterthanworkordercompletiondate"), 2000);
                            return;
                        }
                    }

                }

                if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null)
                {
                    var startPickerDate = DateTime.Parse(TaskStartedDate.Value.ToString("d"));
                    var workorderComletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                    if (startPickerDate > DateTime.Parse(workorderComletionDate.ToString("d")))
                    {
                        UserDialogs.Instance.HideLoading();

                        //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Startdatecannotbegreaterthanworkordercompletiondate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Startdatecannotbegreaterthanworkordercompletiondate"), 2000);
                        return;
                    }
                }


                if (TaskCompletionDate != null && workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null)
                {
                    var workorderComletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                    var completePickerDate = DateTime.Parse(TaskCompletionDate.Value.ToString("d"));
                    var startPickerDate = DateTime.Parse(TaskStartedDate.Value.ToString("d"));

                    if (completePickerDate < startPickerDate)
                    {
                        UserDialogs.Instance.HideLoading();

                        //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Completiondatecannotbelessthanstartdate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Completiondatecannotbelessthanstartdate"), 2000);
                        return;
                    }
                  
                    if (!Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor))
                    {
                        if (completePickerDate > DateTime.Parse(workorderComletionDate.ToString("d")))
                        {
                            UserDialogs.Instance.HideLoading();

                            //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Completiondatecannotbegreaterthanworkordercompletiondate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Completiondatecannotbegreaterthanworkordercompletiondate"), 2000);
                            return;
                        }
                    }
                }

                else if (TaskCompletionDate != null && workorderWrapper.workOrderWrapper.workOrder.CompletionDate == null)
                {

                    var completePickerDate = DateTime.Parse(TaskCompletionDate.Value.ToString("d"));
                    var startPickerDate = DateTime.Parse(TaskStartedDate.Value.ToString("d"));

                    if (completePickerDate < startPickerDate)
                    {
                        UserDialogs.Instance.HideLoading();

                        //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Completiondatecannotbelessthanstartdate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Completiondatecannotbelessthanstartdate"), 2000);
                        return;
                    }

                }


                var workorder = new workOrderWrapper
                {
                    TimeZone = AppSettings.UserTimeZone,
                    CultureName = AppSettings.UserCultureName,
                    UserId = Convert.ToInt32(UserID),
                    ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                    workOrderLabor = new WorkOrderLabor
                    {
                        ModifiedUserName=AppSettings.User.UserName,
                        WorkOrderID = WorkorderID,
                        TaskID = TaskID,
                        EmployeeLaborCraftID = EmployeeID,
                        ContractorLaborCraftID = ContractorID,
                        StartDate = TaskStartedDate.HasValue ? TaskStartedDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null, //TaskStartedDate.Date.Add(DateTime.Now.TimeOfDay),
                        CompletionDate = TaskCompletionDate.HasValue ? TaskCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null, //(ShowCompletionDate.IsVisible == true) ? (DateTime?)null : CompletionDate1.Date.Add(DateTime.Now.TimeOfDay),
                    }

                };


                var response = await _taskAndLabourService.CreateWorkOrderLabor(workorder);
                if (response != null && bool.Parse(response.servicestatus))
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Task&laboursuccessfullyadded"), 2000);
                    await NavigationService.NavigateBackAsync();
                }
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;



            }
            catch (Exception ex)
            {                    UserDialogs.Instance.HideLoading();

                Console.WriteLine(ex.Message + "<<<<<<<<<<<<<<<<>>>>>>>>>>>>" + ex.InnerException + "<<<<<<<<>>>>>>>>>" + ex.StackTrace);
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

            }
        }

        #endregion


        #region MessagingService Callback Methods





        private void OnTaskRequested(object obj)
        {

            if (obj != null)
            {
                var task = obj as TaskLookUp;
                this.TaskID = task.TaskID;
                this.TaskName = ShortString.shorten(task.TaskNumber);


                this.DescriptionText = RemoveHTML.StripHtmlTags(task.Description);

            }


        }

        private void OnEmployeeRequested(object obj)
        {

            if (obj != null)
            {

                var employee = obj as EmployeeLookUp;
                if(String.IsNullOrWhiteSpace(employee.EmployeeName))
                {
                    this.EmployeeID = null;
                    this.EmployeeName = "";
                    ResetContractor();
                    return;
                }
                this.EmployeeID = employee.EmployeeLaborCraftID;
               // this.EmployeeName = ShortString.shorten(employee.EmployeeName);
                this.EmployeeName = ShortString.shorten(employee.EmployeeName) + "(" + employee.LaborCraftCode + ")";

               

            }


        }

        private void OnContractorRequested(object obj)
        {

            if (obj != null)
            {

                var contractor = obj as ContractorLookUp;
                if (String.IsNullOrWhiteSpace(contractor.ContractorName))
                {
                    this.ContractorID = null;
                    this.ContractorName = "";
                    ResetEmployee();
                    return;
                }
                this.ContractorID = contractor.ContractorLaborCraftID;
                //this.ContractorName = ShortString.shorten(contractor.ContractorName);
                this.ContractorName = ShortString.shorten(contractor.ContractorName) + "(" + contractor.LaborCraftCode + ")";

                ResetEmployee();

            }


        }


        private void ResetEmployee()
        {
            this.EmployeeID = null;
            this.EmployeeName = string.Empty;
        }

        private void ResetContractor()
        {
            this.ContractorID = null;
            this.ContractorName = string.Empty;
        }



        #endregion

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {

                try
                {


                    OperationInProgress = true;

                    if (!IsPickerDataSubscribed)
                    {
                        //Retrive Task
                        MessagingCenter.Subscribe<object>(this, MessengerKeys.TaskRequested, OnTaskRequested);

                        //Retrive Emloyee
                        MessagingCenter.Subscribe<object>(this, MessengerKeys.EmployeeRequested_AddTask, OnEmployeeRequested);

                        //Retrive Contractor
                        MessagingCenter.Subscribe<object>(this, MessengerKeys.ContractorRequested_AddTask, OnContractorRequested);


                        IsPickerDataSubscribed = true;
                    }

                    else if (IsPickerDataRequested)
                    {
                       
                        IsPickerDataRequested = false;
                        return;
                    }


                    /// here perform tasks
                   



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
