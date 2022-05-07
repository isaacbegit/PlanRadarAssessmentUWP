using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitPeakWeatherApp.Classes
{
    public  class LogManager
    {
        private string   logPath = System.Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) +@"\WeatherApp\logs";
        public void Log(Exception ex)
        {
            try
            {
                if (!Directory.Exists(logPath ) )
                {
                    Directory.CreateDirectory(logPath);
                }
                string fullError = $"{ex.Message }\n{ex.Source }\n{ex.InnerException }\n{ex.Data}";
                string LogFilePath = logPath + @"\" + DateTime.Now.ToString("dd.MM.yyy") + ".txt";
                using (StreamWriter w = File.AppendText(LogFilePath))
                {
                    w.WriteLine(DateTime.Now.ToShortTimeString() + ": " + fullError);
                }
                

            }
            catch
            {
            }
        }
    }
}
