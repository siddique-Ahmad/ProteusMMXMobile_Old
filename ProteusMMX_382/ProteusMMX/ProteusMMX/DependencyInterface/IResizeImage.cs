using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.DependencyInterface
{
    public interface IResizeImage
    {
        Task<byte[]> ResizeImageAndroid(byte[] imageData, float width, float height);
    }
}
