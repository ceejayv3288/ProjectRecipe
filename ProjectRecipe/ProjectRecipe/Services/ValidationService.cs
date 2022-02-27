using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
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
            else if (recipeToCreate.durationInMin <= 0 ||
                     recipeToCreate.image == null ||
                     string.IsNullOrWhiteSpace(recipeToCreate.description) ||
                     string.IsNullOrWhiteSpace(recipeToCreate.name))
                return false;
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
                     registrationFieldModel.profilePicture != null ||
                     registrationFieldModel.profilePictureByte != null)
                return "Fill up all the required fields.";
            else if (registrationFieldModel.password != registrationFieldModel.confirmPassword)
                return "Passwords are not the same.";
            return null;
        }
    }
}
