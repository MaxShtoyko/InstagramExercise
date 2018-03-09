using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace InstagramEx.Services
{
    static class UserServices
    {
        static public SQLiteAsyncConnection connection;
        static private ObservableCollection<User> Users;
        public static User currentUser = new User() { Name = "Loading..." };

        static UserServices()
        {
            CreadteDb();
        }
        static public async void CreadteDb()
        {
            connection = DependencyService.Get<ISQLiteDb>().GetConnection("Users.db3");
            await connection.CreateTableAsync<User>();
            var users = await connection.Table<User>().ToListAsync();
            Users = new ObservableCollection<User>(users);
        }

        static public bool InDatabase(string login)
        {
            var result = from User in Users
                          where User.Login == login
                          select User;

            return result.Count()!=0;
        }

        static public async void AddToDatabase(User user)
        {
            Users.Add(user);
            await connection.InsertAsync(user);
        }

        static public User getUser(string login)
        {
            foreach(User u in Users)
            {
                if (u.Login == login)
                    return u;
            }

            return null;
        }
    }
}
