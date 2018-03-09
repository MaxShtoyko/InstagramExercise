using InstagramEx.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InstagramEx
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public LoginPage()
        {
            InitializeComponent();
            UserServices.CreadteDb();
        }

        async void LogIn_Clicked(object o, EventArgs e)
        {
            if(UserServices.InDatabase(login.Text) && !String.IsNullOrWhiteSpace(login.Text) && !String.IsNullOrWhiteSpace(password.Text))
            {
                if(UserServices.getUser(login.Text).Password==password.Text)
                {
                    UserServices.currentUser = UserServices.getUser(login.Text);
                    UserServices.currentUser.FromFacebook = false;
                    await Navigation.PushModalAsync(new MainTabbedPage());
                }
                else
                {
                    await DisplayAlert("Error", "Please,check the password!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please,check the login!", "OK");
            }
        }

        async void Registration_Clicked(object o, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegistrationPage());
        }

        async void Facebook_Tapped(object o, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainTabbedPage());
        }

        void Twitter_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Twitter", "This option isn't yet available", "Ok");
        }

        void Google_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Google", "This option isn't yet available", "Ok");
        }

        void Vine_Tapped(object sender, EventArgs e)
        {
            DisplayAlert("Vine", "This option isn't yet available", "Ok");
        }
    }
}