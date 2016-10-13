using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;

using Xamarin.Forms;

namespace AXamarinTestProject
    {
    public partial class CreateAccountPage : ContentPage
        {
        public CreateAccountPage()
            {
            InitializeComponent();
            }
        private void SaveFriend(object sender, EventArgs e)
            {
            var friend = (UserData)BindingContext;
            int result;
            if (!String.IsNullOrEmpty(friend.Name) && !String.IsNullOrEmpty(friend.Login) && !String.IsNullOrEmpty(friend.Password))
                {
                    result = App.Database.SaveItem(friend);
                    if (result != 0)
                        {
                        CrossSettings.Current.AddOrUpdateValue<int>("last_id", friend.Id); //сохранение ауториз.                             
                        Enter();                        
                        }
                    else
                        Answer.Text = "Такой пользователь уже существует";                     
                }
                else
                        Answer.Text = "Незаполненое поле..."; 
            }
        private void Enter()
            {
                Page usPage;
                if (Device.OS == TargetPlatform.iOS)
                            {
                                usPage = new TabPage();
                            }
                else
                            {
                                usPage = new MenuPage();
                            }
                 Application.Current.MainPage = usPage;
            }
        private void DeleteFriend(object sender, EventArgs e)
            {
            var friend = (UserData)BindingContext;
            App.Database.DeleteItem(friend.Id);
            this.Navigation.PopAsync();
            }
        private void Cancel(object sender, EventArgs e)
            {
            this.Navigation.PopAsync();
            }
        }        
    }
