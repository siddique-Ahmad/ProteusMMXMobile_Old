﻿using Acr.UserDialogs;
using Plugin.FilePicker;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Attachment;
using ProteusMMX.Model;
using ProteusMMX.Model.ServiceRequestModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder.Attachments;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProteusMMX.ViewModel.ServiceRequest
{
	public class ServiceRequestAttachmentPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
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
        
        int? _serviceRequestID;
        public int? ServiceRequestID
        {
            get
            {
                return _serviceRequestID;
            }

            set
            {
                if (value != _serviceRequestID)
                {
                    _serviceRequestID = value;
                    OnPropertyChanged(nameof(ServiceRequestID));
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

        public ObservableCollection<ServicerequestAttachment> Attachments { get; set; }
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

        public ICommand GalleryCommand => new AsyncCommand(OpenGallery);

        public ICommand FileCommand => new AsyncCommand(OpenFile);
        public ICommand DeleteCommand => new AsyncCommand(DeleteAttachment);
        public ICommand SaveCommand => new AsyncCommand(SaveAttachment);
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


                    var servicerequest = navigationParams.Parameter as ServiceRequests;
                    this.ServiceRequestID = servicerequest.ServiceRequestID;



                }

                //FormLoadInputForWorkorder = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                await SetTitlesPropertiesForPage();

                if (Application.Current.Properties.ContainsKey("CreateSRAttachment"))
                {
                    CreateAttachment = Application.Current.Properties["CreateSRAttachment"].ToString();
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
                if (Application.Current.Properties.ContainsKey("RemoveSRAttachment"))
                {
                    DeleteAttachments = Application.Current.Properties["RemoveSRAttachment"].ToString();
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

        public ServiceRequestAttachmentPageViewModel(IAuthenticationService authenticationService, IAttachmentService attachmentService, IFormLoadInputService formLoadInputService)
        {
            _authenticationService = authenticationService;
            _attachmentService = attachmentService;
            _formLoadInputService = formLoadInputService;
            Attachments = new ObservableCollection<ServicerequestAttachment>
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

                 // PageTitle = WebControlTitle.GetTargetNameByTitleName("Attachments");
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
                //Device.OpenUri(new Uri(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + action));
                DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + action);

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
        public async Task OpenFile()
        {
            IsDataRequested = true;
            await PickFile();
        }
        public async Task OpenGallery()
        {
            IsDataRequested = true;
            await PickPhoto();
        }
        public async Task OpenMedia()
        {
            IsDataRequested = true;
            await TakePhoto();
            //try
            //{
            //    List<string> choice = new List<string>() { WebControlTitle.GetTargetNameByTitleName("Camera"), WebControlTitle.GetTargetNameByTitleName("Gallery")};
            //    var selected = await DialogService.SelectActionAsync(WebControlTitle.GetTargetNameByTitleName("ChooseFile"), SelectTitle, CancelTitle, choice.ToArray());
            //    if (selected != null && !selected.Equals(WebControlTitle.GetTargetNameByTitleName("Cancel")))
            //    {

            //        if (selected.Equals(WebControlTitle.GetTargetNameByTitleName("Camera")))
            //        {
            //            IsDataRequested = true;
            //            await TakePhoto();
            //        }

            //        else if (selected.Equals(WebControlTitle.GetTargetNameByTitleName("Gallery")))
            //        {
            //            IsDataRequested = true;
            //            await PickPhoto();
            //        }
            //        else
            //        {
            //            IsDataRequested = true;
            //            await PickFile();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    OperationInProgress = false;
            //}

            //finally
            //{
            //    OperationInProgress = false;
            //}

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

                Attachments.Add(new ServicerequestAttachment
                {
                    IsSynced = false,
                    attachmentFileExtension = "Image" + DateTime.Now.Ticks + ".png",
                    ImageBytes = byteImg,
                    AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImg)))),

                });


                file.Dispose();
                ServiceRequestWrapper serviceRequestWrapper = new ServiceRequestWrapper();
                serviceRequestWrapper.attachments = new List<ServiceRequestAttachment>();


                var ImagesPicsList1 = Attachments;
                foreach (var file1 in ImagesPicsList1)
                {
                    if (file1.ServiceRequestAttachmentID == null && file1.IsSynced == false)
                    {
                        ServiceRequestAttachment srattachment = new ServiceRequestAttachment();
                        srattachment.ServiceRequestID = this.ServiceRequestID;
                        srattachment.attachmentFile = Convert.ToBase64String(file1.ImageBytes);
                        srattachment.attachmentFileExtension = file1.attachmentFileExtension;
                        serviceRequestWrapper.attachments.Add(srattachment);

                    }


                }
                var Count = serviceRequestWrapper.attachments.Count;

                var status = await _attachmentService.CreateServiceRequestAttachment(UserID, serviceRequestWrapper);

                if (Boolean.Parse(status.servicestatus))
                {
                    foreach (var file1 in ImagesPicsList1)
                    {
                        if (file1.ServiceRequestAttachmentID == null)
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
        public async Task PickFile()
        {

            try
            {
                OperationInProgress = true;
                await PickAndShow1();

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

        async Task<FileResult> PickAndShow1()
        {
            try
            {
                var customFileType =
                                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                                {
                                    { DevicePlatform.iOS, new[] { "com.microsoft.word.doc","org.openxmlformats.wordprocessingml.document", "org.openxmlformats.spreadsheetml.sheet", "com.adobe.pdf" } }, // or general UTType values
                                    { DevicePlatform.Android, new[] { "application/vnd.openxmlformats-officedocument.wordprocessingml.document","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "application/pdf" } },
                                    { DevicePlatform.UWP, new[] { ".docx" } },
                                    { DevicePlatform.Tizen, new[] { "*/*" } },
                                    { DevicePlatform.macOS, new[] { "doc" } }, // or general UTType values
                                });


                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = customFileType,
                    PickerTitle = "Please select a file"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var fdata2 = result.FullPath;
                    var rdata = System.IO.File.ReadAllBytes(fdata2);
                    string base64String = Convert.ToBase64String(rdata);
                    ServiceRequestWrapper serviceRequestWrapper = new ServiceRequestWrapper();
                    serviceRequestWrapper.attachments = new List<ServiceRequestAttachment>();
                    ServiceRequestAttachment Serviceattachment = new ServiceRequestAttachment();
                    Serviceattachment.ServiceRequestID = ServiceRequestID;
                    Serviceattachment.attachmentFile = base64String;
                    Serviceattachment.attachmentFileExtension = result.FileName;
                    serviceRequestWrapper.attachments.Add(Serviceattachment);
                    //FinalLogstring = FinalLogstring + "Rady CreateWorkorderAttachment userId :" + UserID;
                    var status = await _attachmentService.CreateServiceRequestAttachment(UserID, serviceRequestWrapper);

                    if (Boolean.Parse(status.servicestatus))
                    {
                        //FinalLogstring = FinalLogstring + "If status.servicestatus   " + status.servicestatus;
                        IsDataRequested = false;
                        await this.OnViewAppearingAsync(null);
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AttachmentSuccessfullySaved"), 2000);
                    }


                }

                //return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
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

                Attachments.Add(new ServicerequestAttachment
                {
                    IsSynced = false,
                    attachmentFileExtension = "Image" + DateTime.Now.Ticks + ".png",
                    ImageBytes = byteImg,
                    AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImg)))),

                });


                file.Dispose();
                ServiceRequestWrapper serviceRequestWrapper = new ServiceRequestWrapper();
                serviceRequestWrapper.attachments = new List<ServiceRequestAttachment>();


                var ImagesPicsList1 = Attachments;
                foreach (var file1 in ImagesPicsList1)
                {
                    if (file1.ServiceRequestAttachmentID == null && file1.IsSynced == false)
                    {
                        ServiceRequestAttachment srattachment = new ServiceRequestAttachment();
                        srattachment.ServiceRequestID = this.ServiceRequestID;
                        srattachment.attachmentFile = Convert.ToBase64String(file1.ImageBytes);
                        srattachment.attachmentFileExtension = file1.attachmentFileExtension;
                        serviceRequestWrapper.attachments.Add(srattachment);

                    }


                }
                var Count = serviceRequestWrapper.attachments.Count;

                var status = await _attachmentService.CreateServiceRequestAttachment(UserID, serviceRequestWrapper);

                if (Boolean.Parse(status.servicestatus))
                {
                    foreach (var file1 in ImagesPicsList1)
                    {
                        if (file1.ServiceRequestAttachmentID == null)
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
                if (attachment.ServiceRequestAttachmentID != null)
                {

                    var isRemoved = await RemoveAttachmentFromWeb(attachment.ServiceRequestAttachmentID);
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
        async Task<bool> RemoveAttachmentFromWeb(int? ServiceRequestAttachmentID)
        {

            var serviceRequestWrapper = new ServiceRequestWrapper
            {
                UserId = Convert.ToInt32(UserID),
                attachment = new ServiceRequestAttachment
                {
                    ServiceRequestAttachmentID = ServiceRequestAttachmentID,
                    ModifiedUserName = AppSettings.User.UserName,

                },
            };



            ServiceOutput response = await _attachmentService.DeleteServiceRequestAttachment(serviceRequestWrapper);
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
            return false;

        }
        public async Task SaveAttachment()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //  OperationInProgress = true;



                //workOrderWrapper workorderWrapper = new workOrderWrapper();
                //workorderWrapper.attachments = new List<WorkOrderAttachment>();

                //var ImagesPicsList1 = Attachments;
                //foreach (var file in ImagesPicsList1)
                //{
                //    if (file.WorkOrderAttachmentID == null && file.IsSynced == false)
                //    {
                //        WorkOrderAttachment woattachment = new WorkOrderAttachment();
                //        woattachment.WorkOrderID = WorkorderID;
                //        //woattachment.ModifiedUserName = "Eagle4";
                //        woattachment.attachmentFile = Convert.ToBase64String(file.ImageBytes);
                //        woattachment.attachmentFileExtension = file.attachmentFileExtension;
                //        workorderWrapper.attachments.Add(woattachment);
                //    }


                //}
                ////   var Count = workorderWrapper.attachments.Count;

                //var status = await _attachmentService.CreateWorkorderAttachment(UserID, workorderWrapper);

                //if (Boolean.Parse(status.servicestatus))
                //{
                //    foreach (var file in ImagesPicsList1)
                //    {
                //        if (file.WorkOrderAttachmentID == null)
                //        {
                //            file.IsSynced = true;


                //        }

                //    }
                //    IsDataRequested = false;
                //    await this.OnViewAppearingAsync(null);

                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AttachmentSuccessfullySaved"), 2000);

                //}

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

                //if (IsDataRequested)
                //{
                //    IsDataRequested = false;
                //    return;
                //}

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


                    var attachment = await _attachmentService.GetServiceRequestAttachments(UserID, this.ServiceRequestID.ToString());


                    if (attachment.serviceRequestWrapper != null && attachment.serviceRequestWrapper.attachments != null)
                    {

                        if (attachment.serviceRequestWrapper.attachments.Count > 0)
                        {
                            var FirstattachmentName = attachment.serviceRequestWrapper.attachments.First();
                            //PDFImageText = FirstattachmentName.attachmentFileExtension;
                            ImageText = WebControlTitle.GetTargetNameByTitleName("Total") + " " + WebControlTitle.GetTargetNameByTitleName("Image") + " : " + attachment.serviceRequestWrapper.attachments.Count;
                            foreach (var file in attachment.serviceRequestWrapper.attachments)
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
                                    Attachments.Add(new ServicerequestAttachment
                                    {
                                        IsSynced = true,
                                        attachmentFileExtension = file.attachmentFileExtension,
                                        //ImageBytes = imgUser, //byteImage,
                                        ServiceRequestAttachmentID = file.ServiceRequestAttachmentID,
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


                                            Attachments.Add(new ServicerequestAttachment
                                            {
                                                IsSynced = true,
                                                attachmentFileExtension = file.attachmentFileExtension,
                                                //ImageBytes = imgUser, //byteImage,
                                                ServiceRequestAttachmentID = file.ServiceRequestAttachmentID,
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

                                            Attachments.Add(new ServicerequestAttachment
                                            {
                                                IsSynced = true,
                                                attachmentFileExtension = file.attachmentFileExtension,
                                                //ImageBytes = imgUser, //byteImage,
                                                ServiceRequestAttachmentID = file.ServiceRequestAttachmentID,
                                                AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(imgUser)))),

                                            }
                                            );

                                        }
                                    }
                                    else
                                    {


                                        Attachments.Add(new ServicerequestAttachment
                                        {
                                            IsSynced = true,
                                            attachmentFileExtension = file.attachmentFileExtension,
                                            //ImageBytes = imgUser, //byteImage,
                                            ServiceRequestAttachmentID = file.ServiceRequestAttachmentID,
                                            AttachmentImageSource = ImageSource.FromUri(new Uri(AppSettings.BaseURL + "/Inspection/Service/AttachmentItem.ashx?Id=" + file.ServiceRequestAttachmentID + "&&Module=servicerequest"))

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
            return Task.FromResult(true);
        }

      
        #endregion
    }
    public class ServicerequestAttachment
    {
        public bool IsSynced { get; set; }
        public byte[] ImageBytes { get; set; }
        public string attachmentFileExtension { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public ImageSource AttachmentImageSource { get; set; }
        public int? ServiceRequestAttachmentID { get; set; }
    }
}