using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;

namespace AXamarinTestProject
    {
    public class UsersRepository //класс для манипуляций с БД
        {
            SQLiteConnection database;
            public UsersRepository(string filename)
            {
                string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
                database = new SQLiteConnection(databasePath);
                database.CreateTable<UserData>();
            }
            public IEnumerable<UserData> GetItems()
            {
                return (from i in database.Table<UserData>() select i).ToList();
             
            }
            public UserData GetItem(int id)
            {
                return database.Get<UserData>(id);
            }
             public UserData GetItemByLogin(string login_name)
             {           
                return database.Get<UserData>(i=> i.Login == login_name);
             }         

            public int DeleteItem(int id)
            {
                return database.Delete<UserData>(id);
            }
            public int SaveItem(UserData item)
            {
            try
                {

                if (item.Id != 0)
                    {
                    database.Update(item);
                    return item.Id;
                    }
                else
                    {
                    return database.Insert(item);
                    }
                }
            catch (SQLiteException e)
                {
                return 0;
                }
            }
        }
    }
