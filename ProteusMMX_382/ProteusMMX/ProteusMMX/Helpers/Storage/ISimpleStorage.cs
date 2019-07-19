using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Helpers.Storage
{
    public interface ISimpleStorage
    {
        void Set(string key, string value);
        string Get(string key);
        void Delete(string key);
    }
}
