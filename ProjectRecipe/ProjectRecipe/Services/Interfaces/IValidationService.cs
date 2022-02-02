using ProjectRecipe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services.Interfaces
{
    public interface IValidationService
    {
        bool ValidateCreateRecipe(RecipeCreateModel recipeToCreate);
    }
}
