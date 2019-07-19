using ProteusMMX.Model.WorkOrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.CommonModels
{
    class ValidationResult
    {
 

        public FormControl FailedItem { get; set; }

        public string ErrorMessage { get; set; }

        //string _errorMessage;
        //public string ErrorMessage
        //{
        //    get
        //    {
        //       return FailedItem.FieldLabel + " " + IsRequiredFieldText;
        //    }
        //}
    }
        
}
