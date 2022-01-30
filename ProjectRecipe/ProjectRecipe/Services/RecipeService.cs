﻿using Newtonsoft.Json;
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
using Xamarin.Forms;

namespace ProjectRecipe.Services
{
    public class RecipeService : IRecipeService
    {
        readonly HttpClient client = new HttpClient();
        ByteArrayToImageConverter byteArrayToImageConverter;
        ImageToByteArrayConverter imageToByteArrayConverter;

        public RecipeService()
        {
            byteArrayToImageConverter = new ByteArrayToImageConverter();
            imageToByteArrayConverter = new ImageToByteArrayConverter();
        }

        public async Task<List<RecipeModel>> GetPopularRecipes()
        {
            try
            {
                List<RecipeResponse> recipesResponse = new List<RecipeResponse>();
                List<RecipeModel> recipes = new List<RecipeModel>();
                HttpResponseMessage response = await client.GetAsync($"https://recipeapidev.azurewebsites.net/api/recipe");
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            using (var json = new JsonTextReader(reader))
                            {
                                recipesResponse = App.JsonSerializer.Deserialize<List<RecipeResponse>>(json);
                            }
                        }
                    }
                }

                foreach (var recipe in recipesResponse)
                {
                    recipes.Add(new RecipeModel
                    {
                        Id = recipe.Id,
                        Name = recipe.Name,
                        Description = recipe.Description,
                        DurationInMin = recipe.DurationInMin,
                        CommentsCount = recipe.CommentsCount,
                        Image = byteArrayToImageConverter.Convert(recipe.Image, null, null, null) as ImageSource,
                        LikesCount = recipe.LikesCount,
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
                List<RecipeResponse> recipesResponse = new List<RecipeResponse>();
                List<RecipeModel> recipes = new List<RecipeModel>();
                HttpResponseMessage response = await client.GetAsync($"https://recipeapidev.azurewebsites.net/api/recipe");
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            using (var json = new JsonTextReader(reader))
                            {
                                recipesResponse = App.JsonSerializer.Deserialize<List<RecipeResponse>>(json);
                            }
                        }
                    }
                }

                foreach (var recipe in recipesResponse)
                {
                    recipes.Add(new RecipeModel
                    {
                        Id = recipe.Id,
                        Name = recipe.Name,
                        Description = recipe.Description,
                        DurationInMin = recipe.DurationInMin,
                        CommentsCount = recipe.CommentsCount,
                        Image = byteArrayToImageConverter.Convert(recipe.Image, null, null, null) as ImageSource,
                        LikesCount = recipe.LikesCount
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
                RecipeModel recipe = new RecipeModel();
                HttpResponseMessage response = await client.GetAsync($"https://recipeapidev.azurewebsites.net/api/recipe/{recipeId}");
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            using (var json = new JsonTextReader(reader))
                            {
                                recipe = App.JsonSerializer.Deserialize<RecipeModel>(json);
                            }
                        }
                    }
                }

                return recipe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> CreateRecipe(RecipeModel recipe)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(recipe), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"https://recipeapidev.azurewebsites.net/api/recipe", content);
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

        public async Task<HttpResponseMessage> UpdateRecipe(RecipeModel recipe)
        {
            try
            {
                byte[] imageByte = (byte[])imageToByteArrayConverter.Convert(recipe.Image, null, null, null);
                RecipeRequest recipeRequest = new RecipeRequest
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Description = recipe.Description,
                    DurationInMin = recipe.DurationInMin,
                    Image = imageByte
                };
                var content = new StringContent(JsonConvert.SerializeObject(recipeRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"https://recipeapidev.azurewebsites.net/api/recipe/{recipe.Id}", content);
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
                List<RecipeModel> recipes = new List<RecipeModel>();
                HttpResponseMessage response = await client.DeleteAsync($"https://recipeapidev.azurewebsites.net/api/recipe/{recipeId}");
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
