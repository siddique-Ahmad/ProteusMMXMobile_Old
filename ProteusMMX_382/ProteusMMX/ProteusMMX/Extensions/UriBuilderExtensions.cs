using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProteusMMX.Extensions
{
    internal static class UriBuilderExtensions
    {
        internal static void AppendToPath(this UriBuilder builder, string pathToAdd)
        {
            var completePath = Path.Combine(builder.Path, pathToAdd);
            builder.Path = completePath;
        }
    }
}
