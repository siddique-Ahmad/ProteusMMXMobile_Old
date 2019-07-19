using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.ViewModel
{
    public class ExtendedSplashViewModel : ViewModelBase
    {
        public override async Task InitializeAsync(object navigationData)
        {
            OperationInProgress = true;
            await NavigationService.InitializeAsync();
            OperationInProgress = false;
        }
    }
}
