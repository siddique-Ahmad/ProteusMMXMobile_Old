using ProteusMMX.Model.WorkOrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ServiceRequestModel
{
    public class ServiceRequestFormLoad
    {
        public string FieldName { get; set; }
        public string Expression { get; set; }
        public bool IsRequired { get; set; }
        public string FieldLabel { get; set; }
        public string DisplayFormat { get; set; }
        public string FieldLocation { get; set; }
        public int FieldOrder { get; set; }
        public string ControlValue { get; set; }
        public List<ComboDD> listCombo { get; set; }
    }
}
