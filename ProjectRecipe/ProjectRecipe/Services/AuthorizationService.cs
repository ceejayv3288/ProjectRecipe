using Newtonsoft.Json;
using ProjectRecipe.Constants;
using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectRecipe.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IErrorService errorService;
        private readonly HttpClient client = new HttpClient();

        public AuthorizationService()
        {
            client.BaseAddress = new Uri(ApiConfigurations.RecipeApiUrl);
            errorService = DependencyService.Get<IErrorService>();
        }

        public async Task<ErrorMessageModel> RegisterUser(RegistrationRequest registrationRequest)
        {
            try
            {
                ErrorMessageModel errorMessage = null;
                var content = new StringContent(JsonConvert.SerializeObject(registrationRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}users/register", content);
                if (!response.IsSuccessStatusCode)
                {
                    errorMessage = await errorService.ParseError(response);
                }

                return errorMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Tuple<UserModel, ErrorMessageModel>> LoginUser(AuthenticationRequest authenticationRequest)
        {
            try
            {
                ErrorMessageModel errorMessage = null;
                UserModel user = null;

                var sample = JsonConvert.SerializeObject(authenticationRequest);
                var content = new StringContent(sample, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}users/authenticate", content);

                var sample2 = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            using (var json = new JsonTextReader(reader))
                            {
                                user = App.JsonSerializer.Deserialize<UserModel>(json);
                            }
                        }
                    }
                }
                else
                {
                    errorMessage = await errorService.ParseError(response);
                }

                return new Tuple<UserModel, ErrorMessageModel>(user, errorMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
