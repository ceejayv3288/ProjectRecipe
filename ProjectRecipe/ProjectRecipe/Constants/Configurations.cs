using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Constants
{
    public class Configurations
    {
        public const string RecipeApiUrl = "https://recipeapidev.azurewebsites.net/api/v1/";
        public const string ClientTokenKey = "client_token";
        public const int ClientTokenLifetimeByDays = 30;
    }
}
