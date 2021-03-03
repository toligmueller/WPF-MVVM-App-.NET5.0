using Microsoft.Toolkit.Mvvm.ComponentModel;
using WPF_MVVM_Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input.Wpf;
using System.Diagnostics;
using System.Windows;

namespace WPF_MVVM_Base.ViewModels
{
    public class UserListPageViewModel : ObservableObject
    {
        public UserListPageViewModel()
        {
            if (Application.Current.MainWindow == null)
            {
                _baseUserList.Add(new User() { Surname = "Max", Lastname = "Mustermann", Username = "SuperMaxe69", Email = "M.Mustermann@ColdMail.com", IsActive = true });
                _baseUserList.Add(new User() { Surname = "Petra", Lastname = "Mustermann", Username = "Gürlie", Email = "P.Mustermann@ColdMail.com", IsActive = true });
            }
            else
            {
                _baseUserList.Add(new User() { Surname = "Max", Lastname = "Mustermann", Username = "SuperMaxe69", Email = "M.Mustermann@ColdMail.com", IsActive = true });
                _baseUserList.Add(new User() { Surname = "Petra", Lastname = "Mustermann", Username = "Gürlie", Email = "P.Mustermann@ColdMail.com", IsActive = true });
                _baseUserList.Add(new User() { Surname = "Peter", Lastname = "Griffin", Username = "DaMan", Email = "BirdIsThe@Word.com", IsActive = false });
                _baseUserList.Add(new User() { Surname = "Max", Lastname = "Mustermann", Username = "SuperMaxe69", Email = "M.Mustermann@ColdMail.com", IsActive = true });
                _baseUserList.Add(new User() { Surname = "Max", Username = "SuperMaxe71", IsActive = true });
            }
            UserList = _baseUserList.GetActive();
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                {
                    // Raise Update for all Commands
                    OnPropertyChanged(nameof(ReloadUsersCmd));
                }
            }
        }
        private bool _isBusy;

        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }
        private string _error;

        private Users _baseUserList = new Users();

        public Users UserList { get; set; }

        public RelayCommand ReloadUsersCmd => new(
         () =>
         {
             IsBusy = true;
             try
             {
                 UserList = UserList.GetActive();
                 OnPropertyChanged(nameof(UserList));
             }
             catch (Exception ex)
             {
                 Debug.WriteLine(ex.Message);
             }
             finally
             {
                 IsBusy = false;
             }
         },
         () => !IsBusy
     );
    }
}
