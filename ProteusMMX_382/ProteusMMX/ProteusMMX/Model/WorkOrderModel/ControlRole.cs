using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{

    public class ControlRole
    {
        public string RoleName { get; set; }
        public ControlRight RoleRight { get; set; }
    }
    public enum ControlRight
    {
        Edit,
        View,
        None
    }




}
