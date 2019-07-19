using System;
using System.Collections.Generic;
using System.Text;
using ProteusMMX;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ProteusMMX.Model.CommonModels;


namespace ProteusMMX.ViewModel
{
    public class DescriptionViewModel : ViewModelBase
    {
        string _DescriptionText;
        public string DescriptionText
        {
            get
            {
                return _DescriptionText;
            }

            set
            {
                if (value != _DescriptionText)
                {
                    _DescriptionText = value;
                    OnPropertyChanged(nameof(DescriptionText));
                }
            }
        }
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                if (navigationData != null)
                {

                    var navigationParams = navigationData as TargetNavigationData;

                    //Set Facility
                    if (navigationParams.Description != null)
                    {
                        this.DescriptionText = navigationParams.Description;
                        
                    }

                }
            }
            catch
            {

            }
        }
       

        
    }

                       
}
