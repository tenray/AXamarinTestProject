using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Settings;
using SQLite;

namespace AXamarinTestProject
    {
    public partial class LoginPage : ContentPage
        {
        public LoginPage()
            {
            InitializeComponent();
            }
        private void OnClick(object sender, EventArgs e)
            {
            Answer.Text = "{NameBox.Text, PasswordBox.Text}";            
            }
        // обработка нажатия элемента в списке
        private async void Send(object sender, EventArgs e)
            {
                //валидация базы данных
                //1. получить бд IEnumerable<UserData>
                //2. есть ли такой пользователь в бд, если нет - ошибка
                //3. если есть, сравнить введенный и эаталонный пароль
                //4. если все правильно - вход на главную, иначе - ошибка
                var base_inst = App.Database;
                try
                    {
                    UserData user = base_inst.GetItemByLogin(LoginBox.Text);
                    if (user.Password == PasswordBox.Text)
                        {
                           // UserData us;
                            Page usPage;
                        if (Device.OS == TargetPlatform.iOS)
                            {
                                usPage = new TabPage();
                            }
                        else
                            {
                                usPage = new MenuPage();
                            }                      
                        CrossSettings.Current.AddOrUpdateValue<int>("last_id",user.Id); //сохранение ауториз.                       
                        Application.Current.MainPage = usPage;
                        }
                    else
                        {
                            Answer.Text = "Неправильный пароль";
                        }             
                    }
                catch (System.InvalidOperationException ex) //System.InvalidOperationException
                    {
                        Answer.Text = "Такого юзера не существует";
                        LoginBox.Text.Remove(0);                     
                    }
            }
            // к созданию нового пользователя
        private async void CreateNewAccount(object sender, EventArgs e)
            {
                UserData us = new UserData();
                CreateAccountPage usPage = new CreateAccountPage();//UserDataPage();
                usPage.BindingContext = us;
                await Navigation.PushAsync(usPage);
            }
        }
    }
