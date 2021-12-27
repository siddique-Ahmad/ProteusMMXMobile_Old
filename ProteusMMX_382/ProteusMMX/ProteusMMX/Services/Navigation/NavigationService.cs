using ProteusMMX.Constants;
using ProteusMMX.Crypto;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Asset;
using ProteusMMX.ViewModel.Barcode;
using ProteusMMX.ViewModel.ClosedWorkorder;
using ProteusMMX.ViewModel.Inventory;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.PurchaseOrder;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Asset;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Inventory;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.PurchaseOrder;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Parts;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.TaskAndLabour;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Tools;
using ProteusMMX.ViewModel.ServiceRequest;
using ProteusMMX.ViewModel.Workorder;
using ProteusMMX.Views;
using ProteusMMX.Views.Asset;
using ProteusMMX.Views.Barcode;
using ProteusMMX.Views.ClosedWorkorder;
using ProteusMMX.Views.Common;
using ProteusMMX.Views.Inventory;
using ProteusMMX.Views.PurchaseOrder;
using ProteusMMX.Views.SelectionListPages;
using ProteusMMX.Views.SelectionListPages.Asset;
using ProteusMMX.Views.SelectionListPages.Inventory;
using ProteusMMX.Views.SelectionListPages.PurchaseOrder;
using ProteusMMX.Views.SelectionListPages.Workorder.Parts;
using ProteusMMX.Views.SelectionListPages.Workorder.TaskAndLabour;
using ProteusMMX.Views.SelectionListPages.Workorder.Tools;
using ProteusMMX.Views.Service_Request;
using ProteusMMX.Views.ServiceRequest;
using ProteusMMX.Views.Workorder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProteusMMX.Model.PurchaseOrderModel;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Views.KPI;
using ProteusMMX.ViewModel.KPIDashboard;

namespace ProteusMMX.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        private readonly IAuthenticationService _authenticationService;
        protected readonly IFormLoadInputService _formLoadInputService;
        protected readonly Dictionary<Type, Type> _mappings;
        protected readonly IWorkorderService _workorderService;
        ServiceOutput _formControlsAndRights;
        public ServiceOutput FormControlsAndRights
        {
            get { return _formControlsAndRights; }
            set
            {
                if (value != _formControlsAndRights)
                {
                    _formControlsAndRights = value;

                }
            }
        }


        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _mappings = new Dictionary<Type, Type>();
            _workorderService = workorderService;
            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {

            await NavigateToAsync<LoginPageViewModel>();

            //For Test purpose only
            //await NavigateToAsync<ContractorListSelectionPageViewModel>();

            #region Old Code
            //if (await _authenticationService.UserIsAuthenticatedAndValidAsync())
            //{
            //    await NavigateToAsync<MainViewModel>();
            //}
            //else
            //{
            //    await NavigateToAsync<LoginViewModel>();
            //} 
            #endregion
        }



        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateBackAsync()
        {
            await CurrentApplication.MainPage.Navigation.PopAsync();

            #region Old Code
            // here the mainView is MasterDetails.
            //if (CurrentApplication.MainPage is MainView)
            //{
            //    var mainPage = CurrentApplication.MainPage as MainView;
            //    await mainPage.Detail.Navigation.PopAsync();
            //}
            //else if (CurrentApplication.MainPage != null)
            //{
            //    await CurrentApplication.MainPage.Navigation.PopAsync();
            //} 
            #endregion

        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as NavigationPage;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is WorkorderTabbedPage)
            {

                /// Add children page in it.
                var tabbedPage = page as TabbedPage;
                workOrders WorkOrders = parameter as workOrders;
                //tabbedPage.Title = WebControlTitle.GetTargetNameByTitleName("WorkOrder") + " " + WebControlTitle.GetTargetNameByTitleName("Details") + " - " + WorkOrders.WorkOrderNumber;
                #region Edit Workorder Page
                Page editWorkorderPage = CreateAndBindPage(typeof(EditWorkorderPageViewModel), parameter);
                var PageParameter = new PageParameters { Page = editWorkorderPage, Parameter = parameter };
                (editWorkorderPage.BindingContext as ViewModelBase).InitializeAsync(PageParameter);

                tabbedPage.Children.Add(editWorkorderPage);
                #endregion


                #region Task and Labour Page

                Page taskAndLabourPage = CreateAndBindPage(typeof(TaskAndLabourPageViewModel), parameter);
                var taskAndLabourPageParameter = new PageParameters { Page = taskAndLabourPage, Parameter = parameter };
                (taskAndLabourPage.BindingContext as ViewModelBase).InitializeAsync(taskAndLabourPageParameter);

                if (Application.Current.Properties.ContainsKey("TaskandLabourTabKey"))
                {
                    var TaskLabourTab = Application.Current.Properties["TaskandLabourTabKey"].ToString();
                    if (TaskLabourTab == "E" || TaskLabourTab == "V")
                    {
                        tabbedPage.Children.Add(taskAndLabourPage);
                    }
                }
                #endregion
                #region Inspection Page
                Page inspectionPage = CreateAndBindPage(typeof(InspectionPageViewModel), parameter);
                var inspectionPageParameter = new PageParameters { Page = inspectionPage, Parameter = parameter };
                (inspectionPage.BindingContext as ViewModelBase).InitializeAsync(inspectionPageParameter);
                if (Application.Current.Properties.ContainsKey("InspectionTabKey"))
                {
                    var InspectionTab = Application.Current.Properties["InspectionTabKey"].ToString();
                    if (AppSettings.User.IsInspectionUser == true && InspectionTab != null && (InspectionTab == "E" || InspectionTab == "V"))
                    {
                        if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
                        {


                        }
                        else
                        {
                            if (AppSettings.User.blackhawkLicValidator.InspectionModuleIsEnabled.Equals(true))
                            {
                                tabbedPage.Children.Add(inspectionPage);

                            }

                        }
                    }
                }
                #endregion
                #region WorkOrderStockroomParts Page

                
                    Page workOrderStockroomParts;
                  
                var StockroomPartstabbedPage = page as TabbedPage;
                    WorkOrderStockroomParts stockroomPart = parameter as WorkOrderStockroomParts;
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        workOrderStockroomParts = CreateAndBindPage(typeof(WorkOrderStockroomPartsListingPageViewModelForIOS), parameter);
                        var workOrderStockroomPartsPageParameter = new PageParameters { Page = workOrderStockroomParts, Parameter = parameter };
                        (workOrderStockroomParts.BindingContext as ViewModelBase).InitializeAsync(workOrderStockroomPartsPageParameter);
                    }
                    else
                    {
                        workOrderStockroomParts = CreateAndBindPage(typeof(WorkorderStockroomPartsTabbedPageViewModel), parameter);
                        var workOrderStockroomPartsPageParameter = new PageParameters { Page = workOrderStockroomParts, Parameter = parameter };
                        (workOrderStockroomParts.BindingContext as ViewModelBase).InitializeAsync(workOrderStockroomPartsPageParameter);
                    }
                    if (Application.Current.Properties.ContainsKey("PartsTabKey"))
                    {
                        var PartsTab = Application.Current.Properties["PartsTabKey"].ToString();

                        if (PartsTab == "E" || PartsTab == "V")
                        {
                            if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                            {


                            }
                            else
                            {
                               StockroomPartstabbedPage.Children.Add(workOrderStockroomParts);
                            }

                        }

                    //workOrderNonStockroomParts = CreateAndBindPage(typeof(WorkOrderNonStockroomPartsListingPageViewModel), parameter);
                    //var workOrderNonStockroomPartsPageParameter = new PageParameters { Page = workOrderNonStockroomParts, Parameter = parameter };
                    //(workOrderNonStockroomParts.BindingContext as ViewModelBase).InitializeAsync(workOrderNonStockroomPartsPageParameter);
                    //StockroomPartstabbedPage.Children.Add(workOrderNonStockroomParts);
                }
                
                #endregion
                #region WorkOrderTools Page

                Page workOrderTools = CreateAndBindPage(typeof(WorkorderToolListingPageViewModel), parameter);
                var workOrderToolsPageParameter = new PageParameters { Page = workOrderTools, Parameter = parameter };
                (workOrderTools.BindingContext as ViewModelBase).InitializeAsync(workOrderToolsPageParameter);
                if (Application.Current.Properties.ContainsKey("ToolsTabKey"))
                {
                    var ToolsTab = Application.Current.Properties["ToolsTabKey"].ToString();
                    if (ToolsTab == "E" || ToolsTab == "V")
                    {
                        if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                        {


                        }
                        else
                        {
                            tabbedPage.Children.Add(workOrderTools);
                        }


                    }
                }
                #endregion

                #region Attachments Page


                Page attachmentsPage = CreateAndBindPage(typeof(AttachmentsPageViewModel), parameter);
                var attachmentsPageParameter = new PageParameters { Page = attachmentsPage, Parameter = parameter };
                (attachmentsPage.BindingContext as ViewModelBase).InitializeAsync(attachmentsPageParameter);

                if (Application.Current.Properties.ContainsKey("AttachmentTabKey"))
                {
                    var AttachmentTab = Application.Current.Properties["AttachmentTabKey"].ToString();

                    if (AttachmentTab == "E" || AttachmentTab == "V")
                    {
                        tabbedPage.Children.Add(attachmentsPage);
                    }
                }

                #endregion


                //SetWorkOrderTabRights///////





            }


            if (page is ClosedWorkorderTabbedPage)
            {

                /// Add children page in it.
                var tabbedPage = page as TabbedPage;
                ClosedWorkOrder closedworkorders = parameter as ClosedWorkOrder;
                tabbedPage.Title = WebControlTitle.GetTargetNameByTitleName("ClosedWorkOrder") + " " + WebControlTitle.GetTargetNameByTitleName("Details") + " - " + closedworkorders.WorkOrderNumber;


                #region ClosedWorkOrderDetailsPage
                Page ClosedWorkOrderDetails = CreateAndBindPage(typeof(ClosedWorkorderDetailsPageViewModel), parameter);
                var DetailsParameter = new PageParameters { Page = ClosedWorkOrderDetails, Parameter = parameter };
                (ClosedWorkOrderDetails.BindingContext as ViewModelBase).InitializeAsync(DetailsParameter);
                #endregion

                #region TaskandLaborPage
                Page TaskandLaborPage = CreateAndBindPage(typeof(ClosedWorkorderTaskAndLabourPageViewModel), parameter);
                var TaskParameter = new PageParameters { Page = TaskandLaborPage, Parameter = parameter };
                (TaskandLaborPage.BindingContext as ViewModelBase).InitializeAsync(TaskParameter);
                #endregion

                #region InspectionPage
                Page InspectionPage = CreateAndBindPage(typeof(ClosedWorkorderInspectionViewModel), parameter);
                var InspectionPageParameter = new PageParameters { Page = InspectionPage, Parameter = parameter };
                (InspectionPage.BindingContext as ViewModelBase).InitializeAsync(InspectionPageParameter);
                #endregion

                #region ClosedWorkorderStockroomPartsPage
                Page ClosedWorkorderTabbed;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    ClosedWorkorderTabbed = CreateAndBindPage(typeof(ClosedWorkorderStockroomPartsViewModelForIOS), parameter);
                    var stockroomPartsParameter = new PageParameters { Page = ClosedWorkorderTabbed, Parameter = parameter };
                    (ClosedWorkorderTabbed.BindingContext as ViewModelBase).InitializeAsync(stockroomPartsParameter);
                }
                else
                {

                    ClosedWorkorderTabbed = CreateAndBindPage(typeof(CloseWorkorderStockroomPartsTabbedPageViewModel), parameter);
                    var ClosedWorkorderTabbedParameter = new PageParameters { Page = ClosedWorkorderTabbed, Parameter = parameter };
                    (ClosedWorkorderTabbed.BindingContext as ViewModelBase).InitializeAsync(ClosedWorkorderTabbedParameter);
                }
                #endregion


                #region ClosedWorkorderToolsPage
                Page ToolsPage = CreateAndBindPage(typeof(ClosedWorkorderToolsPageViewModel), parameter);
                var ToolsPageParameter = new PageParameters { Page = ToolsPage, Parameter = parameter };
                (ToolsPage.BindingContext as ViewModelBase).InitializeAsync(ToolsPageParameter);
                #endregion


                #region ClosedWorkorderAttachmentsPage
                Page AttachmentPage = CreateAndBindPage(typeof(ClosedWorkorderAttachmentsViewModel), parameter);
                var AttachmentPageParameter = new PageParameters { Page = AttachmentPage, Parameter = parameter };
                (AttachmentPage.BindingContext as ViewModelBase).InitializeAsync(AttachmentPageParameter);
                #endregion

                ServiceOutput FormControlsAndRightsForClosedworkorder = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "Closedworkorder", "Dialog");
                if (FormControlsAndRightsForClosedworkorder != null && FormControlsAndRightsForClosedworkorder.lstModules != null && FormControlsAndRightsForClosedworkorder.lstModules.Count > 0)
                {
                    var ClosedWorkorderModule = FormControlsAndRightsForClosedworkorder.lstModules[0];
                    if (ClosedWorkorderModule.ModuleName == "ClosedWorkOrderDialog") //ModuleName can't be  changed in service 
                    {
                        if (ClosedWorkorderModule.lstSubModules != null && ClosedWorkorderModule.lstSubModules.Count > 0)
                        {
                            var ClosedWorkorderSubModule = ClosedWorkorderModule.lstSubModules[0];
                            if (ClosedWorkorderSubModule.listControls != null && ClosedWorkorderSubModule.listControls.Count > 0)
                            {
                                tabbedPage.Children.Add(ClosedWorkOrderDetails);
                                var CauseTab = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Causes");
                                var additionalDetailsTab = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "AdditionalDetails");
                                var TaskandLabour = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "TaskandLabor");
                                var Inspection = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Inspection");
                                var Parts = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Parts");
                                var Tools = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Tools");
                                var Attachments = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Attachments");



                                if (TaskandLabour.Expression == "E" || TaskandLabour.Expression == "V")
                                {
                                    tabbedPage.Children.Add(TaskandLaborPage);
                                }
                                if (AppSettings.User.IsInspectionUser == true && (Inspection.Expression == "E" || Inspection.Expression == "V"))
                                {
                                    tabbedPage.Children.Add(InspectionPage);
                                }
                                if (Parts.Expression == "E" || Parts.Expression == "V")
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                                    {


                                    }
                                    else
                                    {
                                        tabbedPage.Children.Add(ClosedWorkorderTabbed);
                                    }

                                }
                                if (Tools.Expression == "E" || Tools.Expression == "V")
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                                    {


                                    }
                                    else
                                    {
                                        tabbedPage.Children.Add(ToolsPage);
                                    }

                                }
                                if (Attachments.Expression == "E" || Attachments.Expression == "V")
                                {
                                    tabbedPage.Children.Add(AttachmentPage);
                                }



                            }



                        }
                    }
                }




            }

            if (page is ProteusMMX.Views.ServiceRequest.ServiceRequestTabbedPage)
            {
                /// Add children page in it.
                var tabbedPage = page as TabbedPage;
                Model.ServiceRequestModel.ServiceRequests ServiceRequestNumber = parameter as Model.ServiceRequestModel.ServiceRequests;
                tabbedPage.Title = WebControlTitle.GetTargetNameByTitleName("Details") + " - " + ServiceRequestNumber.RequestNumber;

                #region ServiceRequestDetailPage
                Page SRDetail = CreateAndBindPage(typeof(EditServiceRequestViewModel), parameter);
                var PageParameter = new PageParameters { Page = SRDetail, Parameter = parameter };
                (SRDetail.BindingContext as ViewModelBase).InitializeAsync(PageParameter);
                #endregion

                #region ServiceRequestAttachmentPage

                Page SRAttachment = CreateAndBindPage(typeof(ServiceRequestAttachmentPageViewModel), parameter);
                var PageParameter1 = new PageParameters { Page = SRAttachment, Parameter = parameter };
                (SRAttachment.BindingContext as ViewModelBase).InitializeAsync(PageParameter1);

                #endregion

                tabbedPage.Children.Add(SRDetail);
                if (Application.Current.Properties.ContainsKey("SRAttachmentTabKey"))
                {
                    var SRAttachmentTab = Application.Current.Properties["SRAttachmentTabKey"].ToString();

                    if (SRAttachmentTab == "E" || SRAttachmentTab == "V")
                    {
                        tabbedPage.Children.Add(SRAttachment);
                    }
                }
              


            }

            if (page is ProteusMMX.Views.PurchaseOrder.PuchaseOrderTabbedPage)
            {
                /// Add children page in it.
                var tabbedPage = page as TabbedPage;
                Model.PurchaseOrderModel.PurchaseOrder PurchaseOrderNumber = parameter as Model.PurchaseOrderModel.PurchaseOrder;
                tabbedPage.Title = WebControlTitle.GetTargetNameByTitleName("PurchaseOrderDetails") + " - " + PurchaseOrderNumber.PurchaseOrderNumber;

                #region PurchaseOrderPartsListingPage
                Page PurchaseOrderParts = CreateAndBindPage(typeof(PurchaseOrderPartsListingPageViewModel), parameter);
                var PageParameter = new PageParameters { Page = PurchaseOrderParts, Parameter = parameter };
                (PurchaseOrderParts.BindingContext as ViewModelBase).InitializeAsync(PageParameter);
                #endregion

                #region PurchaseOrderNonStockroomPartsListingPage

                Page PurchaseOrderNonStockroomParts = CreateAndBindPage(typeof(PurchaseOrderNonStockroomPartsListingPageViewModel), parameter);
                var taskAndLabourPageParameter = new PageParameters { Page = PurchaseOrderNonStockroomParts, Parameter = parameter };
                (PurchaseOrderNonStockroomParts.BindingContext as ViewModelBase).InitializeAsync(taskAndLabourPageParameter);

                #endregion

                #region PurchaseOrderAssetsListingPage Page

                Page PurchaseOrderAssets = CreateAndBindPage(typeof(PurchaseOrderAssetsListingPageViewModel), parameter);
                var workOrderStockroomPartsPageParameter = new PageParameters { Page = PurchaseOrderAssets, Parameter = parameter };
                (PurchaseOrderAssets.BindingContext as ViewModelBase).InitializeAsync(workOrderStockroomPartsPageParameter);

                #endregion



                if (Application.Current.Properties.ContainsKey("Parts"))
                {
                    var POStockroompartsTab = Application.Current.Properties["Parts"].ToString();
                    if (POStockroompartsTab == "E" || POStockroompartsTab == "V")
                    {
                        tabbedPage.Children.Add(PurchaseOrderParts);
                    }
                }
                if (Application.Current.Properties.ContainsKey("Assets"))
                {
                    var POAssetsTab = Application.Current.Properties["Assets"].ToString();
                    if (POAssetsTab == "E" || POAssetsTab == "V")
                    {
                        tabbedPage.Children.Add(PurchaseOrderAssets);
                    }
                }
                if (Application.Current.Properties.ContainsKey("NonStockroomParts"))
                {
                    var PONonStockroomPartsTab = Application.Current.Properties["NonStockroomParts"].ToString();
                    if (PONonStockroomPartsTab == "E" || PONonStockroomPartsTab == "V")
                    {
                        tabbedPage.Children.Add(PurchaseOrderNonStockroomParts);
                    }
                }

                //FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(AppSettings.User.UserID.ToString(), AppSettings.ReceivingModuleName);
                //if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
                //{
                //    var PurchaseOrderModule = FormControlsAndRights.lstModules[0];
                //    if (PurchaseOrderModule.ModuleName == "Purchasing") //ModuleName can't be  changed in service 
                //    {
                //        if (PurchaseOrderModule.lstSubModules != null && PurchaseOrderModule.lstSubModules.Count > 0)
                //        {

                //            var PurchaseOrderSubModule = PurchaseOrderModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "PurchaseOrders");

                //            if (PurchaseOrderSubModule != null)
                //            {
                //                if (PurchaseOrderSubModule.Button != null && PurchaseOrderSubModule.Button.Count > 0)
                //                {
                //                    //  CloseWorkorderRights = workorderSubModule.Button.FirstOrDefault(i => i.Name == "Close");

                //                }

                //                if (PurchaseOrderSubModule.listDialoges != null && PurchaseOrderSubModule.listDialoges.Count > 0)
                //                {
                //                    var PurchaseOrderDialog = PurchaseOrderSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "Receiving");
                //                    if (PurchaseOrderDialog != null && PurchaseOrderDialog.listTab != null && PurchaseOrderDialog.listTab.Count > 0)
                //                    {
                //                        var PONonStockPartsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "NonStockroomParts");
                //                        var POAssetsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Assets");
                //                        var POPartsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Parts");
                //                        if (POPartsTab.Expression == "E" || POPartsTab.Expression == "V")
                //                        {
                //                            tabbedPage.Children.Add(PurchaseOrderParts);
                //                        }
                //                        if (POAssetsTab.Expression == "E" || POAssetsTab.Expression == "V")
                //                        {
                //                            tabbedPage.Children.Add(PurchaseOrderAssets);
                //                        }
                //                        if (PONonStockPartsTab.Expression == "E" || PONonStockPartsTab.Expression == "V")
                //                        {
                //                            tabbedPage.Children.Add(PurchaseOrderNonStockroomParts);
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
            }




            var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

            if (navigationPage != null)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                CurrentApplication.MainPage = new CustomNavigationPage(page);
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

            #region Old Code
            //if (page is MainView)
            //{
            //    CurrentApplication.MainPage = page;
            //}
            //else if (page is LoginView)
            //{
            //    CurrentApplication.MainPage = new CustomNavigationPage(page);
            //}
            //else if (CurrentApplication.MainPage is MainView)
            //{
            //    var mainPage = CurrentApplication.MainPage as MainView;
            //    var navigationPage = mainPage.Detail as CustomNavigationPage;

            //    if (navigationPage != null)
            //    {
            //        var currentPage = navigationPage.CurrentPage;

            //        if (currentPage.GetType() != page.GetType())
            //        {
            //            await navigationPage.PushAsync(page);
            //        }
            //    }
            //    else
            //    {
            //        navigationPage = new CustomNavigationPage(page);
            //        mainPage.Detail = navigationPage;
            //    }

            //    mainPage.IsPresented = false;
            //}
            //else
            //{
            //    var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

            //    if (navigationPage != null)
            //    {
            //        await navigationPage.PushAsync(page);
            //    }
            //    else
            //    {
            //        CurrentApplication.MainPage = new CustomNavigationPage(page);
            //    }
            //} 
            #endregion
        }
        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                Debug.WriteLine($"Mapping type for {viewModelType} is not a page");
                //throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            viewModel.Page = page;
            page.BindingContext = viewModel;

            return page;
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(ExtendedSplashViewModel), typeof(ExtendedSplashPage));
            _mappings.Add(typeof(LoginPageViewModel), typeof(LoginPage));
            _mappings.Add(typeof(WorkorderStockroomPartsTabbedPageViewModel), typeof(WorkorderStockroomPartsTabbedPage));
            
            _mappings.Add(typeof(DashboardPageViewModel), typeof(DashboardPage));
            _mappings.Add(typeof(KPIDashboardViewModel), typeof(KPIDashboard));
            _mappings.Add(typeof(WorkorderListingPageViewModel), typeof(WorkorderListingPage));
            _mappings.Add(typeof(CreateWorkorderPageViewModel), typeof(CreateWorkorderPage));
            _mappings.Add(typeof(WorkorderTabbedPageViewModel), typeof(WorkorderTabbedPage));
            _mappings.Add(typeof(EditWorkorderPageViewModel), typeof(EditWorkorderPage));
            _mappings.Add(typeof(TaskAndLabourPageViewModel), typeof(TaskAndLabourPage));
            _mappings.Add(typeof(CreateTaskPageViewModel), typeof(CreateTaskPage));
            _mappings.Add(typeof(WorkOrderStockroomPartsListingPageViewModel), typeof(WorkOrderStockRoomPartsListing));
            _mappings.Add(typeof(WorkorderToolListingPageViewModel), typeof(WorkOrderTools));
            _mappings.Add(typeof(AddNewToolViewModel), typeof(AddNewTool));
            _mappings.Add(typeof(WorkOrderNonStockroomPartsListingPageViewModel), typeof(WorkOrderNonStockRoomPartsListing));
            _mappings.Add(typeof(CreateNonStockroomPartsPageViewModel), typeof(CreateNonStockroomParts));
            _mappings.Add(typeof(EditNonStockroomPartsPageViewModel), typeof(EditNonStockroomParts));
            _mappings.Add(typeof(CreateWorkOrderStockroomPartsViewModel), typeof(CreateWorkOrderStockroomParts));
            _mappings.Add(typeof(EditWorkOrderStockroomPartsViewModel), typeof(EditWorkOrderStockroomParts));
            _mappings.Add(typeof(AttachmentsPageViewModel), typeof(AttachmentsPage));
            _mappings.Add(typeof(AssetListingPageViewModel), typeof(AssetListingPage));
            _mappings.Add(typeof(ServiceRequestListingPageViewModel), typeof(ServiceRequestListingPage));
            _mappings.Add(typeof(StockroomListingPageViewModel), typeof(StockroomListingPage));
            _mappings.Add(typeof(PurchaseorderListingPageViewModel), typeof(PurchaseorderListingPage));
            _mappings.Add(typeof(InspectionPageViewModel), typeof(InspectionPage));
            _mappings.Add(typeof(PartListingPageViewModel), typeof(PartListingPage));
            _mappings.Add(typeof(InventoryTransactionPageViewModel), typeof(InventoryTransactionPage));
            _mappings.Add(typeof(CreateNewAssetPageViewModel), typeof(CreateNewAssetPage));
            _mappings.Add(typeof(EditAssetPageViewModel), typeof(EditAssetPage));
            _mappings.Add(typeof(CreateServiceRequestViewModel), typeof(CreateServiceRequest));
            _mappings.Add(typeof(EditServiceRequestViewModel), typeof(EditServiceRequest));
            _mappings.Add(typeof(PuchaseOrderTabbedPageViewModel), typeof(PuchaseOrderTabbedPage));
            _mappings.Add(typeof(PurchaseOrderAssetsListingPageViewModel), typeof(PurchaseOrderAssetsListingPage));
            _mappings.Add(typeof(PurchaseOrderNonStockroomPartsListingPageViewModel), typeof(PurchaseOrderNonStockroomPartsListingPage));
            _mappings.Add(typeof(PurchaseOrderPartsListingPageViewModel), typeof(PurchaseOrderPartsListingPage));

            _mappings.Add(typeof(ReceivePuchaseOrderAssetViewModel), typeof(ReceivePuchaseOrderAsset));
            _mappings.Add(typeof(ReceivePuchaseOrderNonStockroomPartViewModel), typeof(ReceivePuchaseOrderNonStockroomPart));
            _mappings.Add(typeof(ReceivePuchaseOrderStockroomPartViewModel), typeof(ReceivePuchaseOrderStockroomPart));
            _mappings.Add(typeof(PurchaseOrderShelfBinListSelectionPageViewModel), typeof(PurchaseOrderShelfBinListSelectionPage));

            _mappings.Add(typeof(BarcodeDashboardViewModel), typeof(BarcodeDashboard));


            _mappings.Add(typeof(SearchClosedWorkorderPageViewModel), typeof(SearchClosedWorkorderPage));
            _mappings.Add(typeof(ClosedWorkorderListingPageViewModel), typeof(ClosedWorkorderListingPage));
            _mappings.Add(typeof(ClosedWorkorderTabbedPageViewModel), typeof(ClosedWorkorderTabbedPage));
            _mappings.Add(typeof(ClosedWorkorderDetailsPageViewModel), typeof(ClosedWorkorderDetailsPage));
            _mappings.Add(typeof(ClosedWorkorderTaskAndLabourPageViewModel), typeof(ClosedWorkorderTaskAndLabourPage));
            _mappings.Add(typeof(ClosedWorkorderToolsPageViewModel), typeof(ClosedWorkorderToolsPage));
            _mappings.Add(typeof(ClosedWorkorderStockroomPartsViewModel), typeof(ClosedWorkorderStockroomParts));
            _mappings.Add(typeof(ClosedWorkorderNonStockroomPartsViewModel), typeof(ClosedWorkorderNonStockroomParts));
            _mappings.Add(typeof(ClosedWorkorderAttachmentsViewModel), typeof(ClosedWorkorderAttachments));
            _mappings.Add(typeof(ClosedWorkorderInspectionViewModel), typeof(ClosedWorkorderInspection));
            _mappings.Add(typeof(CloseWorkorderStockroomPartsTabbedPageViewModel), typeof(CloseWorkorderStockroomPartsTabbedPage));


            _mappings.Add(typeof(ServiceRequestTabbedPageViewModel), typeof(ServiceRequestTabbedPage));
           // _/mappings.Add(typeof(EditServiceRequestViewModel), typeof(EditServiceRequest));
            _mappings.Add(typeof(ServiceRequestAttachmentPageViewModel), typeof(ServiceRequestAttachmentPage));

            _mappings.Add(typeof(SearchAssetByAssetNumberViewModel), typeof(SearchAssetByAssetNumber));

            _mappings.Add(typeof(SearchWorkorderByAssetNumberViewModel), typeof(SearchWorkorderByAssetNumber));

            _mappings.Add(typeof(SearchWorkorderByLocationViewModel), typeof(SearchWorkorderByLocation));

            _mappings.Add(typeof(SearchAssetForBillOfMaterialViewModel), typeof(SearchAssetForBillOfMaterial));

            _mappings.Add(typeof(SearchAssetForAttachmentViewModel), typeof(SearchAssetForAttachment));

            _mappings.Add(typeof(InventoryPerformByListSelectionPageViewModel), typeof(InventoryPerformByListSelectionPage));

            _mappings.Add(typeof(InventoryCheckoutTOListSelectionPageViewModel), typeof(InventoryCheckoutListSelectionPage));

            _mappings.Add(typeof(RiskQuestionPageViewModel), typeof(RiskQuestionsPage));
            _mappings.Add(typeof(DescriptionViewModel), typeof(Description));
            _mappings.Add(typeof(ClosedWorkorderStockroomPartsViewModelForIOS), typeof(ClosedWorkorderStockroomPartsForIOS));
            _mappings.Add(typeof(WorkOrderStockroomPartsListingPageViewModelForIOS), typeof(WorkOrderStockRoomPartsListingForIOS));
            _mappings.Add(typeof(SearchWorkorderByLocationFromBarcodeViewModel), typeof(SearchWorkorderByLocationFromBarcode));
            _mappings.Add(typeof(SearchWorkorderByAssetNumberFromBarcodeViewModel), typeof(SearchWorkorderByAssetNumberFromBarcode));
            _mappings.Add(typeof(SearchAssetFromBarcodeViewModel), typeof(SearchAssetFromBarcode));
            _mappings.Add(typeof(ShowAssetSystemViewModel), typeof(ShowAssetSystem));

            _mappings.Add(typeof(SignatureHistoryViewModel), typeof(SignatureHistory));

            #region list pages Mapping


            _mappings.Add(typeof(FacilityListSelectionPageViewModel), typeof(FacilityListSelectionPage));
            _mappings.Add(typeof(LocationListSelectionPageViewModel), typeof(LocationListSelectionPage));
            _mappings.Add(typeof(AssetListSelectionPageViewModel), typeof(AssetListSelectionPage));
            _mappings.Add(typeof(AssetSystemListSelectionPageViewModel), typeof(AssetSystemListSelectionPage));
            _mappings.Add(typeof(AssignToListSelectionPageViewModel), typeof(AssignToListSelectionPage));
            _mappings.Add(typeof(WorkorderRequesterListSelectionPageViewModel), typeof(WorkorderRequesterListSelectionPage));
            _mappings.Add(typeof(CostCenterListSelectionPageViewModel), typeof(CostCenterListSelectionPage));
            _mappings.Add(typeof(PriorityListSelectionPageViewModel), typeof(PriorityListSelectionPage));
            _mappings.Add(typeof(ShiftListSelectionPageViewModel), typeof(ShiftListSelectionPage));
            _mappings.Add(typeof(WorkorderStatusListSelectionPageViewModel), typeof(WorkorderStatusListSelectionPage));
            _mappings.Add(typeof(WorkorderTypeListSelectionPageViewModel), typeof(WorkorderTypeListSelectionPage));
            _mappings.Add(typeof(CauseListSelectionPageViewModel), typeof(CauseListSelectionPage));
            _mappings.Add(typeof(MaintenanceCodeListSelectionPageViewModel), typeof(MaintenanceCodeListSelectionPage));

            #region Workorder 

            #region Task And Labour

            _mappings.Add(typeof(TaskListSelectionPageViewModel), typeof(TaskListSelectionPage));
            _mappings.Add(typeof(EmployeeListSelectionPageViewModel), typeof(EmployeeListSelectionPage));
            _mappings.Add(typeof(ContractorListSelectionPageViewModel), typeof(ContractorListSelectionPage));


            #endregion

            #region Tools

            _mappings.Add(typeof(ToolCribListSelectionPageViewModel), typeof(ToolCribListSelectionPage));
            _mappings.Add(typeof(ToolNumberListSelectionPageViewModel), typeof(ToolNumberListSelectionPage));

            #endregion

            #region Parts

            _mappings.Add(typeof(PartListSelectionPageViewModel), typeof(PartListSelectionPage));
            _mappings.Add(typeof(StockroomListSelectionPageViewModel), typeof(StockroomListSelectionPage));
            _mappings.Add(typeof(ShelfBinListSelectionPageViewModel), typeof(ShelfBinListSelectionPage));

            #endregion
            #endregion


            #region Inventory 





            _mappings.Add(typeof(InventoryCostCenterListSelectionPageViewModel), typeof(InventoryCostCenterListSelectionPage));
            _mappings.Add(typeof(InventoryShelfBinListSelectionPageViewModel), typeof(InventoryShelfBinListSelectionPage));
            _mappings.Add(typeof(TransactionReasonListSelectionPageViewModel), typeof(TransactionReasonListSelectionPage));
            _mappings.Add(typeof(TransactionTypeListSelectionPageViewModel), typeof(TransactionTypeListSelectionPage));


            #endregion


            #region Asset 





            _mappings.Add(typeof(CategoryListSelectionPageViewModel), typeof(CategoryListSelectionPage));
            _mappings.Add(typeof(RuntimeUnitListSelectionPageViewModel), typeof(RuntimeUnitListSelectionPage));
            _mappings.Add(typeof(VendorListSelectionPageViewModel), typeof(VendorListSelectionPage));

            #endregion
            #endregion

            #region Receiving 
            _mappings.Add(typeof(ReceiverListSelectionPageViewModel), typeof(ReceiverListSelectionPage));

            #endregion


            //_mappings.Add(typeof(AssetAttachmentViewModel), typeof(AssetAttachmentPage));

            #region Old Code
            //_mappings.Add(typeof(BookingCalendarViewModel), typeof(BookingCalendarView));
            //_mappings.Add(typeof(BookingHotelViewModel), typeof(BookingHotelView));
            //_mappings.Add(typeof(BookingHotelsViewModel), typeof(BookingHotelsView));
            //_mappings.Add(typeof(BookingViewModel), typeof(BookingView));
            //_mappings.Add(typeof(CheckoutViewModel), typeof(CheckoutView));
            //_mappings.Add(typeof(LoginViewModel), typeof(LoginView));
            //_mappings.Add(typeof(MainViewModel), typeof(MainView));
            //_mappings.Add(typeof(MyRoomViewModel), typeof(MyRoomView));
            //_mappings.Add(typeof(NotificationsViewModel), typeof(NotificationsView));
            //_mappings.Add(typeof(OpenDoorViewModel), typeof(OpenDoorView));
            //_mappings.Add(typeof(SettingsViewModel<RemoteSettings>), typeof(SettingsView));
            //_mappings.Add(typeof(ExtendedSplashViewModel), typeof(ExtendedSplashView));

            //if (Device.Idiom == TargetIdiom.Desktop)
            //{
            //    _mappings.Add(typeof(HomeViewModel), typeof(UwpHomeView));
            //    _mappings.Add(typeof(SuggestionsViewModel), typeof(UwpSuggestionsView));
            //}
            //else
            //{
            //    _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
            //    _mappings.Add(typeof(SuggestionsViewModel), typeof(SuggestionsView));
            //} 
            #endregion
        }


    }
}
