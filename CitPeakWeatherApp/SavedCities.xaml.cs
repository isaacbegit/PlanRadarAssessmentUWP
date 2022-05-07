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
    public sealed partial class SavedCities : Page
    {
        LogManager logManager;
        WeatherManager weatherManager;
        public SavedCities()
        {
            this.InitializeComponent();
            weatherManager = new WeatherManager();
            logManager = new LogManager();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Load Saved Cites;
                var lstSavedCites= weatherManager.GetSavedCites();
                listCities.ItemsSource = lstSavedCites;
                listCities.SelectedValuePath = "name";
                listCities.DisplayMemberPath = "display";

            }
            catch (Exception ex)
            {


            }
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
                          
                string _cityName = selCity.name;
                my_Frame.Navigate(typeof(CityWeatherInfo), _cityName);
            }
            catch (Exception ex)
            {
                logManager.Log(ex);
            }
        }
    }
}
