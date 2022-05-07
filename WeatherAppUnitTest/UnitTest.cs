
using System;
using CitPeakWeatherApp.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace WeatherAppUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        WeatherManager _weatherManager;
        [TestMethod]
        public void TestMethod1()
        {
            _weatherManager = new WeatherManager();
           Assert .AreEqual(true,_weatherManager.SearchCity("Cairo",10),"Okay" );


        }
    }
}
