using System;
using System.Globalization;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Structura.GuiTests.SeleniumHelpers
{
    //Specflow:
    //[Binding]
    public class SeleniumHelper
    {
        private static IWebDriver _driver;

        public static void ResetDriver()
        {
            try
            {
                if (_driver != null)
                {
                    Driver.Close();
                    Driver.Quit();
                    _driver = null;
                }
            }
            catch (Exception ex) { }
        }
        public static IWebDriver Driver
        {
            get
            {
                if (_driver != null)
                {
                    return _driver;
                }
                _driver = new DriverFactory().Create();
                // Avoid synchronization issues by applying timed delay to each step if necessary
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes((5));
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(5);
                return _driver;
            }
        }

        //Specflow:
        //[AfterTestRun]
        public static void AfterTestRun()
        {
            ResetDriver();
        }


        public static void Wait(int miliseconds, int maxTimeOutSeconds = 60)
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 1, maxTimeOutSeconds));
            var delay = new TimeSpan(0, 0, 0, 0, miliseconds);
            var timestamp = DateTime.Now;
            wait.Until(webDriver => (DateTime.Now - timestamp) > delay);
        }


        public static string GetCosasBuildVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var result = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.MinorRevision);

            return result;
        }
    }
}
