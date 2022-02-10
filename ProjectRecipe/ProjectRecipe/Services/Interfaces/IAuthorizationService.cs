﻿using ProjectRecipe.Models.Service.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<HttpResponseMessage> RegisterUser(RegistrationRequest registrationRequest);
    }
}
