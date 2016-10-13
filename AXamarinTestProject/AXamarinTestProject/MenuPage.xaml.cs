using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;

using Xamarin.Forms;

namespace AXamarinTestProject
    {
    public partial class MenuPage : MasterDetailPage
        {
        public MenuPage()
            {
            InitializeComponent();
            this.IsPresented = true;
            masterPage.toCarouselButton.Clicked += ToCarousel;
            masterPage.toListViewButton.Clicked += ToListView;
            masterPage.logOut.Clicked += UserLogOut;
            masterPage.members.Clicked += ViewMembersPage;
            }

        private void ViewMembersPage(object sender, EventArgs e)
            {
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainPage)));
            IsPresented = false;
            }

        private void UserLogOut(object sender, EventArgs e)
            {
            IsPresented = false;
            CrossSettings.Current.AddOrUpdateValue<int>("last_id", -1); //сброс ауторизации          
            Application.Current.MainPage = new NavigationPage(new LoginPage());
            }

        public void ToCarousel(object sender, EventArgs e)
            {
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MyCarouselPage)));
            IsPresented = false;
            }
        public void ToListView(object sender, EventArgs e)
            {
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ListViewPage)));
            IsPresented = false;
            }
        }
    }