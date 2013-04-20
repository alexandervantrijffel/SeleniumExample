using System;
using System.Globalization;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Tests.Utilities;

namespace Tests
{
    [TestFixture]
    public class AltoroMutualTests
    {
        private IWebDriver _driver;
        private StringBuilder _verificationErrors;
        private string _baseURL;
        private SeleniumUtils _seleniumUtils;

        [SetUp]
        public void SetupTest()
        {
             _driver = new FirefoxDriver();
            //_driver = new ChromeDriver();
            //_driver =  new InternetExplorerDriver();
            _baseURL = "http://demo.testfire.net/";
            _seleniumUtils = new SeleniumUtils(_driver);
            _verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                _driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", _verificationErrors.ToString());
        }

        protected void Login()
        {
            _driver.Navigate().GoToUrl(_baseURL);

            _driver.FindElement(By.Id("_ctl0__ctl0_LoginLink")).Click();
            var element = _driver.FindElement(By.Id("uid"));
            element.Clear();
            element.SendKeys("admin'--");

            element = _driver.FindElement(By.Id("passw"));
            element.Clear();
            element.SendKeys("blah");

            _driver.FindElement(By.XPath("//input[@name=\"btnSubmit\"]")).Click();
        }
        [Test]
        public void LoginWithValidCredentialsShouldSucceed()
        {
            // Arrange
            
            // Act
            Login();
           
            // Assert
            Assert.IsTrue(_seleniumUtils.IsElementPresent(By.Id("btnGetAccount")));
        }

        [Test]
        public void TransferAmountShouldBeAccepted()
        {
            // Arrange

            Login();

            // Act

            _driver.FindElement(By.Id("_ctl0__ctl0_Content_MenuHyperLink3")).Click();
            
            // demonstrating XPath query
            var element = _driver.FindElement(By.XPath("//table/tbody/tr/td/input[@name='transferAmount']"));
            element.Clear();
            element.SendKeys("99");

            _driver.FindElement(By.Id("transfer")).Click();

            // Assert

            // Need to wait until the results are displayed on the web page
            Thread.Sleep(1000);

            var messageElement = _driver.FindElement(By.XPath("//span[@id='soapResp']/span"));
            Assert.IsNotNull(messageElement, "Soap response message not found");
            Assert.IsTrue(messageElement.Text.StartsWith("$99 was successfully transferred from Account 20 into Account 21", true, CultureInfo.InvariantCulture));
        }
    }
}
