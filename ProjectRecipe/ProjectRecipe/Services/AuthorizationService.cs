using Newtonsoft.Json;
using ProjectRecipe.Constants;
using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

        public async Task<Tuple<ResponseModel, ErrorMessageModel>> RegisterUserAsync(RegistrationRequest registrationRequest)
        {
            ResponseModel result = new ResponseModel();
            ErrorMessageModel error = null;
            var content = new StringContent(JsonConvert.SerializeObject(registrationRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}users/register", content);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ResponseModel>(responseString);
            }
            else
            {
                string responseString = await response.Content.ReadAsStringAsync();
                error = JsonConvert.DeserializeObject<ErrorMessageModel>(responseString);
            }

            return new Tuple<ResponseModel, ErrorMessageModel>(result, error);
        }

        public async Task<Tuple<UserModel, ErrorMessageModel>> LoginUserAsync(AuthenticationRequest authenticationRequest)
        {
            try
            {
                UserModel result = new UserModel();
                ErrorMessageModel error = null;
                var jsonSerialize = JsonConvert.SerializeObject(authenticationRequest);
                var content = new StringContent(jsonSerialize, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}users/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<UserModel>(responseString);
                }
                else
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    error = JsonConvert.DeserializeObject<ErrorMessageModel>(responseString);
                }

                return new Tuple<UserModel, ErrorMessageModel>(result, error);
            }
            catch (Exception ex)
            {
                return new Tuple<UserModel, ErrorMessageModel>(null, new ErrorMessageModel { message = "Service Unavailable.", statusCode = (int)HttpStatusCode.NotFound });
            }
        }
    }
}
