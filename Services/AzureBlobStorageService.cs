using Azure.Storage.Blobs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace POE_CLDV_6221_ST10224391.Services
{
    public class AzureBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public AzureBlobStorageService(string storageConnectionString, string containerName)
        {
            var blobServiceClient = new BlobServiceClient(storageConnectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task UploadImageAsync(IFormFile image)
        {
            var blobClient = _containerClient.GetBlobClient(image.FileName);

            using (var stream = image.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }
        }

        public async Task<List<string>> ListImagesAsync()
        {
            var blobs = new List<string>();
            await foreach (var blobItem in _containerClient.GetBlobsAsync())
            {
                blobs.Add(blobItem.Name);
            }
            return blobs;
        }
    }
}
