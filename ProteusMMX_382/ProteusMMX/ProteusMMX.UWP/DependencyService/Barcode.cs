using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Mobile;

namespace ProteusMMX.UWP.DependencyService
{
     public class Barcode
    {
        public async Task<string> BarcodeScan()
        {

            string barcode = null;

            MainPage main = new MainPage();
            main.SetScanner();
            MobileBarcodeScanner scanner = main.GetScanner();

            Result result = await scanner.Scan();
            if (result != null)
            {
                barcode = result.Text;

            }

            return barcode;
        }
    }
}
