using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFlorDeLiz.Infraestructure;
using AppFlorDeLiz.Droid;
using SQLite.Net.Platform.XamarinAndroid;
using System.IO;
using SQLite.Net;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace AppFlorDeLiz.Droid
{
    class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "FDLiz.db3";
            string documentsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsFolder, dbName);
            var platform = new SQLitePlatformAndroid();
            return new SQLiteConnection(platform, path);
        }
    }
}