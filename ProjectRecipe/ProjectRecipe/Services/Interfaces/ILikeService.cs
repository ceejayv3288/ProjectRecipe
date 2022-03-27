using ProjectRecipe.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services.Interfaces
{
    public interface ILikeService
    {
        Task<LikeModel> LikeUnlikeRecipe(int recipeId, string userId);

        Task<LikeModel> GetLikesByRecipeAndUserId(int recipeId, string userId);
    }
}
