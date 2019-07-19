using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Diagnostics;
using ProteusMMX.Model;

namespace ProteusMMX.Services.Request
{
    public class RequestService : IRequestService
    {
        public async Task<ServiceOutput> GetAsync(string url)
        {
            var httpClient = new HttpClient();

            try
            {

                #region Serializer Setting for JSON
                //var _serializerSettings = new JsonSerializerSettings
                //{
                //    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                //    NullValueHandling = NullValueHandling.Ignore
                //};

                JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };

                #endregion

                HttpResponseMessage response = await httpClient.GetAsync(url);
                await HandleResponse(response);
                string serialized = await response.Content.ReadAsStringAsync();
                return await DeserializeObject(serialized, _serializerSettings); //await Task.Run(() => JsonConvert.DeserializeObject<ServiceOutput>(serialized, _serializerSettings));
                

            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public async Task<ServiceOutput> PostAsync(string url, object obj)
        {
            var httpClient = new HttpClient();

            try
            {

                #region Serializer Setting for JSON
                //var _serializerSettings = new JsonSerializerSettings
                //{
                //    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                //    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                //    NullValueHandling = NullValueHandling.Ignore
                //};

                JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };

                #endregion

                var serialized = await Task.Run(() => JsonConvert.SerializeObject(obj, _serializerSettings)); //await JsonConvert.SerializeObjectAsync(jsonString);
                HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(serialized, Encoding.UTF8, "application/json"));
                await HandleResponse(response);
                string responseData = await response.Content.ReadAsStringAsync();
                return await DeserializeObject(responseData, _serializerSettings);   //Task.Run(() => JsonConvert.DeserializeObject<ServiceOutput>(responseData, _serializerSettings));
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //throw new Exception(content);
                    //Debug.WriteLine()
                }

                //throw new HttpRequestException(content);
            }
        }

        private async Task<ServiceOutput> DeserializeObject(string content , JsonSerializerSettings jsonSerializerSettings)
        {
            try
            {
                return JsonConvert.DeserializeObject<ServiceOutput>(content, jsonSerializerSettings);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
