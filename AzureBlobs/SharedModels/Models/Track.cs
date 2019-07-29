using System.Runtime.Serialization;

namespace AzureBlobs.Models
{
    [DataContract]
    public class Track
    {
        [DataMember]
        public string Id { get; set; }
    }
}
