using Newtonsoft.Json;
using ProjectRecipe.Constants;
using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectRecipe.Services
{
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly HttpClient client = new HttpClient();

        public RecipeIngredientService()
        {
            #if DEBUG
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    client = new HttpClient(App.HttpClientHandler);
                    break;
            }
            #endif
        }

        public async Task<HttpResponseMessage> CreateRecipeIngredient(RecipeIngredientModel recipeIngredient)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                var content = new StringContent(JsonConvert.SerializeObject(recipeIngredient), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{Configurations.RecipeApiUrl}recipeIngredients", content);
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

        public async Task<HttpResponseMessage> DeleteRecipeIngredient(int recipeIngredientId)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                List<RecipeModel> recipes = new List<RecipeModel>();
                HttpResponseMessage response = await client.DeleteAsync($"{Configurations.RecipeApiUrl}recipeIngredients/{recipeIngredientId}");
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

        public async Task<RecipeIngredientModel> GetRecipeIngredient(int recipeIngredientId)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                RecipeIngredientModel recipeIngredient = new RecipeIngredientModel();
                HttpResponseMessage response = await client.GetAsync($"{Configurations.RecipeApiUrl}recipeIngredients/{recipeIngredientId}");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    recipeIngredient = JsonConvert.DeserializeObject<RecipeIngredientModel>(responseString);
                }

                return recipeIngredient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> UpdateRecipeIngredient(RecipeIngredientModel recipeIngredient)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                RecipeIngredientRequest recipeRequest = new RecipeIngredientRequest
                {
                    id = recipeIngredient.id,
                    description = recipeIngredient.description,
                    quantity = recipeIngredient.quantity,
                    dateUpdated = DateTime.Now
                };
                var content = new StringContent(JsonConvert.SerializeObject(recipeRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{Configurations.RecipeApiUrl}recipeIngredients/{recipeIngredient.id}", content);
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
