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

        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
            ? "https://gameonweb.azurewebsites.net/images/noimage.png"
            : $"https://gameonweb.azurewebsites.net{PicturePath}";

        public DateTime ReleaseDate { get; set; }

        public ICollection<VGDeveloperResponse> Developers { get; set; }

        public ICollection<VGGenreResponse> Genres { get; set; }

        public ICollection<VGPlatformResponse> Platforms { get; set; }


    }
}
