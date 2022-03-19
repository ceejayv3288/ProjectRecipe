using ProjectRecipe.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services.Interfaces
{
    public interface IRecipeIngredientService
    {
        Task<RecipeIngredientModel> GetRecipeIngredient(int recipeIngredientId);

        Task<HttpResponseMessage> CreateRecipeIngredient(RecipeIngredientModel recipeIngredient);

        Task<HttpResponseMessage> UpdateRecipeIngredient(RecipeIngredientModel recipeIngredient);

        Task<HttpResponseMessage> DeleteRecipeIngredient(int recipeIngredientId);
    }
}
