using InstagramEx.Models;
using InstagramEx.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstagramEx
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage : ContentPage
	{
		public RegistrationPage ()
		{
			InitializeComponent ();
		}

        private async void OnSignUp(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(name.Text) || String.IsNullOrWhiteSpace(password.Text) || String.IsNullOrWhiteSpace(login.Text))
            {
                await DisplayAlert("Error", "Check the information", "OK");
            }
            else
            {              
                if (!UserServices.InDatabase(login.Text))
                {
                    UserServices.currentUser.Name = name.Text;
                    UserServices.currentUser.Login = login.Text;
                    UserServices.currentUser.Password = password.Text;
                    UserServices.currentUser.FromFacebook = false;
                    UserServices.AddToDatabase(UserServices.currentUser);
                }
                UserServices.currentUser = UserServices.getUser(login.Text);

                await Navigation.PushModalAsync(new MainTabbedPage());
            }
        }
    }
}