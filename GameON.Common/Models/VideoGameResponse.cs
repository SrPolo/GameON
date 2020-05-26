using System;
using System.Collections.Generic;

namespace GameON.Common.Models
{
    public class VideoGameResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Synopsis { get; set; }

        public float Score { get; set; }

        public string PicturePath { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ICollection<VGDeveloperResponse> Developers { get; set; }

        public ICollection<VGGenreResponse> Genres { get; set; }

        public ICollection<VGPlatformResponse> Platforms { get; set; }


    }
}
