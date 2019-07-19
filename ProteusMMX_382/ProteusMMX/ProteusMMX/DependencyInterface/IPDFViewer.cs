using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.DependencyInterface
{
    public interface IPDFViewer
    {
        void OpenPDF(string path); //note that interface members are public by default
    }
}
