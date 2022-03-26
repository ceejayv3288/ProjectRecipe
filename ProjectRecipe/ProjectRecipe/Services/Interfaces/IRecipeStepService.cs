using ProjectRecipe.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services.Interfaces
{
    public interface IRecipeStepService
    {
        Task<RecipeStepModel> GetRecipeStep(int recipeId);

        Task<HttpResponseMessage> CreateRecipeStep(RecipeStepModel recipeStep);

        Task<List<RecipeStepModel>> GetRecipeStepsByRecipeId(int recipeId);

        Task<HttpResponseMessage> UpdateRecipeStep(RecipeStepModel recipeStep);

        Task<HttpResponseMessage> DeleteRecipeStep(int recipeStepId);
    }
}
