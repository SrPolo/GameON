using GameON.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameON.Common.Models
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PicturePath { get; set; }

        public LoginType LoginType { get; set; }

        public UserType UserType { get; set; }

        public VideoGameResponse VideoGame { get; set; }

        public ICollection<GameListResponse> GameList { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
            ? "https://gameonweb.azurewebsites.net/images/noimage.png"
            : LoginType == LoginType.GameON ? $"https://gameonweb.azurewebsites.net{PicturePath}" : PicturePath;
    }
}
