using System;
using System.Collections.Generic;

using AzureBlobs.Models;

namespace UnitTests.TestHelpers
{
    public static class GetMock
    {

        public static TrackContainer TrackContainer()
        {
            return new TrackContainer()
            {
                Id = "HS-" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffffff"),
                LeftCornerPosition = new Point() { X = 110, Y = 201, Z = 117 },
                Tracks = new List<Track>()
                {
                    new Track() { Id = "T01" },
                    new Track() { Id = "T04" }
                }
            };
        }
    }
}
