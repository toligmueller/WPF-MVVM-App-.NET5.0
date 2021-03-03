using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace WPF_MVVM_Base.Models
{
    public class Users : ObservableCollection<User>
    {
        public Users() : base() { }

        public Users(ObservableCollection<User> value) : base(value) {}

        public Users(List<User> value) : base(value) { }

        public Users(IEnumerable<User> value) : base(value) {}

        public Users GetActive() => new Users(this.Where(x => x.IsActive));

        public Users Filter(string filter) => new Users(this.Where(x => x.Name.ToLower().Contains(filter.ToLower()) || x.Email.ToLower().Contains(filter.ToLower()) || x.Username.ToLower().Contains(filter.ToLower())));

    }

    public class User : ObservableObject, IDataErrorInfo
    {
        public string Surname
        {
            get => surname;
            set
            {
                if (SetProperty(ref surname, value))
                {
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(IsSet));
                }
            }
        }
        private string surname;

        public string Lastname
        {
            get => lastname;
            set
            {
                if (SetProperty(ref lastname, value))
                {
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(IsSet));
                }
            }
        }
        private string lastname;

        public string Name => string.IsNullOrEmpty(Surname) ? Lastname : Surname + " " + Lastname;

        public string Username
        {
            get => username;
            set
            {
                if (SetProperty(ref username, value))
                {
                    OnPropertyChanged(nameof(IsSet));
                }
            }
        }
        private string username;

        public string Email
        {
            get => email;
            set
            {
                if (SetProperty(ref email, value))
                {
                    OnPropertyChanged(nameof(IsSet));
                }
            }
        }
        private string email;

        public bool IsSet => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Lastname) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email);

        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value);
        }
        private bool isActive = false;

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Surname):
                        if (string.IsNullOrEmpty(Surname))
                        {
                            return Properties.Resources.Error_SurnameRequired;
                        }
                        break;
                    case nameof(Lastname):
                        if (string.IsNullOrEmpty(Lastname))
                        {
                            return Properties.Resources.Error_LastnameRequired;
                        }
                        break;
                    case nameof(Username):
                        if (string.IsNullOrEmpty(Username))
                        {
                            return Properties.Resources.Error_UsernameRequired;
                        }
                        if (!Regex.IsMatch(Username, @"^[a-zA-Z\d\-_]*$"))
                        {
                            return Properties.Resources.Error_UsernameInvalid;
                        }
                        if (Username.Length < 3)
                        {
                            return Properties.Resources.Error_UsernameToShort;
                        }
                        if (Username.Length > 12)
                        {
                            return Properties.Resources.Error_UsernameToLong;
                        }
                        break;
                    case nameof(Email):
                        if (string.IsNullOrEmpty(Email))
                        {
                            return Properties.Resources.Error_EmailRequired;
                        }
                        if (!Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,63})+)$"))
                        {
                            return Properties.Resources.Error_EmailInvalid;
                        }
                        break;
                }
                return string.Empty;
            }
        }
    }
}
