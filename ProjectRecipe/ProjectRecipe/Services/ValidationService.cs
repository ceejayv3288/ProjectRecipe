using ProjectRecipe.Constants;
using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRecipe.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateCreateRecipe(RecipeCreateUpdateModel recipeToCreate)
        {
            if (recipeToCreate == null)
                return false;
            else if (recipeToCreate.durationInMin <= 0 ||
                     recipeToCreate.image == null ||
                     string.IsNullOrWhiteSpace(recipeToCreate.description) ||
                     string.IsNullOrWhiteSpace(recipeToCreate.name) ||
                     recipeToCreate.courseType == (int)CourseTypeEnum.None)
                return false;
            return true;
        }

        public bool ValidateCreateRecipeIngredient(List<RecipeIngredientModel> recipeIngredientList)
        {
            if (!recipeIngredientList.Any())
                return false;

            foreach (var ingredient in recipeIngredientList)
            {
                if (string.IsNullOrWhiteSpace(ingredient.description) || string.IsNullOrWhiteSpace(ingredient.quantity))
                    return false;
            }
            return true;
        }

        public bool ValidateCreateRecipeStep(List<RecipeStepModel> recipeStepList)
        {
            if (!recipeStepList.Any())
                return false;

            foreach (var step in recipeStepList)
            {
                if (string.IsNullOrWhiteSpace(step.description))
                    return false;
            }
            return true;
        }

        public string ValidateRegistration(RegistrationFieldModel registrationFieldModel)
        {
            if (registrationFieldModel == null)
                return "Fill up all the required fields.";
            else if (registrationFieldModel.profilePicture == null ||
                     string.IsNullOrWhiteSpace(registrationFieldModel.firstName) ||
                     string.IsNullOrWhiteSpace(registrationFieldModel.lastName) ||
                     string.IsNullOrWhiteSpace(registrationFieldModel.userName) ||
                     string.IsNullOrWhiteSpace(registrationFieldModel.email) ||
                     string.IsNullOrWhiteSpace(registrationFieldModel.password) ||
                     string.IsNullOrWhiteSpace(registrationFieldModel.confirmPassword) ||
                     registrationFieldModel.profilePicture == null ||
                     registrationFieldModel.profilePictureByte == null)
                return "Fill up all the required fields.";
            else if (registrationFieldModel.password != registrationFieldModel.confirmPassword)
                return "Passwords are not the same.";
            return null;
        }
    }
}
