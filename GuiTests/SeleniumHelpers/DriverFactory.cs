using System;
using System.Configuration;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Structura.GuiTests.Utilities;
using Tests.SeleniumHelpers;

namespace Structura.GuiTests.SeleniumHelpers
{
    public enum DriverToUse
    {
        InternetExplorer,
        Chrome,
        Firefox
    }

    public class DriverFactory
    {
        private static readonly FirefoxProfile FirefoxProfile = CreateFirefoxProfile();

        private static FirefoxProfile CreateFirefoxProfile()
        {
            var firefoxProfile = new FirefoxProfile();
            firefoxProfile.SetPreference("network.automatic-ntlm-auth.trusted-uris", "http://localhost");
            return firefoxProfile;
        }

        public IWebDriver Create()
        {
            IWebDriver driver;
            var driverToUse = ConfigurationHelper.Get<DriverToUse>("DriverToUse");
            var useGrid = ConfigurationHelper.Get<bool>("UseGrid");
            if (useGrid)
            {
                driver = CreateGridDriver(driverToUse);
            }
            else
            {
                switch (driverToUse)
                {
                    case DriverToUse.InternetExplorer:
                        driver = new InternetExplorerDriver(AppDomain.CurrentDomain.BaseDirectory, new InternetExplorerOptions(), TimeSpan.FromMinutes(5));
                        break;
                    case DriverToUse.Firefox:
                        var firefoxProfile = FirefoxProfile;
                        driver = new FirefoxDriver(firefoxProfile);
                        driver.Manage().Window.Maximize();
                        break;
                    case DriverToUse.Chrome:
                        driver = new ChromeDriver();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            driver.Manage().Window.Maximize();
            var timeouts = driver.Manage().Timeouts();

            timeouts.ImplicitlyWait(TimeSpan.FromSeconds(ConfigurationHelper.Get<int>("ImplicitlyWait")));
            timeouts.SetPageLoadTimeout(TimeSpan.FromSeconds(ConfigurationHelper.Get<int>("PageLoadTimeout")));

            // Suppress the onbeforeunload event first. This prevents the application hanging on a dialog box that does not close.
            ((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");
            return driver;
        }

        public static IWebDriver CreateGridDriver(DriverToUse driverToUse)
        {
            var gridUrl = ConfigurationManager.AppSettings["GridUrl"];
            var desiredCapabilities = DesiredCapabilities.InternetExplorer();
            switch (driverToUse)
            {
                case DriverToUse.Firefox:
                    desiredCapabilities = DesiredCapabilities.Firefox();
                    desiredCapabilities.SetCapability(FirefoxDriver.ProfileCapabilityName, FirefoxProfile);

                    break;
                case DriverToUse.InternetExplorer:
                    desiredCapabilities = DesiredCapabilities.InternetExplorer();
                    break;
                case DriverToUse.Chrome:
                    desiredCapabilities = DesiredCapabilities.Chrome();
                    break;
            }
            desiredCapabilities.IsJavaScriptEnabled = true;
            var remoteDriver = new ExtendedRemoteWebDriver(new Uri(gridUrl), desiredCapabilities, TimeSpan.FromSeconds(180));
            var nodeHost = remoteDriver.GetNodeHost();
            Debug.WriteLine("Running tests on host " + nodeHost);
            return remoteDriver;
        }
    }
}