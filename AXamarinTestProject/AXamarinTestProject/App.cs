using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Plugin.Settings;

namespace AXamarinTestProject
    {
    public class App : Application
        {
           int last_id;
           string current_user;            
        
        public const string DATABASE_NAME= "friends.db";
        public static UsersRepository database;
        public static UsersRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new UsersRepository(DATABASE_NAME);
                }
                return database;
            }
        }

        public static int GetLastId
            {
            get;
            }

        public App()
            {
            // The root page of your application
            last_id = CrossSettings.Current.GetValueOrDefault<int>("last_id", -1);
            current_user = CrossSettings.Current.GetValueOrDefault<string>("current_user", "empty");
         
                if (last_id <= -1)
                {                     
                    MainPage = new NavigationPage(new LoginPage());
                }
            else
                {               
                    if (Device.OS == TargetPlatform.iOS)
                        {
                            MainPage = new NavigationPage(new TabPage()); //(new MainPage());
                        }
                    else
                        MainPage = new MenuPage(); //(new MainPage());                        
                 }
            }

        protected override void OnStart()
            {
            // Handle when your app starts
               last_id = CrossSettings.Current.GetValueOrDefault<int>("last_id", -1);
               current_user = CrossSettings.Current.GetValueOrDefault<string>("current_user", "empty");
            }

        protected override void OnSleep()
            {
            // Handle when your app sleeps
            }

        protected override void OnResume()
            {
            // Handle when your app resumes
            }
        }
    }
