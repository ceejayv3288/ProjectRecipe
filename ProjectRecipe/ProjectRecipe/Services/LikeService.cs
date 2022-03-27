using Newtonsoft.Json;
using ProjectRecipe.Constants;
using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectRecipe.Services
{
    public class LikeService : ILikeService
    {
        private readonly HttpClient client = new HttpClient();

        public LikeService()
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

        public async Task<LikeModel> GetLikesByRecipeAndUserId(int recipeId, string userId)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                LikeModel like = new LikeModel();
                HttpResponseMessage response = await client.GetAsync($"{Configurations.RecipeApiUrl}likes/{recipeId}/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    var deserializeLike = JsonConvert.DeserializeObject<LikeModel>(responseString);
                    if (deserializeLike != null)
                    {
                        like = new LikeModel
                        {
                            Id = deserializeLike.Id,
                            UserId = deserializeLike.UserId,
                            RecipeId = deserializeLike.RecipeId,
                            IsLiked = deserializeLike.IsLiked,
                            DateCreated = deserializeLike.DateCreated,
                            DateUpdated = DateTime.Now
                        };
                    }
                }
                else
                {
                    like = new LikeModel
                    {
                        UserId = userId,
                        RecipeId = recipeId,
                        IsLiked = false,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(like), Encoding.UTF8, "application/json");
                    HttpResponseMessage likeResponse = await client.PostAsync($"{Configurations.RecipeApiUrl}likes", content);
                    if (likeResponse.IsSuccessStatusCode)
                    {
                        string likeResponseString = await likeResponse.Content.ReadAsStringAsync();
                        var deserializeCreatedLike = JsonConvert.DeserializeObject<LikeModel>(likeResponseString);

                        like = new LikeModel
                        {
                            Id = deserializeCreatedLike.Id,
                            UserId = deserializeCreatedLike.UserId,
                            RecipeId = deserializeCreatedLike.RecipeId,
                            IsLiked = deserializeCreatedLike.IsLiked,
                            DateCreated = deserializeCreatedLike.DateCreated,
                            DateUpdated = deserializeCreatedLike.DateUpdated
                        };
                    }
                }
                return like;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LikeModel> LikeUnlikeRecipe(int recipeId, string userId)
        {
            try
            {
                if (client.DefaultRequestHeaders.Contains("Authorization"))
                {
                    client.DefaultRequestHeaders.Remove("Authorization");
                }
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.ClientToken);

                LikeModel like = new LikeModel();
                HttpResponseMessage response = await client.GetAsync($"{Configurations.RecipeApiUrl}likes/{recipeId}/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    var deserializeLike = JsonConvert.DeserializeObject<LikeModel>(responseString);
                    if (deserializeLike != null)
                    {
                        like = new LikeModel
                        {
                            Id = deserializeLike.Id,
                            UserId = deserializeLike.UserId,
                            RecipeId = deserializeLike.RecipeId,
                            IsLiked = !deserializeLike.IsLiked,
                            DateCreated = deserializeLike.DateCreated,
                            DateUpdated = DateTime.Now
                        };
                    }

                    var content = new StringContent(JsonConvert.SerializeObject(like), Encoding.UTF8, "application/json");
                    HttpResponseMessage updateLikeResponse = await client.PutAsync($"{Configurations.RecipeApiUrl}likes/{like.Id}", content);
                    if (updateLikeResponse.IsSuccessStatusCode)
                    {
                        string updateLikeResponseString = await updateLikeResponse.Content.ReadAsStringAsync();
                        var deserializeUpdatedLike = JsonConvert.DeserializeObject<LikeModel>(updateLikeResponseString);

                        like = new LikeModel
                        {
                            Id = deserializeUpdatedLike.Id,
                            UserId = deserializeUpdatedLike.UserId,
                            RecipeId = deserializeUpdatedLike.RecipeId,
                            IsLiked = deserializeUpdatedLike.IsLiked,
                            DateCreated = deserializeUpdatedLike.DateCreated,
                            DateUpdated = deserializeUpdatedLike.DateUpdated
                        };
                    }
                }
                else
                {
                    like = new LikeModel
                    {
                        UserId = userId,
                        RecipeId = recipeId,
                        IsLiked = true,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(like), Encoding.UTF8, "application/json");
                    HttpResponseMessage likeResponse = await client.PostAsync($"{Configurations.RecipeApiUrl}likes", content);
                    if (likeResponse.IsSuccessStatusCode)
                    {
                        string likeResponseString = await likeResponse.Content.ReadAsStringAsync();
                        var deserializeCreatedLike = JsonConvert.DeserializeObject<LikeModel>(likeResponseString);

                        like = new LikeModel
                        {
                            Id = deserializeCreatedLike.Id,
                            UserId = deserializeCreatedLike.UserId,
                            RecipeId = deserializeCreatedLike.RecipeId,
                            IsLiked = deserializeCreatedLike.IsLiked,
                            DateCreated = deserializeCreatedLike.DateCreated,
                            DateUpdated = deserializeCreatedLike.DateUpdated
                        };
                    }
                }
                return like;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
