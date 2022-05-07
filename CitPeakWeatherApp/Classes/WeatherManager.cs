using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace CitPeakWeatherApp.Classes
{
    public  class WeatherManager
    {
        LogManager logManager;
        string savedCitiesPath = System.IO.Path .GetTempPath() + @"\WeatherApp\cities.txt";
        public WeatherManager()
        {
            logManager = new LogManager();
        }



        public async Task<List<City>> SearchCity(string cityName,int limit)
        {
            try
            {
                var apiUrl = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&appid=f5cb0b965ea1564c50c6f1b74534d823&limit={limit}";
                var http = new HttpClient();
                var response = await http.GetAsync(apiUrl);
                var result = await response.Content.ReadAsStringAsync();
                var cities = JsonConvert.DeserializeObject<List<City>>(result);

                return cities;
            }catch (Exception ex)
            {
                
                logManager.Log(ex);
                return null;
            }

        }

        public async Task<Root>  GetCityWeather(string cityName)
        {
            try
            {
                var apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid=f5cb0b965ea1564c50c6f1b74534d823";
                var http = new HttpClient();
                var response = await http.GetAsync(apiUrl);
                var result = await response.Content.ReadAsStringAsync();
                var weatherInfo = JsonConvert.DeserializeObject<Root>(result);

                return weatherInfo;
            }catch (Exception ex)
            {
                logManager.Log(ex);
                return null;
            }



        }

        /// <summary>
        /// /Save Selected City  will appear on App Home page 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="cityName"></param>
        /// <returns> true /false </returns>
        public bool SaveCity(string description,string cityName)
        {
            try
            {


                var writeLine = $"{cityName}|{description}";
                string getdirectory = Path.GetDirectoryName(savedCitiesPath);
               
                if (!Directory.Exists(getdirectory))
                {
                    Directory.CreateDirectory(getdirectory);

                }
                    


                using (StreamWriter w = File.AppendText(savedCitiesPath ))
                {
                    w.WriteLine(writeLine);
                }


                return true;

            }catch (Exception ex)
            {
                logManager.Log(ex);
                return false ;
            }

        }

        public List<SearchResults> GetSavedCites()
        {
            try
            {
                var lstSavedCites= new List<SearchResults>();   
                var lines = File.ReadAllLines(savedCitiesPath );
                foreach (var line in lines )
                {
                    var token = line.Split("|");
                    if (token.Length > 0)
                    {
                        var cityInfo = new SearchResults()
                        {
                             name =token[0],
                             display = token[1]
                        };
                        lstSavedCites.Add (cityInfo);
                    }

                }
                return lstSavedCites;

            }
            catch (Exception ex)
            {
                logManager.Log(ex);
                return null;

            }
            

        }



        public  BitmapImage GetWeatherIcon(string iconId)
        {
            try
            {
                string icon = string.Format("Http://openweathermap.org/img/w/{0}.png", iconId);
                BitmapImage bitmapImage = new BitmapImage(new Uri(icon, UriKind.Absolute));
                return bitmapImage;

            }catch(Exception ex)
            {
                logManager.Log(ex);
                return null;
            }
        }



    }
}
