using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AXamarinTestProject
    {
    public partial class MasterPage : ContentPage
        {
        public Button toCarouselButton;
        public Button toListViewButton;
        public Button logOut;
        public Button members;

        public MasterPage()
            {
            InitializeComponent();
            toCarouselButton = ToCarouselButton;
            toListViewButton = ToListViewButton;
            logOut = LogOut;
            members = Members;
            }
        }
    }

