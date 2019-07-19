using ProteusMMX.Model.WorkOrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.CommonModels
{
    public class DialogTab
    {
        public int RuleID { get; set; }
        public string DialogTabName { get; set; }
        public string TargetName { get; set; }
        public string Expression { get; set; }
        public List<FormControl> listControls { get; set; }
        public List<TabDialog> listTabDialog { get; set; }

        public List<ButtonControls> ButtonControls { get; set; }
    }
    public class ButtonControls
    {
        public int RuleID { get; set; }
        public string Name { get; set; }
    
        public string Expression { get; set; }
    }

    public class FormControl
    {
        public int RuleID { get; set; }
        public string ControlName { get; set; }
        public string TargetName { get; set; }
        public string DataType { get; set; }
        public int? DecimalPlace { get; set; }
        public string DisplayFormat { get; set; }
        public bool? IsRequired { get; set; }
        public string FieldLocation { get; set; }
        public int? FieldOrder { get; set; }
        public string Expression { get; set; }
        public List<ComboDD> listCombo { get; set; }
    }

    public class Module
    {
        public int RuleID { get; set; }
        public string ModuleName { get; set; }
        public string TargetName { get; set; }

        public string Expression { get; set; }
        public List<SubModule> lstSubModules { get; set; }
    }

    public class FormListButton
    {
        public int RuleID { get; set; }
        public string Name { get; set; }
        public string EnglishUs { get; set; }
        public string Expression { get; set; }
    }
    public class SubModule
    {
        public int RuleID { get; set; }
        public string SubModuleName { get; set; }
        public string TargetName { get; set; }
        public string Expression { get; set; }
        public List<SubModuleDialog> listDialoges { get; set; }
        public List<FormControl> listControls { get; set; }
        public List<FormListButton> Button { get; set; }
    }

    public class SubModuleDialog
    {
        public int RuleID { get; set; }
        public string DialogName { get; set; }
        public string TargetName { get; set; }
        public string Expression { get; set; }
        public List<DialogTab> listTab { get; set; }
        public List<FormControl> listControls { get; set; }

        public List<FormListButton> Button { get; set; }
    }

    public class TabDialog
    {
        public int RuleID { get; set; }
        public string TabDialogName { get; set; }
        public string TargetName { get; set; }
        public string Expression { get; set; }
        public List<FormControl> listControls { get; set; }
        public List<TabDialogChild> listTab { get; set; }

        public List<ButtonControls> ButtonControls { get; set; }
    }

    public class TabDialogChild
    {
        public int RuleID { get; set; }
        public string Name { get; set; }
        public string TargetName { get; set; }
        public string Expression { get; set; }
        public List<FormControl> listControls { get; set; }
    }
}
