using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using InstagramEx.Models;
using InstagramEx.Services;
using SQLite;
using Xamarin.Forms;

namespace InstagramEx
{
    [Table("Users")]
    public class User:INotifyPropertyChanged
    {        
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if(name!=value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string login;
        [PrimaryKey]
        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                if (gender != value)
                {
                    gender = value;
                    OnPropertyChanged(nameof(Gender));
                }
            }
        }

        public string Password { get; set; }
        public bool FromFacebook = true;

        [Ignore]
        public ObservableCollection<Photo> Images { get; set; }

        public void OnPropertyChanged(string propertyName="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetUser(FacebookProfile profile)
        {
            UserServices.currentUser.Name = profile.Name;
            UserServices.currentUser.Login = profile.Id;
            UserServices.currentUser.Gender = profile.Gender;
        }
    }

}
