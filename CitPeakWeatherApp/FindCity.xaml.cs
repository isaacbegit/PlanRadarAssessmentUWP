using CitPeakWeatherApp.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CitPeakWeatherApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FindCity : Page
    {
        WeatherManager weatherManager;
        LogManager logManager;
        public string  ImageSource { get; set; }
        public FindCity()
        {
            this.InitializeComponent();
            
            weatherManager = new WeatherManager();
            logManager= new LogManager();
           
        }

        public List<SearchResults> bindCities = new List<SearchResults>();
        private async  void btnSearch_Click(object sender, RoutedEventArgs e)
        {

           
            var citName = cityName.Text.Trim();
            imagGif.Visibility = Visibility.Visible;
           
            var resultCities =await  weatherManager.SearchCity(citName, 10);
            imagGif.Visibility = Visibility.Collapsed;


            if (resultCities != null)
            {
                foreach (var city in resultCities)
            {
                var bcity = new SearchResults()
                {
                    name = city.name,
                    display = $"{city.name },{city.state},{city.country  } "

                };
                bindCities.Add(bcity);

            }

           
                listCities.ItemsSource= bindCities;
                listCities.SelectedValuePath = "name";
                listCities.DisplayMemberPath = "display";

            }
        }

        private void CityWeatherView(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void listCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                Frame contentFrame = Window.Current.Content as Frame;
                MainPage mp = contentFrame.Content as MainPage;
                Grid grid = mp.Content as Grid;
                Frame my_Frame = grid.FindName("mainframe") as Frame;

                var selCity = listCities.SelectedItem as SearchResults;
                // Save selected city.
                weatherManager.SaveCity(selCity.display, selCity.name);

                string _cityName = selCity.name;
                my_Frame.Navigate(typeof(CityWeatherInfo), _cityName);
            }catch (Exception ex)
            {
                logManager.Log (ex);
            }
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            
            
        }
    }
   


}
