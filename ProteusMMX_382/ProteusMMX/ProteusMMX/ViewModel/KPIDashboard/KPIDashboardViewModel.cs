using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProteusMMX.ViewModel.KPIDashboard
{
    public class KPIDashboardViewModel: ViewModelBase,IHandleViewAppearing, IHandleViewDisappearing
    {
        public readonly IWorkorderService _workorderService;
        // Priority
        int? _priorityID;
        public int? PriorityID
        {
            get
            {
                return _priorityID;
            }

            set
            {
                if (value != _priorityID)
                {
                    _priorityID = value;
                    OnPropertyChanged(nameof(PriorityID));
                }
            }
        }

        string _priorityName;
        public string PriorityName
        {
            get
            {
                return _priorityName;
            }

            set
            {
                if (value != _priorityName)
                {
                    _priorityName = value;
                    OnPropertyChanged(nameof(PriorityName));
                }
            }
        }

        string _overDueText;
        public string OverDueText
        {
            get
            {
                return _overDueText;
            }

            set
            {
                if (value != _overDueText)
                {
                    _overDueText = value;
                    OnPropertyChanged("OverDueText");
                }
            }
        }

        string _todayText;
        public string TodayText
        {
            get
            {
                return _todayText;
            }

            set
            {
                if (value != _todayText)
                {
                    _todayText = value;
                    OnPropertyChanged("TodayText");
                }
            }
        }

        string _thisWeekText ;
        public string ThisWeekText
        {
            get
            {
                return _thisWeekText;
            }

            set
            {
                if (value != _thisWeekText)
                {
                    _thisWeekText = value;
                    OnPropertyChanged("ThisWeekText");
                }
            }
        }

        string _todayHours;
        public string TodayHours
        {
            get
            {
                return _todayHours;
            }

            set
            {
                if (value != _todayHours)
                {
                    _todayHours = value;
                    OnPropertyChanged("TodayHours");
                }
            }
        }

        string _thisWeekHours;
        public string ThisWeekHours
        {
            get
            {
                return _thisWeekHours;
            }

            set
            {
                if (value != _thisWeekHours)
                {
                    _thisWeekHours = value;
                    OnPropertyChanged("ThisWeekHours");
                }
            }
        }

        string _thisMonthHours;
        public string ThisMonthHours
        {
            get
            {
                return _thisMonthHours;
            }

            set
            {
                if (value != _thisMonthHours)
                {
                    _thisMonthHours = value;
                    OnPropertyChanged("ThisMonthHours");
                }
            }
        }

        string _todayPlannedHours;
        public string TodayPlannedHours
        {
            get
            {
                return _todayPlannedHours;
            }

            set
            {
                if (value != _todayPlannedHours)
                {
                    _todayPlannedHours = value;
                    OnPropertyChanged("TodayPlannedHours");
                }
            }
        }

        string _weeklyPlannedHours;
        public string WeeklyPlannedHours
        {
            get
            {
                return _weeklyPlannedHours;
            }

            set
            {
                if (value != _weeklyPlannedHours)
                {
                    _weeklyPlannedHours = value;
                    OnPropertyChanged("WeeklyPlannedHours");
                }
            }
        }

        string _monthlyPlannedHours;
        public string MonthlyPlannedHours
        {
            get
            {
                return _monthlyPlannedHours;
            }

            set
            {
                if (value != _monthlyPlannedHours)
                {
                    _monthlyPlannedHours = value;
                    OnPropertyChanged("MonthlyPlannedHours");
                }
            }
        }

        string _pageTitle;
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



        public ICommand PriorityCommand => new AsyncCommand(ShowPriority);
        
        public ICommand TodayTapCommand => new AsyncCommand(TodayTap);

        public ICommand ThisWeekTapCommand => new AsyncCommand(ThisWeekTap);
        public ICommand OverDueTapCommand => new AsyncCommand(OnOverDueTap);
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("KPIDashboard");
            }
            catch (Exception)
            {

                throw;
            }

        }
        public KPIDashboardViewModel(IWorkorderService workorderService)
        {
            
            _workorderService = workorderService;
        }
        public async Task ShowPriority()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(10);
                await NavigationService.NavigateToAsync<PriorityListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

        
        public async Task ThisWeekTap()
        {
            try
            {
                if (ThisWeekText == "0")
                {
                    return;
                }
                if (!String.IsNullOrWhiteSpace(this.PriorityID.ToString()))
                {
                    Application.Current.Properties["PriorityID"] = this.PriorityID;
                }
                Application.Current.Properties["weekly"] = "weekly";
               
                await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();

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
        public async Task TodayTap()
        {
            try
            {
                if (TodayText == "0")
                {
                    return;
                }
                if (!String.IsNullOrWhiteSpace(this.PriorityID.ToString()))
                {
                    Application.Current.Properties["PriorityID"] = this.PriorityID;
                }
                Application.Current.Properties["today"] = "today";
               
                await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();

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
        public async Task OnOverDueTap()
        {
            try
            {
                if(OverDueText=="0")
                {
                    return;
                }
                if(!String.IsNullOrWhiteSpace(this.PriorityID.ToString()))
                {
                    Application.Current.Properties["PriorityID"] = this.PriorityID;
                }
                Application.Current.Properties["overdue"] = "overdue";
                await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();

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
        private void OnPriorityRequested(object obj)
        {

            if (obj != null)
            {

                var priority = obj as ComboDD;
                this.PriorityID = priority.SelectedValue;
                this.PriorityName = ShortString.shorten(priority.SelectedText);
                 GetKPIDashboardDetailsByPriority(AppSettings.User.UserID,this.PriorityID);

            }


        }
        async Task GetKPIDashboardDetailsByPriority(int userid, int? priorityID)
        {
            try
            {

                var KPIResponse = await _workorderService.GetWorkOrderKPIDetails(AppSettings.User.UserID, priorityID);
                if (KPIResponse != null && KPIResponse.KPIWorkorderWrapper != null)
                {
                   
                    OverDueText = String.IsNullOrEmpty(KPIResponse.KPIWorkorderWrapper.OverdueWorkOrder) ? "0" : KPIResponse.KPIWorkorderWrapper.OverdueWorkOrder;
                    TodayText = String.IsNullOrEmpty(KPIResponse.KPIWorkorderWrapper.TodayOpenWorkOrder) ? "0" : KPIResponse.KPIWorkorderWrapper.TodayOpenWorkOrder;
                    ThisWeekText = String.IsNullOrEmpty(KPIResponse.KPIWorkorderWrapper.WeeklyWorkOrder) ? "0" : KPIResponse.KPIWorkorderWrapper.WeeklyWorkOrder;
                    TodayHours = String.IsNullOrEmpty(KPIResponse.KPIWorkorderWrapper.TodayHours) ? "0" : KPIResponse.KPIWorkorderWrapper.TodayHours;
                    ThisWeekHours = String.IsNullOrEmpty(KPIResponse.KPIWorkorderWrapper.WeeklyHours) ? "0" : KPIResponse.KPIWorkorderWrapper.WeeklyHours;
                    ThisMonthHours = String.IsNullOrEmpty(KPIResponse.KPIWorkorderWrapper.MonthlyHours) ? "0" : KPIResponse.KPIWorkorderWrapper.MonthlyHours;

                    TodayPlannedHours = String.IsNullOrEmpty(KPIResponse.KPIWorkorderWrapper.TodayPlannedHours) ? "0" : KPIResponse.KPIWorkorderWrapper.TodayPlannedHours;

                    WeeklyPlannedHours = String.IsNullOrEmpty(KPIResponse.KPIWorkorderWrapper.WeeklyPlannedHours) ? "0" : KPIResponse.KPIWorkorderWrapper.WeeklyPlannedHours;

                    MonthlyPlannedHours = String.IsNullOrEmpty(KPIResponse.KPIWorkorderWrapper.MonthlyPlannedHours) ? "0" : KPIResponse.KPIWorkorderWrapper.MonthlyPlannedHours;

                }
            }

            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }

            finally
            {
                
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }
        }
        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(10);
                //Retrive Priority
                MessagingCenter.Subscribe<object>(this, MessengerKeys.PriorityRequested, OnPriorityRequested);

                await GetKPIDashboardDetailsByPriority(AppSettings.User.UserID, this.PriorityID);
            }
            catch (Exception)
            {
                UserDialogs.Instance.HideLoading();
                
            }
            finally
            {
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;

            }
        }
        public async Task OnViewDisappearingAsync(VisualElement view)
        {
           
        }

    }
    
}
