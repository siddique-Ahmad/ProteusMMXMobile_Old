using Acr.UserDialogs;
using Newtonsoft.Json;
using PanCardView.Extensions;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ProteusMMX.DependencyInterface;
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
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace ProteusMMX.ViewModel.Workorder
{
    public class AttachmentsPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;
        public readonly IAttachmentService _attachmentService;
        protected readonly IFormLoadInputService _formLoadInputService;

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
        string _deleteTitle;
        public string DeleteTitle
        {
            get
            {
                return _deleteTitle;
            }

            set
            {
                if (value != _deleteTitle)
                {
                    _deleteTitle = value;
                    OnPropertyChanged(nameof(DeleteTitle));
                }
            }
        }

        string _swipeText;
        public string SwipeText
        {
            get
            {
                return _swipeText;
            }

            set
            {
                if (value != _swipeText)
                {
                    _swipeText = value;
                    OnPropertyChanged(nameof(SwipeText));
                }
            }
        }


        string _pDFImageText;
        public string PDFImageText
        {
            get
            {
                return _pDFImageText;
            }

            set
            {
                if (value != _pDFImageText)
                {
                    _pDFImageText = value;
                    OnPropertyChanged(nameof(PDFImageText));
                }
            }
        }
        string _imageText;
        public string ImageText
        {
            get
            {
                return _imageText;
            }

            set
            {
                if (value != _imageText)
                {
                    _imageText = value;
                    OnPropertyChanged(nameof(ImageText));
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



        #endregion


        #region Attachment Page Properties


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




        public ObservableCollection<WorkorderAttachment> Attachments { get; set; }
        public ObservableCollection<string> DocumentAttachments { get; set; }

        int _selectedIndexItem = -1;
        public int SelectedIndexItem
        {
            get
            {
                return _selectedIndexItem;
            }

            set
            {
                _selectedIndexItem = value;
                OnPropertyChanged("SelectedIndexItem");

            }
        }

        int _selectedPosition;
        public int SelectedPosition
        {
            get
            {
                return _selectedPosition;
            }

            set
            {
                if (value != _selectedPosition)
                {
                    _selectedPosition = value;
                    OnPropertyChanged(nameof(SelectedPosition));
                }
            }
        }

        #endregion


        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand DocCommand => new AsyncCommand(OpenDoc);
        public ICommand CameraCommand => new AsyncCommand(OpenMedia);
        public ICommand DeleteCommand => new AsyncCommand(DeleteAttachment);
        public ICommand SaveCommand => new AsyncCommand(SaveAttachment);

        public ICommand AttachmentTapCommand => new AsyncCommand(AttachmentClicked);


        public bool IsAutoAnimationRunning { get; set; }

        public bool IsUserInteractionRunning { get; set; }

        public ICommand PanPositionChangedCommand { get; }



        #endregion

        #region Rights Properties

        string CreateAttachment;
        string DeleteAttachments;
        string AttachmentFilesAttachments;

        bool _attachmentCameraButtonIsVisible = true;
        public bool AttachmentCameraButtonIsVisible
        {
            get
            {
                return _attachmentCameraButtonIsVisible;
            }

            set
            {
                if (value != _attachmentCameraButtonIsVisible)
                {
                    _attachmentCameraButtonIsVisible = value;
                    OnPropertyChanged(nameof(AttachmentCameraButtonIsVisible));
                }
            }
        }

        bool _attachmentCameraButtonIsEnabled = true;
        public bool AttachmentCameraButtonIsEnabled
        {
            get
            {
                return _attachmentCameraButtonIsEnabled;
            }

            set
            {
                if (value != _attachmentCameraButtonIsEnabled)
                {
                    _attachmentCameraButtonIsEnabled = value;
                    OnPropertyChanged(nameof(AttachmentCameraButtonIsEnabled));
                }
            }
        }

        bool _attachmentDeleteButtonIsEnabled = true;
        public bool AttachmentDeleteButtonIsEnabled
        {
            get
            {
                return _attachmentDeleteButtonIsEnabled;
            }

            set
            {
                if (value != _attachmentDeleteButtonIsEnabled)
                {
                    _attachmentDeleteButtonIsEnabled = value;
                    OnPropertyChanged(nameof(AttachmentDeleteButtonIsEnabled));
                }
            }
        }

        bool _attachmentDeleteButtonIsVisible = true;
        public bool AttachmentDeleteButtonIsVisible
        {
            get
            {
                return _attachmentDeleteButtonIsVisible;
            }

            set
            {
                if (value != _attachmentDeleteButtonIsVisible)
                {
                    _attachmentDeleteButtonIsVisible = value;
                    OnPropertyChanged(nameof(AttachmentDeleteButtonIsVisible));
                }
            }
        }

        bool _pDFImageTextIsVisible = true;
        public bool PDFImageTextIsVisible
        {
            get
            {
                return _pDFImageTextIsVisible;
            }

            set
            {
                if (value != _pDFImageTextIsVisible)
                {
                    _pDFImageTextIsVisible = value;
                    OnPropertyChanged(nameof(PDFImageTextIsVisible));
                }
            }
        }


        bool _attachmentSaveButtonIsEnabled = true;
        public bool AttachmentSaveButtonIsEnabled
        {
            get
            {
                return _attachmentSaveButtonIsEnabled;
            }

            set
            {
                if (value != _attachmentSaveButtonIsEnabled)
                {
                    _attachmentSaveButtonIsEnabled = value;
                    OnPropertyChanged(nameof(AttachmentSaveButtonIsEnabled));
                }
            }
        }

        bool _attachmentSaveButtonIsVisible = true;
        public bool AttachmentSaveButtonIsVisible
        {
            get
            {
                return _attachmentSaveButtonIsVisible;
            }

            set
            {
                if (value != _attachmentSaveButtonIsVisible)
                {
                    _attachmentSaveButtonIsVisible = value;
                    OnPropertyChanged(nameof(AttachmentSaveButtonIsVisible));
                }
            }
        }

        bool _attachmentFileIsEnabled = true;
        public bool AttachmentFileIsEnabled
        {
            get
            {
                return _attachmentFileIsEnabled;
            }

            set
            {
                if (value != _attachmentFileIsEnabled)
                {
                    _attachmentFileIsEnabled = value;
                    OnPropertyChanged(nameof(AttachmentFileIsEnabled));
                }
            }
        }

        bool _attachmentFileIsVisible = true;
        public bool AttachmentFileIsVisible
        {
            get
            {
                return _attachmentFileIsVisible;
            }

            set
            {
                if (value != _attachmentFileIsVisible)
                {
                    _attachmentFileIsVisible = value;
                    OnPropertyChanged(nameof(AttachmentFileIsVisible));
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

                if (navigationData != null)
                {

                    var navigationParams = navigationData as PageParameters;


                    var workorder = navigationParams.Parameter as workOrders;
                    this.WorkorderID = workorder.WorkOrderID;



                }

                //FormLoadInputForWorkorder = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                await SetTitlesPropertiesForPage();

                if (Application.Current.Properties.ContainsKey("CreateAttachment"))
                {
                    CreateAttachment = Application.Current.Properties["CreateAttachment"].ToString();
                    if (CreateAttachment == "E")
                    {
                        AttachmentCameraButtonIsVisible = true;
                        AttachmentSaveButtonIsVisible = true;
                    }
                    else if (CreateAttachment == "V")
                    {
                        AttachmentSaveButtonIsEnabled = false;
                        AttachmentCameraButtonIsEnabled = false;

                    }
                    else
                    {
                        AttachmentCameraButtonIsVisible = false;
                        AttachmentSaveButtonIsVisible = false;
                    }

                }
                if (Application.Current.Properties.ContainsKey("DeleteAttachments"))
                {
                    DeleteAttachments = Application.Current.Properties["DeleteAttachments"].ToString();
                    if (DeleteAttachments == "E")
                    {
                        AttachmentDeleteButtonIsVisible = true;
                    }
                    else if (DeleteAttachments == "V")
                    {
                        AttachmentDeleteButtonIsEnabled = false;
                    }
                    else
                    {
                        AttachmentDeleteButtonIsVisible = false;
                    }
                }
                if (Application.Current.Properties.ContainsKey("AttachmentFiles"))
                {
                    AttachmentFilesAttachments = Application.Current.Properties["AttachmentFiles"].ToString();
                    if (AttachmentFilesAttachments == "E")
                    {
                        AttachmentFileIsVisible = true;
                    }
                    else if (AttachmentFilesAttachments == "V")
                    {
                        AttachmentFileIsEnabled = false;
                    }
                    else
                    {
                        AttachmentFileIsVisible = false;
                    }
                }
                OperationInProgress = false;

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

        public AttachmentsPageViewModel(IAuthenticationService authenticationService, IAttachmentService attachmentService, IFormLoadInputService formLoadInputService)
        {
            _authenticationService = authenticationService;
            _attachmentService = attachmentService;
            _formLoadInputService = formLoadInputService;
            Attachments = new ObservableCollection<WorkorderAttachment>
            {

            };

            DocumentAttachments = new ObservableCollection<string>
            {

            };
            PanPositionChangedCommand = new Command(v =>
            {

                if (IsAutoAnimationRunning || IsUserInteractionRunning)
                {
                    return;
                }

                var index = SelectedIndexItem + (bool.Parse(v.ToString()) ? 1 : -1);
                if (index < 0 || index >= Attachments.Count)
                {
                    return;
                }
                SelectedIndexItem = index;
                var Selecteditemname = Attachments[SelectedIndexItem];
                if (Selecteditemname.attachmentFileExtension != null &&
                                  (Selecteditemname.attachmentFileExtension.ToLower().Contains(".pdf") ||
                                  Selecteditemname.attachmentFileExtension.ToLower().Contains(".doc") ||
                                  Selecteditemname.attachmentFileExtension.ToLower().Contains(".docx") ||
                                  Selecteditemname.attachmentFileExtension.ToLower().Contains(".xls") ||
                                  Selecteditemname.attachmentFileExtension.ToLower().Contains(".xlsx") ||
                                  Selecteditemname.attachmentFileExtension.ToLower().Contains(".txt")))
                {
                    PDFImageText = Selecteditemname.attachmentFileExtension;
                }
                else
                {
                    PDFImageText = "";
                }


            });


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
                    SaveTitle = WebControlTitle.GetTargetNameByTitleName("Save");
                    SwipeText = WebControlTitle.GetTargetNameByTitleName("Pleaseswipelefttoright");
                    DeleteTitle = WebControlTitle.GetTargetNameByTitleName("Delete");
                    SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");

                    ImageText = WebControlTitle.GetTargetNameByTitleName("Total") + " " + WebControlTitle.GetTargetNameByTitleName("Image") + " : " + 0;






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
        public async Task OpenDoc()
        {

            try
            {


                OperationInProgress = true;
                var action = await DialogService.SelectActionAsync(WebControlTitle.GetTargetNameByTitleName("ChooseFile"), SelectTitle, CancelTitle, DocumentAttachments.ToArray());
                if (DocumentAttachments.Count == 0)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThereisnoPDFattached"), 2000);
                    return;
                }
                if (action == WebControlTitle.GetTargetNameByTitleName("Cancel") || action == null)
                {
                    //UserDialogs.Instance.HideLoading();

                    return;
                }
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + action);
                        break;
                    case Device.Android:
                        DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + action);

                        //var pdfurl = AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + action;
                        //Device.OpenUri(new Uri(pdfurl));
                        break;
                    case Device.UWP:
                        DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + action);
                        break;
                }


                //browser.Source = AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + action;
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


        public async Task AttachmentClicked()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task OpenMedia()
        {
            try
            {
                List<string> choice = new List<string>() { WebControlTitle.GetTargetNameByTitleName("Camera"), WebControlTitle.GetTargetNameByTitleName("Gallery")};
                var selected = await DialogService.SelectActionAsync(WebControlTitle.GetTargetNameByTitleName("ChooseFile"), SelectTitle, CancelTitle, choice.ToArray());
                if (selected != null && !selected.Equals(WebControlTitle.GetTargetNameByTitleName("Cancel")))
                {

                    if (selected.Equals(WebControlTitle.GetTargetNameByTitleName("Camera")))
                    {
                        IsDataRequested = true;
                        await TakePhoto();
                    }
                    //else
                    //{
                    //    IsDataRequested = true;
                    //    await PickPhoto();
                    //}
                    else if (selected.Equals(WebControlTitle.GetTargetNameByTitleName("Gallery")))
                    {
                        IsDataRequested = true;
                        await PickPhoto();
                    }
                    else
                    {
                        IsDataRequested = true;
                        await PickFile();
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

        public async Task TakePhoto()
        {
            try
            {


                OperationInProgress = true;

                int count = 0;
                foreach (var item in Attachments)
                {

                    if (item.IsSynced == false)
                    {
                        count++;
                    }

                    if (count == 5)
                    {
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CannotUploadmorethanfiveimages"), 2000);
                        return;
                    }
                }
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {


                    DialogService.ShowToast("No camera available.", 2000);
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "ProteusMMX",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front
                });

                if (file == null)
                {


                    return;
                }


                string filepath = file.AlbumPath;
                string filename = "Image" + DateTime.Now.Ticks + ".png";

                byte[] byteImg = StreamToBase64.FileToByte(file);

                //if (Attachments.Count > 4 && Attachments != null)
                //{
                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CannotUploadmorethanfiveimages"), 2000);
                //    return;

                //}

                Attachments.Add(new WorkorderAttachment
                {
                    IsSynced = false,
                    attachmentFileExtension = "Image" + DateTime.Now.Ticks + ".png",
                    ImageBytes = byteImg,
                    AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImg)))),

                });


                file.Dispose();
                workOrderWrapper workorderWrapper = new workOrderWrapper();
                workorderWrapper.attachments = new List<WorkOrderAttachment>();

                var ImagesPicsList1 = Attachments;
                foreach (var file1 in ImagesPicsList1)
                {
                    if (file1.WorkOrderAttachmentID == null && file1.IsSynced == false)
                    {
                        WorkOrderAttachment woattachment = new WorkOrderAttachment();
                        woattachment.WorkOrderID = WorkorderID;
                        //woattachment.ModifiedUserName = "Eagle4";
                        //  woattachment.attachmentFile = Convert.ToBase64String(file.ImageBytes);
                        woattachment.attachmentFile = Convert.ToBase64String(file1.ImageBytes);
                        woattachment.attachmentFileExtension = file1.attachmentFileExtension;
                        workorderWrapper.attachments.Add(woattachment);
                    }


                }
                //   var Count = workorderWrapper.attachments.Count;

                var status = await _attachmentService.CreateWorkorderAttachment(UserID, workorderWrapper);

                if (Boolean.Parse(status.servicestatus))
                {
                    foreach (var file1 in ImagesPicsList1)
                    {
                        if (file1.WorkOrderAttachmentID == null)
                        {
                            file1.IsSynced = true;


                        }

                    }
                    IsDataRequested = false;
                    await this.OnViewAppearingAsync(null);

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AttachmentSuccessfullySaved"), 2000);



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
        #region Old 
        //public async Task PickFile()
        //{
        //    String FinalLogstring = String.Empty;
        //    try
        //    {

        //        FinalLogstring = "PickFile Is Start";
        //        OperationInProgress = true;
        //        int count = 0;
        //        foreach (var item in Attachments)
        //        {

        //            if (item.IsSynced == false)
        //            {
        //                count++;
        //            }


        //        }
        //        string[] fileTypes = null;
        //        if (Device.RuntimePlatform == Device.iOS|| Device.RuntimePlatform == Device.Android)
        //        {
        //            fileTypes = new string[] { "com.adobe.pdf", "public.rft", "com.microsoft.word.doc", "org.openxmlformats.wordprocessingml.document" };
        //        }
        //       // await PickAndShow(fileTypes);
        //        FinalLogstring = FinalLogstring + " file await CrossFilePicker.Current.PickFile() start ";
        //        var file = await CrossFilePicker.Current.PickFile(fileTypes);
        //        //var file = await FilePicker.PickAsync(options);
        //        FinalLogstring = FinalLogstring + " file check null ";
        //        if (file == null)
        //        {

        //            return;
        //        }

        //        FinalLogstring = FinalLogstring + " file await  " + file.FilePath;

        //        string filepath = file.FilePath;
        //        int filesize = file.DataArray.Length;
        //        var filelength = filesize / 1024;
        //        string strfilesize = Convert.ToString(filelength) + "KB";
        //        FinalLogstring = FinalLogstring + " file Size  " + strfilesize;
        //        if (filelength > 1024)
        //        {
        //            await App.Current.MainPage.DisplayAlert("Alert", "File too large File must be less than 1 Mb", "OK");
        //            //UserDialogs.Instance.Toast("File too large File must be less than 1 Mb");
        //            return;
        //            //filelength = filelength / 1024;
        //            //strfilesize = Convert.ToString(filelength) + "MB";
        //        }
        //        FinalLogstring = FinalLogstring + " Checked All Validation   ";

        //        string base64String = Convert.ToBase64String(file.DataArray);
        //        FinalLogstring = FinalLogstring + "base64String   file.DataArray ";
        //        workOrderWrapper workorderWrapper = new workOrderWrapper();
        //        workorderWrapper.attachments = new List<WorkOrderAttachment>();
        //        WorkOrderAttachment woattachment = new WorkOrderAttachment();
        //        woattachment.WorkOrderID = WorkorderID;
        //        woattachment.attachmentFile = base64String;
        //        woattachment.attachmentFileExtension = file.FileName;
        //        workorderWrapper.attachments.Add(woattachment);
        //        FinalLogstring = FinalLogstring + "Rady CreateWorkorderAttachment userId :" + UserID;
        //        var status = await _attachmentService.CreateWorkorderAttachment(UserID, workorderWrapper);

        //        if (Boolean.Parse(status.servicestatus))
        //        {
        //            FinalLogstring = FinalLogstring + "If status.servicestatus   " + status.servicestatus;
        //            IsDataRequested = false;
        //            await this.OnViewAppearingAsync(null);
        //            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AttachmentSuccessfullySaved"), 2000);
        //        }
        //        else
        //        {
        //            FinalLogstring = FinalLogstring + " else status.servicestatus   " + status.servicestatus;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        LogMessage(FinalLogstring + " catch " + ex.ToString() + " StackTrace " + ex.StackTrace + " InnerException  " + ex.InnerException);
        //        OperationInProgress = false;
        //    }

        //    finally
        //    {
        //        LogMessage(FinalLogstring + " finally : ");
        //        OperationInProgress = false;
        //    }
        //}
        #endregion

        public async Task PickFile()
        {
            string FinalLogstring = string.Empty;
            try
            {
                OperationInProgress = true;
                string[] fileTypes = null;
                if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
                {
                    fileTypes = new string[] { "com.adobe.pdf", "public.rft", "com.microsoft.word.doc", "org.openxmlformats.wordprocessingml.document" };
                }
                await PickAndShow(fileTypes);
            }
            catch (Exception ex)
            {

                LogMessage(FinalLogstring + " catch PickFile1 " + ex.ToString() + " StackTrace " + ex.StackTrace + " InnerException  " + ex.InnerException);
                OperationInProgress = false;
            }
            finally
            {
                LogMessage(FinalLogstring + " finally PickFile1 : ");
                OperationInProgress = false;
            }
        }

        private async Task PickAndShow(string[] fileTypes)
        {
            string FinalLogstring = string.Empty;
            try
            {

                var file = await CrossFilePicker.Current.PickFile(fileTypes);
                if (file != null)
                {
                    if (file == null)
                    {

                        return;
                    }

                    FinalLogstring = FinalLogstring + " file await  " + file.FilePath;

                    string filepath = file.FilePath;
                    int filesize = file.DataArray.Length;
                    var filelength = filesize / 1024;
                    string strfilesize = Convert.ToString(filelength) + "KB";
                    FinalLogstring = FinalLogstring + " file Size  " + strfilesize;
                    if (filelength > 1024)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "File too large File must be less than 1 Mb", "OK");
                        //UserDialogs.Instance.Toast("File too large File must be less than 1 Mb");
                        return;
                        //filelength = filelength / 1024;
                        //strfilesize = Convert.ToString(filelength) + "MB";
                    }
                    FinalLogstring = FinalLogstring + " Checked All Validation   ";

                    string base64String = Convert.ToBase64String(file.DataArray);
                    FinalLogstring = FinalLogstring + "base64String   file.DataArray ";
                    workOrderWrapper workorderWrapper = new workOrderWrapper();
                    workorderWrapper.attachments = new List<WorkOrderAttachment>();
                    WorkOrderAttachment woattachment = new WorkOrderAttachment();
                    woattachment.WorkOrderID = WorkorderID;
                    woattachment.attachmentFile = base64String;
                    woattachment.attachmentFileExtension = file.FileName;
                    workorderWrapper.attachments.Add(woattachment);
                    FinalLogstring = FinalLogstring + "Rady CreateWorkorderAttachment userId :" + UserID;
                    var status = await _attachmentService.CreateWorkorderAttachment(UserID, workorderWrapper);

                    if (Boolean.Parse(status.servicestatus))
                    {
                        FinalLogstring = FinalLogstring + "If status.servicestatus   " + status.servicestatus;
                        IsDataRequested = false;
                        await this.OnViewAppearingAsync(null);
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AttachmentSuccessfullySaved"), 2000);
                    }
                    else
                    {
                        FinalLogstring = FinalLogstring + " else status.servicestatus   " + status.servicestatus;
                    }
                }
            }
            catch (Exception ex)
            {

                LogMessage(FinalLogstring + " catch " + ex.ToString() + " StackTrace " + ex.StackTrace + " InnerException  " + ex.InnerException);
                OperationInProgress = false;
            }
            finally
            {
                LogMessage(FinalLogstring + " finally : ");
                OperationInProgress = false;
            }
        }
        public async Task PickPhoto()
        {
            try
            {


                OperationInProgress = true;
                int count = 0;
                foreach (var item in Attachments)
                {

                    if (item.IsSynced == false)
                    {
                        count++;
                    }

                    if (count == 5)
                    {
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CannotUploadmorethanfiveimages"), 2000);
                        return;
                    }
                }
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast("No camera available.", 2000);
                    return;
                }
                //var file1 = await CrossFilePicker.Current.PickFile();
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });


                if (file == null)
                {

                    return;
                }


                string filepath = file.AlbumPath;
                string filename = "Image" + DateTime.Now.Ticks + ".png";

                byte[] byteImg = StreamToBase64.FileToByte(file);

                //if (Attachments.Count > 4 && Attachments != null)
                //{
                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CannotUploadmorethanfiveimages"), 2000);

                //    return;

                //}

                Attachments.Add(new WorkorderAttachment
                {
                    IsSynced = false,
                    attachmentFileExtension = "Image" + DateTime.Now.Ticks + ".png",
                    ImageBytes = byteImg,
                    AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImg)))),

                });


                file.Dispose();
                workOrderWrapper workorderWrapper = new workOrderWrapper();
                workorderWrapper.attachments = new List<WorkOrderAttachment>();

                var ImagesPicsList1 = Attachments;
                foreach (var file1 in ImagesPicsList1)
                {
                    if (file1.WorkOrderAttachmentID == null && file1.IsSynced == false)
                    {
                        WorkOrderAttachment woattachment = new WorkOrderAttachment();
                        woattachment.WorkOrderID = WorkorderID;
                        //woattachment.ModifiedUserName = "Eagle4";
                        //  woattachment.attachmentFile = Convert.ToBase64String(file.ImageBytes);
                        woattachment.attachmentFile = Convert.ToBase64String(file1.ImageBytes);
                        woattachment.attachmentFileExtension = file1.attachmentFileExtension;
                        workorderWrapper.attachments.Add(woattachment);
                    }


                }
                //   var Count = workorderWrapper.attachments.Count;

                var status = await _attachmentService.CreateWorkorderAttachment(UserID, workorderWrapper);

                if (Boolean.Parse(status.servicestatus))
                {
                    foreach (var file1 in ImagesPicsList1)
                    {
                        if (file1.WorkOrderAttachmentID == null)
                        {
                            file1.IsSynced = true;


                        }

                    }
                    IsDataRequested = false;
                    await this.OnViewAppearingAsync(null);

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AttachmentSuccessfullySaved"), 2000);



                }
            }
            catch (Exception)
            {


                OperationInProgress = false;
            }

            finally
            {

                OperationInProgress = false;
            }
        }


        static List<String> GetBase64Strings(String path)
        {
            List<String> base64Strings = new List<String>();
            var files = Directory.GetFiles(path);
            if (files.Length != 0)
            {
                foreach (var filePath in files)
                {
                    Byte[] bytes = File.ReadAllBytes(filePath);
                    String base64String = Convert.ToBase64String(bytes);
                    base64Strings.Add(base64String);
                }
            }
            return base64Strings;
        }
        public async Task DeleteAttachment()
        {

            try
            {
                //  UserDialogs.Instance.ShowLoading();

                OperationInProgress = true;
                IsDataRequested = true;



                var result = await DialogService.ShowConfirmAsync(WebControlTitle.GetTargetNameByTitleName("Areyousuretodeletethisimage?"), string.Empty, WebControlTitle.GetTargetNameByTitleName("OK"), WebControlTitle.GetTargetNameByTitleName("Cancel"));
                if (!result)
                {
                    UserDialogs.Instance.HideLoading();
                    return;
                }


                var position = this.SelectedIndexItem;

                if (Attachments.Count == 0)
                {
                    UserDialogs.Instance.HideLoading();

                    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Thereisnoimagetodelete").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Thereisnoimagetodelete"), 2000);
                    return;
                }

                var attachment = Attachments[position];
                if (attachment.WorkOrderAttachmentID != null)
                {

                    var isRemoved = await RemoveAttachmentFromWeb(attachment.WorkOrderAttachmentID);
                    if (isRemoved)
                    {
                        Attachments.RemoveAt(position);
                    }


                }

                else
                {
                    Attachments.RemoveAt(position);
                }

                await OnViewAppearingAsync(null);
            }
            catch (Exception)
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
        async Task<bool> RemoveAttachmentFromWeb(int? WorkOrderAttachmentID)
        {

            var workorderWrapper = new workOrderWrapper
            {
                UserId = Convert.ToInt32(UserID),
                attachment = new WorkOrderAttachment
                {
                    WorkOrderAttachmentID = WorkOrderAttachmentID,
                    ModifiedUserName = AppSettings.User.UserName,

                },
            };



            ServiceOutput response = await _attachmentService.DeleteWorkorderAttachment(workorderWrapper);
            if (response != null && response.servicestatusmessge != null)
            {
                if (response.servicestatusmessge == "success")
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AttachmentSuccessfullyRemoved"), 2000);
                    return true;
                }

                else
                {

                    DialogService.ShowToast(response.servicestatusmessge, 2000);
                    return false;
                }

            }

            else
            {
                return false;
            }

        }
        public async Task SaveAttachment()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //  OperationInProgress = true;



                workOrderWrapper workorderWrapper = new workOrderWrapper();
                workorderWrapper.attachments = new List<WorkOrderAttachment>();

                var ImagesPicsList1 = Attachments;
                foreach (var file in ImagesPicsList1)
                {
                    if (file.WorkOrderAttachmentID == null && file.IsSynced == false)
                    {
                        WorkOrderAttachment woattachment = new WorkOrderAttachment();
                        woattachment.WorkOrderID = WorkorderID;



                        //woattachment.ModifiedUserName = "Eagle4";
                        woattachment.attachmentFile = Convert.ToBase64String(file.ImageBytes);
                        woattachment.attachmentFileExtension = file.attachmentFileExtension;
                        workorderWrapper.attachments.Add(woattachment);
                    }


                }
                //   var Count = workorderWrapper.attachments.Count;

                var status = await _attachmentService.CreateWorkorderAttachment(UserID, workorderWrapper);

                if (Boolean.Parse(status.servicestatus))
                {
                    foreach (var file in ImagesPicsList1)
                    {
                        if (file.WorkOrderAttachmentID == null)
                        {
                            file.IsSynced = true;


                        }

                    }
                    IsDataRequested = false;
                    await this.OnViewAppearingAsync(null);

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AttachmentSuccessfullySaved"), 2000);

                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
            }
        }
        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {
                OperationInProgress = true;

                try
                {


                    #region Remove all existing image and documents from ViewModel
                    if (Attachments != null && Attachments.Count > 0)
                    {
                        var count = Attachments.Count;
                        for (int i = 1; i <= count; i++)
                        {
                            Attachments.RemoveAt(count - i);
                        }

                    }

                    if (DocumentAttachments != null && DocumentAttachments.Count > 0)
                    {
                        var count = DocumentAttachments.Count;
                        for (int i = 1; i <= count; i++)
                        {
                            DocumentAttachments.RemoveAt(count - i);
                        }

                    }
                    #endregion


                    var attachment = await _attachmentService.GetWorkorderAttachments(UserID, WorkorderID.ToString());


                    if (attachment.workOrderWrapper != null && attachment.workOrderWrapper.attachments != null)
                    {

                        if (attachment.workOrderWrapper.attachments.Count > 0)
                        {
                            var FirstattachmentName = attachment.workOrderWrapper.attachments.First();

                            ImageText = WebControlTitle.GetTargetNameByTitleName("Total") + " " + WebControlTitle.GetTargetNameByTitleName("Image") + " : " + attachment.workOrderWrapper.attachments.Count;
                            foreach (var file in attachment.workOrderWrapper.attachments)
                            {

                                if (file.attachmentFileExtension != null &&
                                    (file.attachmentFileExtension.ToLower().Contains(".pdf") ||
                                    file.attachmentFileExtension.ToLower().Contains(".doc") ||
                                    file.attachmentFileExtension.ToLower().Contains(".docx") ||
                                    file.attachmentFileExtension.ToLower().Contains(".xls") ||
                                    file.attachmentFileExtension.ToLower().Contains(".xlsx") ||
                                    file.attachmentFileExtension.ToLower().Contains(".txt")))
                                {
                                    PDFImageText = FirstattachmentName.attachmentFileExtension;
                                    DocumentAttachments.Add(file.attachmentFileExtension);

                                    byte[] imgUser = StreamToBase64.StringToByte(ShortString.shortenBase64(""));
                                    Attachments.Add(new WorkorderAttachment
                                    {
                                        IsSynced = true,
                                        attachmentFileExtension = file.attachmentFileExtension,
                                        //ImageBytes = imgUser, //byteImage,
                                        WorkOrderAttachmentID = file.WorkOrderAttachmentID,
                                        AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(imgUser)))),

                                    }
                                       );
                                }
                                else
                                {
                                    if (Device.RuntimePlatform == Device.UWP)
                                    {
                                        byte[] imgUser = StreamToBase64.StringToByte(file.attachmentFile);
                                        MemoryStream stream = new MemoryStream(imgUser);
                                        bool isimage = Extension.IsImage(stream);
                                        if (isimage == true)
                                        {

                                            byte[] byteImage = await Xamarin.Forms.DependencyService.Get<IResizeImage>().ResizeImageAndroid(imgUser, 350, 350);


                                            Attachments.Add(new WorkorderAttachment
                                            {
                                                IsSynced = true,
                                                attachmentFileExtension = file.attachmentFileExtension,
                                                //ImageBytes = imgUser, //byteImage,
                                                WorkOrderAttachmentID = file.WorkOrderAttachmentID,
                                                AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImage)))),

                                            }
                                            );
                                            //}


                                        }
                                    }
                                    else if (Device.RuntimePlatform == Device.Android)
                                    {
                                        byte[] imgUser = StreamToBase64.StringToByte(file.attachmentFile);
                                        MemoryStream stream = new MemoryStream(imgUser);
                                        bool isimage = Extension.IsImage(stream);
                                        if (isimage == true)
                                        {

                                            Attachments.Add(new WorkorderAttachment
                                            {
                                                IsSynced = true,
                                                attachmentFileExtension = file.attachmentFileExtension,
                                                //ImageBytes = imgUser, //byteImage,
                                                WorkOrderAttachmentID = file.WorkOrderAttachmentID,
                                                AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(imgUser)))),

                                            }
                                            );

                                        }
                                    }
                                    else
                                    {


                                        Attachments.Add(new WorkorderAttachment
                                        {
                                            IsSynced = true,
                                            attachmentFileExtension = file.attachmentFileExtension,
                                            //ImageBytes = imgUser, //byteImage,
                                            WorkOrderAttachmentID = file.WorkOrderAttachmentID,
                                            AttachmentImageSource = ImageSource.FromUri(new Uri(AppSettings.BaseURL + "/Inspection/Service/AttachmentItem.ashx?Id=" + file.WorkOrderAttachmentID + "&&Module=workorder"))

                                        }
                                        );


                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ImageText = WebControlTitle.GetTargetNameByTitleName("Total") + " " + WebControlTitle.GetTargetNameByTitleName("Image") + " : " + 0;
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
            PDFImageText = "";
            return Task.FromResult(true);
        }

        public async Task<ServiceOutput> ServiceCallWebClient(string url, string mtype, IDictionary<string, string> urlSegment, object jsonString)
        {
            ServiceOutput responseContent = new ServiceOutput();
            try
            {

                if (!string.IsNullOrEmpty(url))
                {
                    string segurl = string.Empty;
                    if (urlSegment != null)
                    {
                        foreach (KeyValuePair<string, string> entry in urlSegment)
                        {
                            segurl = segurl + "/" + entry.Value;
                        }
                    }

                    if (!string.IsNullOrEmpty(segurl))
                    {
                        url = url + segurl;
                    }



                    if (!string.IsNullOrEmpty(mtype))
                    {
                        if (mtype.ToLower().Equals("get"))
                        {


                            HttpClient client = new HttpClient();
                            client.BaseAddress = new Uri(url);

                            // Add an Accept header for JSON format.
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));

                            HttpResponseMessage response = client.GetAsync(url).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                var content = JsonConvert.DeserializeObject(
                                        response.Content.ReadAsStringAsync()
                                        .Result);


                                responseContent = JsonConvert.DeserializeObject<ServiceOutput>(content.ToString());

                            }


                        }
                        else if (mtype.ToLower().Equals("post"))
                        {
                            try
                            {
                                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                                httpWebRequest.ContentType = "application/json";
                                httpWebRequest.Method = mtype;
                                var stream = await httpWebRequest.GetRequestStreamAsync();
                                string Json = JsonConvert.SerializeObject(jsonString);
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(Json);
                                    writer.Flush();
                                    writer.Dispose();
                                }

                                using (HttpWebResponse response = await httpWebRequest.GetResponseAsync() as HttpWebResponse)
                                {
                                    if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        //  Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                                        {
                                            var content = reader.ReadToEnd();

                                            responseContent = JsonConvert.DeserializeObject<ServiceOutput>(content.ToString());
                                        }
                                    }


                                }

                            }
                            catch (WebException ex)
                            {

                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }
            }



            catch (Exception ex)
            {

                throw ex;
            }

            return responseContent;
        }
        #endregion

        static async Task SendURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = u,
                    Content = c
                };

                HttpResponseMessage result = await client.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }

        }

        public void LogMessage(string FinalLogstring)
        {
            FinalLogstring = FinalLogstring + " " + DateTime.UtcNow.ToString();
            Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/Service/CreateLogs");

            var payload = new LogModel()
            {
                Message = FinalLogstring,
                FileName = "PdfUplodMobileError.txt"

            };

            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => SendURI(posturi, c));

        }
    }



    public class WorkorderAttachment
    {
        public bool IsSynced { get; set; }
        public byte[] ImageBytes { get; set; }
        public string attachmentFileExtension { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public ImageSource AttachmentImageSource { get; set; }
        public int? WorkOrderAttachmentID { get; set; }
    }

}
