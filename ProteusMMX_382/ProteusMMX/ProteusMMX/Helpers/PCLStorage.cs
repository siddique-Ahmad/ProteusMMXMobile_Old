using Newtonsoft.Json;
using PCLStorage;
using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Helpers
{
    public static class PCLStorage
    {
        public async static Task<bool> PCLCreateFolder(string folderName)
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            bool IsFolderExit = await PCLHelper.IsFolderExistAsync(folderName, folder);
            if (!IsFolderExit)
            {
                folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
                return true;
            }
            return IsFolderExit;
        }

        public  async static Task<bool> PCLCreateFIle(string filename)
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            bool IsFileExit = await PCLHelper.IsFileExistAsync(filename, folder); ;
            if (!IsFileExit)
            {
                IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                return true;
            }
            return IsFileExit;
        }

        public  static ServiceOutput GetPCLAsync(string serialized)
        {
            try
            {
                #region Serializer Setting for JSON

                JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };

                #endregion


                return  DeserializeObject(serialized, _serializerSettings); //await Task.Run(() => JsonConvert.DeserializeObject<ServiceOutput>(serialized, _serializerSettings));


            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public async  static Task<string> PostPCLAsync(object serialized)
        {
            try
            {
                JsonSerializerSettings _serializerSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                var serializedt = await Task.Run(() => JsonConvert.SerializeObject(serialized, _serializerSettings));

                return serializedt;


            }
            catch (Exception ex)
            {
            }

            return null;
        }

        private  static ServiceOutput DeserializeObject(string content, JsonSerializerSettings jsonSerializerSettings)
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
