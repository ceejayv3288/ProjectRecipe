using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models.Service.Request
{
    public class RegistrationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
