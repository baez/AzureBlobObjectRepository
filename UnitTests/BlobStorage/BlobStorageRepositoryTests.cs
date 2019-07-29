using System;
using System.Linq;
using System.Threading.Tasks;

using AzureBlobs.Models;

using AzureBlobsCore.BlobStorage;
using AzureBlobsCore.Settings;
using AzureBlobsCore.Shared;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using UnitTests.TestHelpers;

namespace AzureBlobs.BlobStorage.Tests
{
    /// <summary>
    /// This class includes integration tests for the blob storage repository.
    /// </summary>
    [TestClass(), TestCategory("Integration")]
    public class BlobStorageRepositoryTests
    {
        // replace with AzureDevConnectionString for production integration tests
        private static readonly string _blobConnectionString = AppSettings.LocalDevConnectionString;

        [TestMethod()]
        public void UploadCollection_WhenCollectionValidUploadToAzure_ShouldUpload()
        {
            // --- Arrange
            TrackContainer trackContainer = GetMock.TrackContainer();
            var resultJson = Serializer.WriteFromObject(typeof(TrackContainer), trackContainer);

            var sut = new BlobStorageRepository(_blobConnectionString);

            // --- Act
            var blobName = trackContainer.Id;
            var blobItem = sut.UploadCollection(resultJson, blobName);

            // --- Assert
            Assert.IsNotNull(blobItem);
            Assert.AreEqual(blobName, blobItem.BlobId);
            Assert.IsTrue(blobItem.BlobUri.AbsoluteUri.LastIndexOf(blobName) > 0);
        }

        [TestMethod()]
        public async Task UploadCollectionAsync_WhenCollectionValidUploadToAzure_ShouldUpload()
        {
            // --- Arrange
            TrackContainer trackContainer = GetMock.TrackContainer();
            var resultJson = Serializer.WriteFromObject(typeof(TrackContainer), trackContainer);

            var sut = new BlobStorageRepository(_blobConnectionString);

            // --- Act
            var blobName = trackContainer.Id;
            var blobItem = await sut.UploadCollectionAsync(resultJson, blobName);

            // --- Assert
            Assert.IsNotNull(blobItem);
            Assert.AreEqual(blobName, blobItem.BlobId);
            Assert.IsTrue(blobItem.BlobUri.AbsoluteUri.LastIndexOf(blobName) > 0);
        }

        [TestMethod(), ExpectedException(typeof(ArgumentException))]
        public async Task UploadCollectionAsync_WhenBlobNameIsEmpty_ShouldThrow()
        {
            // --- Arrange
            TrackContainer trackContainer = GetMock.TrackContainer();
            var resultJson = Serializer.WriteFromObject(typeof(TrackContainer), trackContainer);

            var sut = new BlobStorageRepository(_blobConnectionString);

            // --- Act
            string blobName = "";
            await sut.UploadCollectionAsync(resultJson, blobName);
        }

        [TestMethod()]
        public void GetBlobs_WhenAzureBlobStorageHasItems_ShouldReturnItems()
        {
            var connStr = AppSettings.AzureDevConnectionString;
            var sut = new BlobStorageRepository(_blobConnectionString);

            // --- Act
            var blobItems = sut.GetBlobs();

            // --- Assert
            Assert.IsNotNull(blobItems);
            Assert.IsTrue(blobItems.ToArray().Length > 0);
        }

        [TestMethod()]
        public void GetBlobs_WhenBlobStorageHasItemsAndPrefixIsFound_ShouldReturnItems()
        {
            var sut = new BlobStorageRepository(_blobConnectionString);
            TrackContainer trackContainer = GetMock.TrackContainer();
            var resultJson = Serializer.WriteFromObject(typeof(TrackContainer), trackContainer);
            var blobItem = sut.UploadCollection(resultJson, trackContainer.Id);

            // --- Act
            var blobItems = sut.GetBlobs(blobItem.BlobId);

            // --- Assert
            Assert.IsNotNull(blobItems);
            Assert.IsTrue(blobItems.ToArray().Length > 0);
        }

        [TestMethod()]
        public void GetBlobsAsync_WhenBlobStorageHasItemsAndPrefixIsNotFound_ShouldNotReturnAnyItems()
        {
            var sut = new BlobStorageRepository(_blobConnectionString);

            // --- Act
            var resultBlockBlob = sut.GetBlobs("NonExistingPrefix");

            // --- Assert
            Assert.IsNotNull(resultBlockBlob);
            Assert.IsTrue(resultBlockBlob.ToArray().Length == 0);
        }

        [TestMethod()]
        public void DownloadBlob_WhenValidCollectionUploadedAzure_ShouldDownloadToCollectionObject()
        {
            // --- Arrange
            TrackContainer trackContainer = GetMock.TrackContainer();
            var containerJson = Serializer.WriteFromObject(typeof(TrackContainer), trackContainer);

            var sut = new BlobStorageRepository(_blobConnectionString);

            // --- Act
            var blobName = trackContainer.Id;
            sut.UploadCollection(containerJson, blobName);
            var objectString = sut.DownloadBlob(blobName);
            var container = Serializer.ReadToTrackObject<TrackContainer>(objectString);

            // --- Assert
            Assert.IsNotNull(container);
        }

        [TestMethod()]
        public void DownloadBlob_WhenBlobnameDoesNotExist_ShouldReturnEmptyString()
        {
            // --- Arrange
            var sut = new BlobStorageRepository(_blobConnectionString);

            // --- Act
            var blobName = "non existing blob";
            var objectString = sut.DownloadBlob(blobName);

            // --- Assert
            Assert.IsTrue(objectString == string.Empty);
        }

    }
}