using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using TestFileUpload.Models;

namespace TestFileUpload.Services
{
    public class AzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobStorageService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration.GetSection("Blob:connectionString").Value);
            _containerName = configuration.GetSection("Blob:containerName").Value!;
        }

        public async Task UploadFileAsync(UsersFile usersFile)
        {
            var blobClient = _blobServiceClient.GetBlobContainerClient(_containerName).GetBlobClient(usersFile.FilePath);

            var metadata = new Dictionary<string, string>
            {
                { "email", usersFile.Email!.Trim() }
            };

            using (var fileStream = usersFile.FileObject.OpenReadStream())
            {

                var options = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = usersFile.FileObject.ContentType
                    }
                };
                options.Metadata = metadata;
                await blobClient.UploadAsync(fileStream, options);
            }
        }
    }
}