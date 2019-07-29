namespace AzureBlobsCore.Settings
{
    public static class AppSettings
    {
        public static readonly string LocalDevConnectionString = "UseDevelopmentStorage=true";
        public static readonly string AzureDevConnectionString = "Add your connection string here or use the line above to use Dev Blob Simulator";

        public static readonly string BlobContainerName = "objects";
    }
}
