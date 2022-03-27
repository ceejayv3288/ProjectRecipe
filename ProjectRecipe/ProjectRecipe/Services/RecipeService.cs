using Newtonsoft.Json;
using ProjectRecipe.Constants;
using ProjectRecipe.Converters;
using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
using ProjectRecipe.Models.Service.Response;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectRecipe.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient client = new HttpClient();
        ByteArrayToImageConverter byteArrayToImageConverter;
        ImageToByteArrayConverter imageToByteArrayConverter;

        public RecipeService()
        {
        #if DEBUG
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    client = new HttpClient(App.HttpClientHandler);
                    break;
            }
        #endif
            byteArrayToImageConverter = new ByteArrayToImageConverter();
            imageToByteArrayConverter = new ImageToByteArrayConverter();
        }

        public async Task<List<RecipeModel>> GetPopularRecipes()
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                List<RecipeResponse> recipesResponse = new List<RecipeResponse>();
                List<RecipeModel> recipes = new List<RecipeModel>();
                HttpResponseMessage response = await client.GetAsync($"{Configurations.RecipeApiUrl}recipes/GetPopularRecipes/{App.UserId}");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    recipesResponse = JsonConvert.DeserializeObject<List<RecipeResponse>>(responseString);
                }

                foreach (var recipe in recipesResponse)
                {
                    recipes.Add(new RecipeModel
                    {
                        id = recipe.id,
                        name = recipe.name,
                        description = recipe.description,
                        durationInMin = recipe.durationInMin,
                        commentsCount = recipe.commentsCount,
                        image = byteArrayToImageConverter.Convert(recipe.image, null, null, null) as ImageSource,
                        likesCount = recipe.likesCount,
                        courseType = recipe.courseType,
                        isLiked = recipe.isLiked
                    });
                }
                return recipes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RecipeModel>> GetAllRecipes()
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                List<RecipeResponse> recipesResponse = new List<RecipeResponse>();
                List<RecipeModel> recipes = new List<RecipeModel>();
                HttpResponseMessage response = await client.GetAsync($"{Configurations.RecipeApiUrl}recipes");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    recipesResponse = JsonConvert.DeserializeObject<List<RecipeResponse>>(responseString);
                }

                foreach (var recipe in recipesResponse)
                {
                    recipes.Add(new RecipeModel
                    {
                        id = recipe.id,
                        name = recipe.name,
                        description = recipe.description,
                        durationInMin = recipe.durationInMin,
                        commentsCount = recipe.commentsCount,
                        image = byteArrayToImageConverter.Convert(recipe.image, null, null, null) as ImageSource,
                        likesCount = recipe.likesCount
                    });
                }
                return recipes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RecipeModel> GetRecipe(int recipeId)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                RecipeModel recipe = new RecipeModel();
                HttpResponseMessage response = await client.GetAsync($"{Configurations.RecipeApiUrl}recipes/{recipeId}");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    var recipeResponse = JsonConvert.DeserializeObject<RecipeResponse>(responseString);
                    recipe = new RecipeModel
                    {
                        id = recipeResponse.id,
                        name = recipeResponse.name,
                        description = recipeResponse.description,
                        durationInMin = recipeResponse.durationInMin,
                        commentsCount = recipeResponse.commentsCount,
                        image = byteArrayToImageConverter.Convert(recipeResponse.image, null, null, null) as ImageSource,
                        likesCount = recipeResponse.likesCount
                    };
                }
                return recipe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RecipeModel> CreateRecipe(RecipeCreateUpdateModel recipeCreateUpdate)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                RecipeModel recipe = new RecipeModel();
                var content = new StringContent(JsonConvert.SerializeObject(recipeCreateUpdate), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{Configurations.RecipeApiUrl}recipes", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    var recipeResponse = JsonConvert.DeserializeObject<RecipeResponse>(responseString);
                    recipe = new RecipeModel
                    {
                        id = recipeResponse.id,
                        name = recipeResponse.name,
                        description = recipeResponse.description,
                        durationInMin = recipeResponse.durationInMin,
                        commentsCount = recipeResponse.commentsCount,
                        image = byteArrayToImageConverter.Convert(recipeResponse.image, null, null, null) as ImageSource,
                        likesCount = recipeResponse.likesCount
                    };
                }
                return recipe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> UpdateRecipe(RecipeModel recipe)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                byte[] imageByte = (byte[])imageToByteArrayConverter.Convert(recipe.image, null, null, null);
                RecipeRequest recipeRequest = new RecipeRequest
                {
                    id = recipe.id,
                    name = recipe.name,
                    description = recipe.description,
                    durationInMin = recipe.durationInMin,
                    image = imageByte
                };
                var content = new StringContent(JsonConvert.SerializeObject(recipeRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{Configurations.RecipeApiUrl}recipes/{recipe.id}", content);
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

        public async Task<HttpResponseMessage> DeleteRecipe(int recipeId)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                List<RecipeModel> recipes = new List<RecipeModel>();
                HttpResponseMessage response = await client.DeleteAsync($"{Configurations.RecipeApiUrl}recipes/{recipeId}");
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

        public async Task<List<RecipeModel>> GetRecipesByUser(string userId)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                List<RecipeResponse> recipesResponse = new List<RecipeResponse>();
                List<RecipeModel> recipes = new List<RecipeModel>();
                HttpResponseMessage response = await client.GetAsync($"{Configurations.RecipeApiUrl}recipes/userId/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    recipesResponse = JsonConvert.DeserializeObject<List<RecipeResponse>>(responseString);
                }

                foreach (var recipe in recipesResponse)
                {
                    recipes.Add(new RecipeModel
                    {
                        id = recipe.id,
                        name = recipe.name,
                        description = recipe.description,
                        durationInMin = recipe.durationInMin,
                        commentsCount = recipe.commentsCount,
                        image = byteArrayToImageConverter.Convert(recipe.image, null, null, null) as ImageSource,
                        likesCount = recipe.likesCount
                    });
                }
                return recipes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
