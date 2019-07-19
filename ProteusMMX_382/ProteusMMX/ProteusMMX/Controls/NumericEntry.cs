using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Controls
{
    /// <summary>
    /// Entry for Decimal type data.
    /// </summary>
    public class NumericEntry : Entry
    {
        public NumericEntry()
        {
            this.Keyboard = Keyboard.Numeric;
            this.TextChanged += NumericEntry_TextChanged;
        }

        private void NumericEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(val))
                {
                    Decimal.Parse(val); 
                }
            }
            catch (Exception ex)
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }

        }
    }
}
