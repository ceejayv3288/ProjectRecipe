using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models.Service.Request
{
    public class AuthenticationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
