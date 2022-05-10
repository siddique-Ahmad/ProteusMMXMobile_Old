using ProteusMMX.Controls;
using ProteusMMX.Helpers.Attachment;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SignaturePad.Forms;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Common
{
     [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class SignaturePage : PopupPage
    {

        public event EventHandler<SignaturePageModel> OnSignatureDrawn;
        CustomImage imageview;
        public SignaturePage(CustomImage imageview)
        {
            InitializeComponent();
            this.imageview = imageview;
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //OnDispose?.Invoke();
        }

        private async void OK_Clicked(object sender, EventArgs e)
        {
            var _stream = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png, false, false);

            //OnSignatureDrawn.Invoke(this, Convert.ToBase64String(StreamToByteArrary(_stream)));
            //OnSignatureDrawn.Invoke(this, StreamToBase64.StreamToBase64String(_stream));

            if (_stream == null)
            {
                OnSignatureDrawn.Invoke(this, new SignaturePageModel() { SignatureBase64 = null, ImageView = this.imageview });
                OnClose(null, null);
            }

            else
            {
                OnSignatureDrawn.Invoke(this, new SignaturePageModel() { SignatureBase64 = StreamToBase64.StreamToBase64String(_stream), ImageView = this.imageview });
                OnClose(null, null);
            }

        }


    }

    public class SignaturePageModel
    {
        public CustomImage ImageView { get; set; }
        public string SignatureBase64 { get; set; }

    }
}