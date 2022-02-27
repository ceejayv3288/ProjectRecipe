using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models
{
    public class UserModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public bool emailApproved { get; set; }
        public string role { get; set; }
        public byte[] profilePicture { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateUpdated { get; set; }
        public string token { get; set; }
    }
}
