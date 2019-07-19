using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Connectivity
{
    public interface IConnectivityService
    {
        bool IsConnected { get; set; }
    }
}
