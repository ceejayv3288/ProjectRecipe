using ProjectRecipe.Commands;
using ProjectRecipe.Commands.Navigation;
using ProjectRecipe.Converters;
using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
using ProjectRecipe.Services.Interfaces;
using ProjectRecipe.Views.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectRecipe.ViewModels
{
    public class RegistrationPageViewModel : BaseViewModel
    {
        private readonly IValidationService validationService;
        private readonly IAuthorizationService authorizationService;

        ByteArrayToImageConverter byteArrayToImageConverter;
        ImageToByteArrayConverter imageToByteArrayConverter;

        public OpenMediaPickerCommand OpenMediaPickerCommand { get; set; }
        public RegisterCommand RegisterCommand { get; set; }

        public RegistrationFieldModel registrationFieldModel { get; set; } = new RegistrationFieldModel();

        public RegistrationPageViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
            OpenMediaPickerCommand = new OpenMediaPickerCommand(this);
            PopPageCommand = new PopPageCommand(this);

            validationService = DependencyService.Get<IValidationService>();
            authorizationService = DependencyService.Get<IAuthorizationService>();

            byteArrayToImageConverter = new ByteArrayToImageConverter();
            imageToByteArrayConverter = new ImageToByteArrayConverter();
        }

        public async Task ExecuteOpenMediaPickerCommand()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (result != null)
            {
                var stream = await result?.OpenReadAsync();
                registrationFieldModel.profilePicture = ImageSource.FromFile(result.FullPath);

                Image imageToConvert = new Image
                {
                    Source = ImageSource.FromStream(() => stream)
                };
                registrationFieldModel.profilePictureByte = (byte[])imageToByteArrayConverter.Convert(imageToConvert.Source, null, null, null);
            }
        }

        public async void ExecuteRegisterCommand()
        {
            string errorMessage = validationService.ValidateRegistration(registrationFieldModel);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", errorMessage, "Ok");
                return;
            }

            RegistrationRequest registrationRequest = new RegistrationRequest
            {
                firstName = registrationFieldModel.firstName?.Trim(),
                middleName = registrationFieldModel.middleName?.Trim(),
                lastName = registrationFieldModel.lastName?.Trim(),
                username = registrationFieldModel.userName?.Trim(),
                password = registrationFieldModel.password?.Trim(),
                email = registrationFieldModel.email?.Trim(),
                profilePicture = registrationFieldModel.profilePictureByte,
                dateCreated = DateTime.Now
            };

            App.Current.MainPage.Navigation.ShowPopup(LoadingPopup);
            var result = await authorizationService.RegisterUserAsync(registrationRequest);
            if (result.Item2 == null)
            {
                LoadingPopup.Dismiss(null);
                await Application.Current.MainPage.DisplayAlert("Alert", result.Item1.message, "Ok");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                LoadingPopup.Dismiss(null);
                await Application.Current.MainPage.DisplayAlert("Alert", string.IsNullOrWhiteSpace(result.Item2.message) ? result.Item2.errors.FirstOrDefault() : result.Item2.message, "Ok");
            }
        }
    }
}
