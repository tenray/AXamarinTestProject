using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;

using Xamarin.Forms;

namespace AXamarinTestProject
    {
    public partial class MainPage : ContentPage
        {
        public MainPage()
            {
            InitializeComponent();
            Answer.Text = "toolbar start!";

                /*public static readonly BindableProperty ToolbarItemCommand1Property =
                BindableProperty.Create("ToolbarItemCommand1", typeof(Command), typeof(), default(Command));
                public Command ToolbarItemCommand
                {
                    get { return (Command)GetValue(ToolbarItemCommand1Property); }
                    set { SetValue(ToolbarItemCommand1Property, value); }
                }

                ToolbarItemCommand1 = new Command( () =>
                {
                    Answer.Text = "123232131";
                });*/
            }
        protected override void OnAppearing()
            {
                friendsList.ItemsSource = App.Database.GetItems();
                base.OnAppearing();
            }
            // обработка нажатия элемента в списке
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
            {
                UserData selectedFriend = (UserData)e.SelectedItem;
                CreateAccountPage friendPage = new CreateAccountPage(); //UserDataPage();
                friendPage.BindingContext = selectedFriend;
                await Navigation.PushAsync(friendPage);
            }
            // обработка нажатия кнопки добавления
        private async void CreateFriend(object sender, EventArgs e)
            {
                UserData friend = new UserData();
                CreateAccountPage friendPage = new CreateAccountPage();//UserDataPage();
                friendPage.BindingContext = friend;
                await Navigation.PushAsync(friendPage);
            }
        private void ToolbarItemCommand()
            {
                Answer.Text = "toolbar worked!";
                /*CrossSettings.Current.AddOrUpdateValue<int>("last_id", -1); //сброс ауториз.  
                Application.Current.MainPage = null;            
                Application.Current.MainPage = new NavigationPage(new LoginPage());*/
            }
         public void OnActivated ()
            {
                  Answer.Text = "activ!";     
            }
        }
    }
