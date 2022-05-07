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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CitPeakWeatherApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CityWeatherInfo : Page
    {
        WeatherManager weatherManager;
        private string cityName;
        public CityWeatherInfo()
        {
            this.InitializeComponent();
            weatherManager = new WeatherManager();
           

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            cityName = e.Parameter.ToString();


        }

        private   void page_Loaded(object sender, RoutedEventArgs e)
        {
          


        }



        private async  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var weatherInfo = await weatherManager.GetCityWeather(cityName);
            txt_city_name.Text = weatherInfo.name;
            txt_description.Text = weatherInfo.Weather[0].description;
            txt_humidity.Text = $"Humidity  {weatherInfo.main.humidity} %";
            txt_wind_speed.Text = $"Wind speed { weatherInfo.wind.speed}";
            // Get weather Icon 
            string iconId = weatherInfo.Weather[0].icon;
            var weatherIcon=  weatherManager.GetWeatherIcon(iconId);
            weatherImage .Source = weatherIcon;
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

 private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }


    }
}
