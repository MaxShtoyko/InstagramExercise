using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InstagramEx.Models;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InstagramEx.Services;

namespace InstagramEx
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsPage : ContentPage
	{
        public static SQLiteAsyncConnection connection;

        public NewsPage ()
		{           
			InitializeComponent ();
            connection = DependencyService.Get<ISQLiteDb>().GetConnection("Images.db3");
            CreadteDb();
		}

        public async void CreadteDb()
        {
            await connection.CreateTableAsync<Photo>();
            var images = await connection.Table<Photo>().ToListAsync();
            UserServices.currentUser.Images = new ObservableCollection<Photo>(images);
            imageList.ItemsSource = UserServices.currentUser.Images.Reverse();            
        }

	}
}