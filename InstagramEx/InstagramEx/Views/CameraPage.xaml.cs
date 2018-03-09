using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using InstagramEx.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using InstagramEx.Services;

namespace InstagramEx
{
	public partial class CameraPage : ContentPage
	{
		public CameraPage ()
		{
			InitializeComponent ();
		}

        async void TakePhoto_Clicked(object sender, System.EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });
            if (file == null)
                return;
            await DisplayAlert("File Location", file.Path, "OK");

            image.Source = file.Path;

            UserServices.currentUser.Images.Add(new Photo() { Path = file.Path, Date = DateTime.Now.ToString(), Publisher = UserServices.currentUser.Name });
            await NewsPage.connection.InsertAsync(new Photo() { Path = file.Path, Date = DateTime.Now.ToString(), Publisher = UserServices.currentUser.Name });
            file.Dispose();
        }
    }
}