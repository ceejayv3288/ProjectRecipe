using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Constants
{
    public class Configurations
    {
        public const string RecipeApiUrl = "https://192.168.0.33:45455/api/v1/";
        public const string ClientTokenKey = "client_token";
        public const string UserIdKey = "user_id";
        public const int ClientTokenLifetimeByDays = 30;
    }
}
