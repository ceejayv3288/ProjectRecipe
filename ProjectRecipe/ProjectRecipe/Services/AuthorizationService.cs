using Newtonsoft.Json;
using ProjectRecipe.Constants;
using ProjectRecipe.Models.Service.Request;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient client = new HttpClient();

        public AuthorizationService()
        {

        }

        public async Task<HttpResponseMessage> RegisterUser(RegistrationRequest registrationRequest)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(registrationRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{ApiConfigurations.RecipeApiUrl}users/register", content);
                if (response.IsSuccessStatusCode)
                {

                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
