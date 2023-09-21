using Newtonsoft.Json;
using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Helpers
{
    public class TrackSystemLog
    {
        public static void LogMessage(string FinalLogstring)
        {
            FinalLogstring = FinalLogstring + " " + System.DateTime.UtcNow.ToString();
            FinalLogstring = FinalLogstring + " ----------------------";
            Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/Service/CreateLogs");

            var payload = new LogModel()
            {
                Message = FinalLogstring,
                FileName = "MMXMobileLog.txt"

            };

            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => SendURI(posturi, c));


        }
        static async Task SendURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = u,
                    Content = c
                };

                HttpResponseMessage result = await client.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }

        }
    }
}
