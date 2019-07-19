using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ProteusMMX.Helpers
{
    public class WebControlTitle
    {
        #region Old Code
        //public static string GetTargetNameByTitleName(ServiceOutput formLoadInputs, string TitleName)
        //{
        //    try
        //    {
        //        var title = formLoadInputs.listWebControlTitles.FirstOrDefault(i => i.TitleName == TitleName).TargetName;
        //        return title;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.StackTrace + "<<<<<<<<<>>>>>>>>" + ex.Message);
        //        return "";
        //    }
        //} 
        #endregion




        public static string GetTargetNameByTitleName(string TitleName)
        {
            try
            {
                string title = "";
                AppSettings.Translations.TryGetValue(TitleName, out title);
                return title;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace + "<<<<<<<<<>>>>>>>>" + ex.Message);
                return "";
            }
        }
    }
}
