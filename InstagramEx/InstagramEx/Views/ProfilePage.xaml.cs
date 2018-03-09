using InstagramEx.Services;
using InstagramEx.ViewModels;
using System;
using Xamarin.Forms;

namespace InstagramEx
{
    public partial class ProfilePage : ContentPage
    {

        /// <summary>
        /// Make sure to get a new ClientId from:
        /// https://developers.facebook.com/apps/
        /// </summary>
        private string ClientId = "592106287822987";

        public ProfilePage()
        {
            InitializeComponent();

            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id=" 
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {   
            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;

                 await vm.SetFacebookUserProfileAsync(accessToken);

                Content = MainStackLayout;
                user.BindingContext = UserServices.currentUser;
            }
            
        }

        private async void Gender_Completed(object sender, EventArgs e)
        {
            await UserServices.connection.UpdateAsync(UserServices.currentUser);
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }
}
