using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Helpers.Attachment
{
    public static class StreamToBase64
    {
        public static string StreamToBase64String(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                //ms.ToArray();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static Stream StringFromBase64Stream(string input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] b = Convert.FromBase64String(input);
                //ms.ToArray();

                return new MemoryStream(b);
            }
        }

        public static byte[] StringToByte(string input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] b = Convert.FromBase64String(input);
                //ms.ToArray();

                return b;
            }
        }
        //public static byte[] ReadAllBytes(string fileName)
        //{
        //    byte[] buffer = null;
        //    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        //    {
        //        buffer = new byte[fs.Length];
        //        fs.Read(buffer, 0, (int)fs.Length);
        //    }
        //    return buffer;
        //}

        public static byte[] FileToByte(MediaFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.GetStream().CopyTo(ms);
                file.Dispose();
                //ms.ToArray();

                return ms.ToArray();
            }
        }
        public static byte[] FileToByte1(Stream Path)
        {
            using (var streamReader = new StreamReader(Path))
            {
                var bytes = default(byte[]);
                using (var memstream = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memstream);
                    bytes = memstream.ToArray();
                    return bytes;

                }
            }

        }
        //public static MemoryStream GetPage(int pageNum)
        //{
        //    if (pdfDocumentView != null && pageNum >= 0 && pageNum < pdfDocumentView.PageCount)
        //    {
        //        //Export the PDF pages to image
        //        Bitmap bmp = pdfDocumentView.ExportAsImage(pageNum);
        //        MemoryStream stream = new MemoryStream();
        //        //Save the exported PDF page as Image
        //        bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //        return stream;
        //    }
        //    return null;
        //}
    }
}
