using System;
//using Xamarin.Forms;
using System.IO;
using AXamarinTestProject.iOS;
using Xamarin.Forms;
//using System.Runtime.CompilerServices;

//using System.Runtime.CompilerServices;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace AXamarinTestProject.iOS
    {
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // папка библиотеки
            var path = Path.Combine(libraryPath, sqliteFilename);
             
            return path;
        }
    }
}
