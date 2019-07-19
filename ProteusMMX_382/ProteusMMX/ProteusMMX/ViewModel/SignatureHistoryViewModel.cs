using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProteusMMX.ViewModel
{
    public class SignatureHistoryViewModel : ViewModelBase
    {
        string _signature;
        public string Signature
        {
            get
            {
                return _signature;
            }

            set
            {
                if (value != _signature)
                {
                    _signature = value;
                    OnPropertyChanged(nameof(Signature));
                }
            }
        }
        string _signatureIntent;
        public string SignatureIntent
        {
            get
            {
                return _signatureIntent;
            }

            set
            {
                if (value != _signatureIntent)
                {
                    _signatureIntent = value;
                    OnPropertyChanged(nameof(SignatureIntent));
                }
            }
        }
        string _signatureTimestamp;
        public string SignatureTimestamp
        {
            get
            {
                return _signatureTimestamp;
            }

            set
            {
                if (value != _signatureTimestamp)
                {
                    _signatureTimestamp = value;
                    OnPropertyChanged(nameof(SignatureTimestamp));
                }
            }
        }

        ObservableCollection<SignatureAuditDetail> _signatureHistoryCollection = new ObservableCollection<SignatureAuditDetail>();

        public ObservableCollection<SignatureAuditDetail> SignatureHistoryCollection
        {
            get
            {
                return _signatureHistoryCollection;
            }

        }
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                if (navigationData != null)
                {

                    var navigationParams = navigationData as TargetNavigationData;
                    List<SignatureAuditDetail> collection = (List<SignatureAuditDetail>)navigationData;
                    var signaturecollection = collection;
                    await AdditemsinSignatureCollection(signaturecollection);
                    
                   

                }
            }
            catch
            {

            }
        }
        private async Task AdditemsinSignatureCollection(List<SignatureAuditDetail> signature)
        {
            if (signature != null && signature.Count > 0)
            {
                foreach (var item in signature)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _signatureHistoryCollection.Add(item);
                        OnPropertyChanged(nameof(SignatureHistoryCollection));
                    });



                }

            }
        }
    }
}


