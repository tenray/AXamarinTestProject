using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

using System.Windows.Input;

using System.Threading;
using System.Text.RegularExpressions;

using Xamarin.Forms;

namespace AXamarinTestProject
    {
    public partial class ListViewPage : ContentPage, INotifyPropertyChanged
        {
      
        List<ServerRequestData> res; 
        public Entry searchLine; //строка поиска
        Button searchButton; //кнопка поиска
        public StackLayout main_stackLayout; //главный стек
        public Label output; //для проверок
        StackLayout currentList = null; //отдельний стек для списка, удаляем, при новом запросе.
        DataTemplate currentListViewFormat; //текущий шаблон отображения. Инициализируется далее в коде
        public int listFormatFlag = 0; //флаг. задает текущее отображение списка. runtime реализации переключения нет
        enum listViewFormat //список доступных форматов отображения
            {
                First,
                Second
            }      
      
        public ListViewPage()
            {
            InitializeComponent();
            SetupViewTemplates(); //создаем делегаты шаблонов отображения списка
            output = new Label() { Text = "Привет из Xamarin Forms" }; //проверка          
            
            //создаем первичную структуру страницы
            searchLine = new Entry { Placeholder = "запрос..." };
            searchLine.HorizontalOptions = LayoutOptions.EndAndExpand;
            searchLine.WidthRequest = 800;
          
                 Button searchButton = new Button
                    {
                        Text = "Search",
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                        BorderWidth = 1,
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.Start
                    };
            searchButton.Clicked += OnButtonClicked;
            searchButton.WidthRequest = 200;

                StackLayout header = new StackLayout()
                    {
                        Children = {searchButton,searchLine}
                    };
                header.Orientation = StackOrientation.Horizontal;
                //header.HorizontalOptions = "EndAndExpand";
    
                 main_stackLayout = new StackLayout()
                    {
                        Children = {header, output}
                    };
                main_stackLayout.Orientation = StackOrientation.Vertical;
                this.Content = main_stackLayout;

            if (ConnectTexting.IsConnected()) //проверка интернета
                {                
                output.Text = "ONline";
                searchButton.IsEnabled = true;               
                }
            else
                {
                output.Text = "no internet connection";
                searchButton.IsEnabled = false; //если нет интернета делаем кнопку не активной
                }
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;          
            }

        private void SetupViewTemplates()//шаблоны отображения списка. runtime переключатель не предусмотрен. флаг переключается в коде
            {
                if (listFormatFlag == (int)listViewFormat.First)
                    {
                    currentListViewFormat = new DataTemplate(() =>
                    {
                            //poster - строка адреса картинки
                        Image img = new Image();
                        img.SetBinding(Image.SourceProperty, "Poster");

                            // привязка к свойству Title
                            Label titleLabel = new Label();
                        titleLabel.SetBinding(Label.TextProperty, "Title");

                            // привязка к свойству Year
                            Label yearLabel = new Label();
                        yearLabel.SetBinding(Label.TextProperty, "Year");

                            // создаем объект ViewCell.
                            return new ViewCell
                            {
                            View = new StackLayout
                                {
                                Padding = new Thickness(10, 25),
                                Orientation = StackOrientation.Vertical,
                                Children = { img, titleLabel, yearLabel }
                                }
                            };
                    });
                    }
                else
                    {
                        currentListViewFormat = new DataTemplate(() =>
                        {
                            Label titleLabel = new Label();
                        titleLabel.SetBinding(Label.TextProperty, "Title");

                            Image img = new Image();
                            img.SetBinding(Image.SourceProperty, "Poster");
                            img.HorizontalOptions = LayoutOptions.Center;
                                
                             Button btn = new Button
                                {
                                    Text = "Vote",
                                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                                    BorderWidth = 1,
                                    HorizontalOptions = LayoutOptions.Center,
                                    VerticalOptions = LayoutOptions.End
                                };
                                
                                Label msg = new Label() { Text =""};                          
                            btn.Clicked += (sender, args) =>
                                {
                                    Button b = (Button)sender;
                                    b.IsEnabled = false;
                                    msg.Text = "You VOTE!";
                                };
                                

                                // создаем объект ViewCell.
                                return new ViewCell
                                {
                                View = new StackLayout
                                    {
                                    Padding = new Thickness(10, 25, 10, 10),
                                    Orientation = StackOrientation.Vertical,
                                    Children = { titleLabel, img, btn, msg }
                                    }
                                };
                        });
                    }
            }        

        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (ConnectTexting.IsConnected())
                {                    
                searchButton.IsEnabled = true;               
                }
            else
                {               
                searchButton.IsEnabled = false;
                }
        }

        private void OnButtonClicked(object sender, System.EventArgs e)
            {
                Button button = (Button)sender;
                button.IsEnabled = false;                  

             Task t = ServerRequest();
             TimeSpan ts = TimeSpan.FromSeconds(10); //всременной промежуток 10с
             t.Wait(ts); //ждем ответ от сервера некоторое время
           // ServerImotationRequest();
            if (res != null && res.Count != 0) //проверяем получины ли данные
            {
                output.Text = res.Count.ToString(); //проверка, кол-во единиц данных
                BuildList(); //строим список
            }
             else
               output.Text = "No answer...";    
             button.IsEnabled = true;       
        }

        private void ServerImotationRequest() //for test
            {
                 var content = @"{""Search"":[{""Title"":""The Matrix"",""Year"":""1999"",""imdbID"":""tt0133093"",""Type"":""movie"",""Poster"":""https://images-na.ssl-images-amazon.com/images/M/MV5BMDMyMmQ5YzgtYWMxOC00OTU0LWIwZjEtZWUwYTY5MjVkZjhhXkEyXkFqcGdeQXVyNDYyMDk5MTU@._V1_SX300.jpg""},{""Title"":""The Matrix Reloaded"",""Year"":""2003"",""imdbID"":""tt0234215"",""Type"":""movie"",""Poster"":""https://images-na.ssl-images-amazon.com/images/M/MV5BMjA0NDM5MDY2OF5BMl5BanBnXkFtZTcwNzg5OTEzMw@@._V1_SX300.jpg""},{""Title"":""The Matrix Revolutions"",""Year"":""2003"",""imdbID"":""tt0242653"",""Type"":""movie"",""Poster"":""https://images-na.ssl-images-amazon.com/images/M/MV5BMTkyNjc4NTQzOV5BMl5BanBnXkFtZTcwNDYzMTQyMQ@@._V1_SX300.jpg""},{""Title"":""The Matrix Revisited"",""Year"":""2001"",""imdbID"":""tt0295432"",""Type"":""movie"",""Poster"":""http://ia.media-imdb.com/images/M/MV5BMTIzMTA4NDI4NF5BMl5BanBnXkFtZTYwNjg5Nzg4._V1_SX300.jpg""},{""Title"":""Enter the Matrix"",""Year"":""2003"",""imdbID"":""tt0277828"",""Type"":""game"",""Poster"":""http://ia.media-imdb.com/images/M/MV5BMjA4NTYwNjk0M15BMl5BanBnXkFtZTgwODk3MDMwMTE@._V1_SX300.jpg""},{""Title"":""The Matrix: Path of Neo"",""Year"":""2005"",""imdbID"":""tt0451118"",""Type"":""game"",""Poster"":""http://ia.media-imdb.com/images/M/MV5BYWM2ZWU5MDUtYTU2ZS00ZDFmLWIyNGEtNWZkMjRmZjI2YzMzXkEyXkFqcGdeQXVyMTA1OTEwNjE@._V1_SX300.jpg""},{""Title"":""CR: Enter the Matrix"",""Year"":""2009"",""imdbID"":""tt1675286"",""Type"":""game"",""Poster"":""http://ia.media-imdb.com/images/M/MV5BMTExMzY3NTQ1NjleQTJeQWpwZ15BbWU3MDAyMjk2NzM@._V1_SX300.jpg""},{""Title"":""Sex and the Matrix"",""Year"":""2000"",""imdbID"":""tt0274085"",""Type"":""movie"",""Poster"":""N/A""},{""Title"":""The Matrix Online"",""Year"":""2005"",""imdbID"":""tt0390244"",""Type"":""game"",""Poster"":""http://ia.media-imdb.com/images/M/MV5BMTYxNTM5MDkwMF5BMl5BanBnXkFtZTcwMTAzMTEzMQ@@._V1_SX300.jpg""},{""Title"":""Return to Source: Philosophy & 'The Matrix'"",""Year"":""2004"",""imdbID"":""tt0439783"",""Type"":""movie"",""Poster"":""https://images-na.ssl-images-amazon.com/images/M/MV5BODIwNDQ3MTYtMWZiYS00MDYyLWI4ZGEtZjBkODU4NTgyNDFkXkEyXkFqcGdeQXVyMjM3ODA2NDQ@._V1_SX300.jpg""}],""totalResults"":""57"",""Response"":""True""}";
                  try
                {                                                                    
                    JObject o = JObject.Parse(content);              

                    res = new List<ServerRequestData>();      
                    var str = o.SelectToken(@"$.Search");                  
                    foreach (var item in str)
                        {                   
                        var requestData = JsonConvert.DeserializeObject<ServerRequestData>(item.ToString());                    
                        res.Add(requestData);
                        }
                    }
                catch(Exception ex)
                {
                    output.Text = "ERROR";
                }
            }

        private async Task ServerRequest()
            {    
                string text_query = searchLine.Text; //форматирование введенного в поисковую строку текста для внедрения в запрос
                text_query = text_query.Trim();
                text_query = System.Text.RegularExpressions.Regex.Replace(text_query, @"\s+", "+");
                string url = "http://www.omdbapi.com/?s=" +text_query+ "&y=&plot=full&r=json";
               // searchLine.Text = url; //проверка
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(url);
                    var response = await client.GetAsync(client.BaseAddress);
                    response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка
                  
                    // десериализация ответа в формате json
                    var content = await response.Content.ReadAsStringAsync();                                                  
                    JObject o = JObject.Parse(content);              
                    res = new List<ServerRequestData>(); //создание списка     
                    var str = o.SelectToken(@"$.Search");                  
                    foreach (var item in str)
                        {                   
                        var requestData = JsonConvert.DeserializeObject<ServerRequestData>(item.ToString());                    
                        res.Add(requestData); //заполнение списка десериализоваными на основе ServerRequestData данными
                        }
                    }
                catch(Exception ex)
                {
                    output.Text = "ERROR"; 
                }
            }

       /* public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }*/

        private void BuildList()
            {                     

            ListView listView = new ListView //формируем список
                {                
                ItemsSource = res, // Определяем источник данных                             
                ItemTemplate = currentListViewFormat   // Определяем формат отображения данных                  
                };

            listView.HasUnevenRows = true;

                if (currentList != null) //удаление контейнера со старым списком, при новом запросе
                    {
                        var IsoldlistView = main_stackLayout.Children.Contains(currentList);
                        if (IsoldlistView)
                            {
                            main_stackLayout.Children.Remove(currentList);
                            }
                        else
                            output.Text = "not matched";
                    }
            
            currentList = new StackLayout { Children =  {listView}};  //контейнер со списком
            main_stackLayout.Children.Add(currentList);//new StackLayout { Children =  {listView}});
            }               
        }
    }
