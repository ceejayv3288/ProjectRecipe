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
                     String.IsNullOrWhiteSpace(recipeToCreate.description) ||
                     String.IsNullOrWhiteSpace(recipeToCreate.name))
                return false;
            return true;
        }

        public bool ValidateRegistration(RegistrationRequest registrationRequest)
        {
            if (registrationRequest == null)
                return false;
            else if (registrationRequest.profilePicture == null ||
                     String.IsNullOrWhiteSpace(registrationRequest.firstName) ||
                     String.IsNullOrWhiteSpace(registrationRequest.lastName) ||
                     String.IsNullOrWhiteSpace(registrationRequest.username) ||
                     String.IsNullOrWhiteSpace(registrationRequest.password))
                return false;
            return true;
        }
    }
}
