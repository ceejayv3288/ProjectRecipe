using ProjectRecipe.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<List<RecipeModel>> GetPopularRecipes();
        Task<List<RecipeModel>> GetAllRecipes();
        Task<RecipeModel> GetRecipe(int recipeId);
        Task<HttpResponseMessage> CreateRecipe(RecipeModel recipe);
        Task<HttpResponseMessage> UpdateRecipe(RecipeModel recipe);
        Task<HttpResponseMessage> DeleteRecipe(int recipeId);
    }
}
