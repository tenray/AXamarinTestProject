using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
//using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AXamarinTestProject;
using System.IO;
using Xamarin.Forms;
//using AXamarinTestProject.;
//using System.Runtime.CompilerServices;

[assembly: Dependency(typeof(AXamarinTestProject.Droid .SQLite_Android))]
namespace AXamarinTestProject.Droid 
    {
    public class SQLite_Android : ISQLite
        {
             public SQLite_Android() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;
        }
        }
    }