using Azure.Storage.Files.Shares;
using Azure;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POE_CLDV_6221_ST10224391.Services
{
    public class AzureFileStorageService
    {
        private readonly ShareClient _shareClient;

        public AzureFileStorageService(string storageConnectionString, string shareName)
        {
            _shareClient = new ShareClient(storageConnectionString, shareName);
            _shareClient.CreateIfNotExists();
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            var directoryClient = _shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await fileClient.CreateAsync(stream.Length);
                await fileClient.UploadRangeAsync(
                    new HttpRange(0, stream.Length),
                    stream);
            }
        }

        public async Task<List<string>> ListFilesAsync()
        {
            var directoryClient = _shareClient.GetRootDirectoryClient();
            var files = new List<string>();

            await foreach (var item in directoryClient.GetFilesAndDirectoriesAsync())
            {
                files.Add(item.Name);
            }

            return files;
        }
    }
}
