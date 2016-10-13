using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

using Xamarin.Forms;

namespace AXamarinTestProject
    {
    public partial class ConnectTexting : ContentPage //проверка подключения
        {

            Label connectionStateLbl;
            Label connectionDetailsLbl;        

        public ConnectTexting()
            {
            InitializeComponent();
            connectionStateLbl = new Label
            {
                Text = "Подключение отсутствует",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            connectionDetailsLbl = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            Content = new StackLayout
            {
                Children = {connectionStateLbl, connectionDetailsLbl}
            };
             
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
            }

         protected override void OnAppearing()
            {
                base.OnAppearing();
                CheckConnection();
            }
            // обработка изменения состояния подключения
            private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
            {
                CheckConnection();
            }
            // получаем состояние подключения
            private void CheckConnection()
            {
                connectionStateLbl.IsVisible = !CrossConnectivity.Current.IsConnected;
                connectionDetailsLbl.IsVisible = CrossConnectivity.Current.IsConnected;
 
                if (CrossConnectivity.Current != null &&
                    CrossConnectivity.Current.ConnectionTypes != null &&
                    CrossConnectivity.Current.IsConnected == true)
                {
                    var connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
                    connectionDetailsLbl.Text = connectionType.ToString();
                }
            }
            public static bool IsConnected()
            {

            if (CrossConnectivity.Current != null &&
                CrossConnectivity.Current.ConnectionTypes != null &&
                CrossConnectivity.Current.IsConnected == true)
                {
                var connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault();
                return true;
                }
            else
                return false;
            }
        }
    }
