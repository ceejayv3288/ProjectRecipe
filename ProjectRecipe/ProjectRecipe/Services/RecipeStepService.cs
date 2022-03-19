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
    public class RecipeStepService : IRecipeStepService
    {
        private readonly HttpClient client = new HttpClient();

        public RecipeStepService()
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

        public async Task<HttpResponseMessage> CreateRecipeStep(RecipeStepModel recipeStep)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                var content = new StringContent(JsonConvert.SerializeObject(recipeStep), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{Configurations.RecipeApiUrl}recipeSteps", content);
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

        public async Task<HttpResponseMessage> DeleteRecipeStep(int recipeStepId)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                List<RecipeModel> recipes = new List<RecipeModel>();
                HttpResponseMessage response = await client.DeleteAsync($"{Configurations.RecipeApiUrl}recipeSteps/{recipeStepId}");
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

        public async Task<RecipeModel> GetRecipeStep(int recipeStepId)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                RecipeModel recipe = new RecipeModel();
                HttpResponseMessage response = await client.GetAsync($"{Configurations.RecipeApiUrl}recipeSteps/recipeId/{recipeStepId}");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    recipe = JsonConvert.DeserializeObject<RecipeModel>(responseString);
                }

                return recipe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> UpdateRecipeStep(RecipeStepModel recipeStep)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                RecipeStepRequest recipeStepRequest = new RecipeStepRequest
                {
                    id = recipeStep.id,
                    description = recipeStep.description,
                    order = recipeStep.order,
                    dateUpdated = DateTime.Now
                };
                var content = new StringContent(JsonConvert.SerializeObject(recipeStepRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{Configurations.RecipeApiUrl}recipeSteps/{recipeStep.id}", content);
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
