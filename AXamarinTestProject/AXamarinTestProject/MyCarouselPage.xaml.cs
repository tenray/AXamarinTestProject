using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AXamarinTestProject
    {
    public partial class MyCarouselPage : CarouselPage
        {
        public MyCarouselPage()
            {
            InitializeComponent();
            }
         public void switcher_Toggled(object sender, ToggledEventArgs e) //переключатель на стр.2 изменяет состояние кнопки на стр.1
            {
            StateExampleButton.IsEnabled = !StateExampleButton.IsEnabled; 
            }
        //TODO:  сохранение состояния переключателя и кнопки, инициализация согласно этому состоянию при создании странички
        }
    }
