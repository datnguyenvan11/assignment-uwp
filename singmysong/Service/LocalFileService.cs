using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using singmysong.Entity;
using Windows.Storage;

namespace singmysong.Service
{
    class LocalFileService : IFileService
    {
        public async Task<bool> SaveMemberCredentialToFile(MemberCredential memberCredential)
        {
            try
            {
                var storageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("SavedFile",
                    CreationCollisionOption.OpenIfExists);
                var storageFile =
                   await storageFolder.CreateFileAsync("token.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(storageFile, JsonConvert.SerializeObject(memberCredential));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<MemberCredential> ReadMemberCredentialFromFile()
        {
            var storageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("SavedFile",
                CreationCollisionOption.OpenIfExists);
            try
            {
                var storageFile =
                    await storageFolder.GetFileAsync("token.txt");
                if (storageFile != null)
                {
                    var jsonContent = await FileIO.ReadTextAsync(storageFile);
                    return JsonConvert.DeserializeObject<MemberCredential>(jsonContent);
                }
            }
            catch (Exception e)
            {
                // ignored
            }
            return null;
        }

       
    }
}
