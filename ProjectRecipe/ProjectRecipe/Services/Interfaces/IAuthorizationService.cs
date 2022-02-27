using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<Tuple<ResponseModel, ErrorMessageModel>> RegisterUserAsync(RegistrationRequest registrationRequest);
        Task<Tuple<UserModel, ErrorMessageModel>> LoginUserAsync(AuthenticationRequest authenticationRequest);
    }
}
