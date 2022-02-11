using ProjectRecipe.Commands;
using ProjectRecipe.Commands.Navigation;
using ProjectRecipe.Converters;
using ProjectRecipe.Models;
using ProjectRecipe.Models.Service.Request;
using ProjectRecipe.Services.Interfaces;
using ProjectRecipe.Views.Popups;
using System;
using System.Collections.Generic;
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

        private string _firstName;
        public string firstName
        {
            get { return _firstName; }
            set
            {
                SetProperty(ref _firstName, value);
            }
        }

        private string _middleName;
        public string middleName
        {
            get { return _middleName; }
            set
            {
                SetProperty(ref _middleName, value);
            }
        }

        private string _lastName;
        public string lastName
        {
            get { return _lastName; }
            set
            {
                SetProperty(ref _lastName, value);
            }
        }

        private string _userName;
        public string userName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value);
            }
        }

        private string _password;
        public string password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        private ImageSource _profilePicture;
        public ImageSource profilePicture
        {
            get { return _profilePicture; }
            set
            {
                SetProperty(ref _profilePicture, value);
            }
        }

        private byte[] _profilePictureByte;
        public byte[] profilePictureByte
        {
            get { return _profilePictureByte; }
            set
            {
                SetProperty(ref _profilePictureByte, value);
            }
        }

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
                profilePicture = ImageSource.FromFile(result.FullPath);

                Image imageToConvert = new Image
                {
                    Source = ImageSource.FromStream(() => stream)
                };
                profilePictureByte = (byte[])imageToByteArrayConverter.Convert(imageToConvert.Source, null, null, null);
            }
        }

        public async void ExecuteRegisterCommand()
        {
            RegistrationRequest registrationRequest = new RegistrationRequest
            {
               firstName = this.firstName?.Trim(),
               middleName = this.middleName?.Trim(),
               lastName = this.lastName?.Trim(),
               username = this.userName?.Trim(),
               password = this.password?.Trim(),
               profilePicture = this.profilePictureByte,
               dateCreated = DateTime.Now
            };

            bool isValid = validationService.ValidateRegistration(registrationRequest);
            if (!isValid)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Fill up all the required fields.", "Ok");
                return;
            }
                
            App.Current.MainPage.Navigation.ShowPopup(LoadingPopup);
            var result = await authorizationService.RegisterUser(registrationRequest);
            if (result == null)
            {
                LoadingPopup.Dismiss(null);
                await Application.Current.MainPage.DisplayAlert("Alert", "Successfully Registered.", "Ok");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                LoadingPopup.Dismiss(null);
                await Application.Current.MainPage.DisplayAlert("Alert", result.message, "Ok");
            }
        }
    }
}
