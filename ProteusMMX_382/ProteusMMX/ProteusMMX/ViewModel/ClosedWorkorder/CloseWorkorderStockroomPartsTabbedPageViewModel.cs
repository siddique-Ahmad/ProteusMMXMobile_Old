﻿using ProteusMMX.Model;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.CloseWorkorder;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Workorder;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.ViewModel.ClosedWorkorder
{
    public class CloseWorkorderStockroomPartsTabbedPageViewModel : ViewModelBase
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        public readonly INavigationService _navigationservice;

        public readonly IWorkorderService _workorderService;

        public readonly ICloseWorkorderService _closeWorkorderService;
        #endregion



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

        #region Methods
        public CloseWorkorderStockroomPartsTabbedPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, INavigationService navigationservice, IWorkorderService workorderService, ICloseWorkorderService closeWorkorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _navigationservice = navigationservice;
            _workorderService = workorderService;
            _closeWorkorderService = closeWorkorderService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {



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
        #endregion
    }
}
