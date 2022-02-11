using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models.Service.Request
{
    public class AuthenticationRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
