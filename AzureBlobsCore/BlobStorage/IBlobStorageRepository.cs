using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AzureBlobs.Models;

namespace AzureBlobsCore.BlobStorage
{
    public interface IBlobStorageRepository
    {
        Task<BlobItem> UploadCollectionAsync(string collection, string blobName);

        BlobItem UploadCollection(string collection, string blobName);
        Task<bool> CheckIfBlobExistsAsync(string blobName);

        IEnumerable<BlobItem> GetBlobs(string prefix = null);

        string DownloadBlob(string blobName);
    }
}
