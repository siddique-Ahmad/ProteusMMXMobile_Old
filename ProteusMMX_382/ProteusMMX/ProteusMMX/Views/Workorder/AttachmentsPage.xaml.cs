
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;

using ProteusMMX.Helpers;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Views;
using Xamarin.Forms;

using System.ComponentModel;
using ProteusMMX.ViewModel;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using Xamarin.Forms.Xaml;
using ProteusMMX.Model;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
using ProteusMMX.Services.Workorder.Attachments;
using System.Net.Http;
using System.Net.Http.Headers;
using ProteusMMX.DependencyInterface;

namespace ProteusMMX.Views.Workorder
{
     [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class AttachmentsPage : ContentPage
    {

        AttachmentsPageViewModel ViewModel => this.BindingContext as AttachmentsPageViewModel;
        public int? WorkorderID { get; set; }
        public string UserID { get; set; }
      //  AttachmentsPageViewModel ViewModel => this.BindingContext as AttachmentsPageViewModel;
        public AttachmentsPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            this.IconImageSource = "Attachements.png";
          


        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }



         

        }
      

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
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

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            var a = button.BindingContext as ProteusMMX.ViewModel.Workorder.WorkorderAttachment;
            var fileextension = a.attachmentFileExtension;

            if (fileextension != null &&
                                   (fileextension.ToLower().Contains(".pdf") ||
                                   fileextension.ToLower().Contains(".doc") ||
                                   fileextension.ToLower().Contains(".docx") ||
                                   fileextension.ToLower().Contains(".xls") ||
                                   fileextension.ToLower().Contains(".xlsx") ||
                                   fileextension.ToLower().Contains(".txt")))
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + fileextension);
                        break;
                    case Device.Android:
                        DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + fileextension);

                        break;
                    case Device.UWP:
                        DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + fileextension);
                        break;
                    default:
                        break;
                }
            }
        }
    }



}