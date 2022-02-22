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
        #if DEBUG
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    client = new HttpClient(App.HttpClientHandler);
                    break;
            }
        #endif
            client.BaseAddress = new Uri(Configurations.RecipeApiUrl);
            errorService = DependencyService.Get<IErrorService>();
        }

        public async Task<ResponseModel> RegisterUserAsync(RegistrationRequest registrationRequest)
        {
            ResponseModel result = new ResponseModel();
            var content = new StringContent(JsonConvert.SerializeObject(registrationRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}users/register", content);

            if (response != null)
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        using (var json = new JsonTextReader(reader))
                        {
                            result = App.JsonSerializer.Deserialize<ResponseModel>(json);
                        }
                    }
                }
            }
            
            return result;
        }

        public async Task<UserModel> LoginUserAsync(AuthenticationRequest authenticationRequest)
        {
            try
            {
                UserModel user = new UserModel();
                var jsonSerialize = JsonConvert.SerializeObject(authenticationRequest);
                var content = new StringContent(jsonSerialize, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}users/login", content);

                if (response != null)
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
                return user;
            }
            catch (Exception ex)
            {
                return new UserModel { response = new ResponseModel { isSuccess = false, message = "Service Unavailable." } };
            }
        }
    }
}
