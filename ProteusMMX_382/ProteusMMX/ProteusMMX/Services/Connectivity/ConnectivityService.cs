using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Connectivity
{
    public class ConnectivityService : IConnectivityService
    {

        public bool IsConnected { get; set; }
        public ConnectivityService()
        {
            this.IsConnected = CrossConnectivity.Current.IsConnected;

            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                this.IsConnected = args.IsConnected;
                Debug.WriteLine($"Connectivity changed to {args.IsConnected}");
            };
        }
     
    }
}
