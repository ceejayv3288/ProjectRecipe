using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services.Interfaces
{
    public interface IValidationService
    {
        bool ValidateCreateRecipe(RecipeCreateUpdateModel recipeToCreateUpdate);
        bool ValidateCreateRecipeStep(List<RecipeStepModel> recipeStepList);
        bool ValidateCreateRecipeIngredient(List<RecipeIngredientModel> recipeIngredientList);
        string ValidateRegistration(RegistrationFieldModel registrationFieldModel);
    }
}
