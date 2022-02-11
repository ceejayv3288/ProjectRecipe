using ProjectRecipe.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services.Interfaces
{
    public interface IErrorService
    {
        Task<ErrorMessageModel> ParseError(HttpResponseMessage errorResponse);
    }
}
