using Newtonsoft.Json;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services
{
    internal class ErrorService : IErrorService
    {
        public async Task<ErrorMessageModel> ParseError(HttpResponseMessage errorResponse)
        {
            try
            {
                ErrorMessageModel errorMessage = new ErrorMessageModel(); ;

                var result = await errorResponse.Content.ReadAsStringAsync();

                if (errorResponse.StatusCode == HttpStatusCode.Unauthorized)
                {

                }
                else if (errorResponse.StatusCode == HttpStatusCode.InternalServerError)
                {

                }
                else if (errorResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    errorMessage.message = "Server temporarily not available";
                    errorMessage.statusCode = 503;
                }
                else if (errorResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    errorMessage = JsonConvert.DeserializeObject<ErrorMessageModel>(result);
                }
                else
                {
                    errorMessage = JsonConvert.DeserializeObject<ErrorMessageModel>(result);
                }

                return errorMessage;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
