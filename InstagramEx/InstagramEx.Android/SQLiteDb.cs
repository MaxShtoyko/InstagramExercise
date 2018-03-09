using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using InstagramEx.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]

namespace InstagramEx.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection(string DataBaseName)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, DataBaseName);

            return new SQLiteAsyncConnection(path);
        }
    }
}