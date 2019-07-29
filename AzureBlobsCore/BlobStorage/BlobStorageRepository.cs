using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureBlobs.Models;
using AzureBlobsCore.Settings;

using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace AzureBlobsCore.BlobStorage
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly string _connectionString;

        public BlobStorageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BlobItem UploadCollection(string collection, string blobName)
        {
            var cloudBlobContainer = GetContainer();

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
            cloudBlockBlob.Properties.ContentType = "application/json";
            cloudBlockBlob.UploadText(collection);

            return new BlobItem()
            {
                BlobId = cloudBlockBlob.Name,
                BlobUri = cloudBlockBlob.Uri
            };
        }

        public async Task<BlobItem> UploadCollectionAsync(string collection, string blobName)
        {
            var cloudBlobContainer = await GetContainerAsync();

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
            cloudBlockBlob.Properties.ContentType = "application/json";
            await cloudBlockBlob.UploadTextAsync(collection);

            return new BlobItem()
            {
                BlobId = cloudBlockBlob.Name,
                BlobUri = cloudBlockBlob.Uri
            };
        }

        public async Task<bool> CheckIfBlobExistsAsync(string blobName)
        {
            var cloudBlobContainer = await GetContainerAsync();
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);

            return await cloudBlockBlob.ExistsAsync();
        }

        public IEnumerable<BlobItem> GetBlobs(string prefix = null)
        {
            var cloudBlockBlobs = new List<CloudBlockBlob>();
            var cloudBlobContainer = GetContainer();

            BlobContinuationToken token = null;
            do
            {
                var blobResultSegment = cloudBlobContainer.ListBlobsSegmented(prefix, token);
                token = blobResultSegment.ContinuationToken;
                cloudBlockBlobs.AddRange(blobResultSegment.Results.OfType<CloudBlockBlob>());
            }
            while (token != null);

            return cloudBlockBlobs.Select(b => new BlobItem() { BlobId = b.Name, BlobUri = b.Uri }).ToList();
        }

        public string DownloadBlob(string blobName)
        {
            var cloudBlobContainer = GetContainer();
            BlobContinuationToken token = null;

            var blobResultSegment = cloudBlobContainer.ListBlobsSegmented(blobName, token);
            var blockBlobs = new List<CloudBlockBlob>();
            blockBlobs.AddRange(blobResultSegment.Results.OfType<CloudBlockBlob>());

            if (blockBlobs.Any())
            {
                return blockBlobs.ElementAt(0).DownloadText();
            }

            return string.Empty;
        }

        private async Task<CloudBlobContainer> GetContainerAsync()
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(AppSettings.BlobContainerName);
            await cloudBlobContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

            return cloudBlobContainer;
        }

        private CloudBlobContainer GetContainer()
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(_connectionString);

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(AppSettings.BlobContainerName);
            cloudBlobContainer.CreateIfNotExists(BlobContainerPublicAccessType.Blob, null, null);

            return cloudBlobContainer;
        }
    }
}
