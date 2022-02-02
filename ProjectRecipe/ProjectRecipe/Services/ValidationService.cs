using ProjectRecipe.Models;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateCreateRecipe(RecipeCreateModel recipeToCreate)
        {
            if (recipeToCreate == null)
                return false;
            else if (recipeToCreate.DurationInMin <= 0 ||
                     recipeToCreate.Image == null ||
                     String.IsNullOrWhiteSpace(recipeToCreate.Description) ||
                     String.IsNullOrWhiteSpace(recipeToCreate.Name))
                return false;
            return true;
        }
    }
}
