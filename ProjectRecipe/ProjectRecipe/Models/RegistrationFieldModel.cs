using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.Models
{
    public class RegistrationFieldModel : BaseModel
    {
        private string _firstName;
        public string firstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value; OnPropertyChanged(nameof(value));
            }
        }
        private string _middleName;
        public string middleName
        {
            get { return _middleName; }
            set
            {
                _middleName = value; OnPropertyChanged(nameof(value));
            }
        }
        private string _lastName;
        public string lastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value; OnPropertyChanged(nameof(value));
            }
        }
        private string _userName;
        public string userName
        {
            get { return _userName; }
            set
            {
                _userName = value; OnPropertyChanged(nameof(value));
            }
        }
        private string _email;
        public string email
        {
            get { return _email; }
            set
            {
                _email = value; OnPropertyChanged(nameof(value));
            }
        }
        private string _password;
        public string password
        {
            get { return _password; }
            set
            {
                _password = value; OnPropertyChanged(nameof(value));
            }
        }
        private string _confirmPassword;
        public string confirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value; OnPropertyChanged(nameof(value));
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
    }
}
