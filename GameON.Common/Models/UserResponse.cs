using GameON.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameON.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }

        

        public string Email { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public UserType UserType { get; set; }

    }
}
