using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AzureBlobs.Models
{
    [DataContract]
    public class TrackContainer
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public Point LeftCornerPosition { get; set; }

        [DataMember]
        public IEnumerable<Track> Tracks { get; set; }
    }
}
