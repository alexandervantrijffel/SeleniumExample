using System;
using System.Globalization;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Tests.PageObjects;
using Tests.Utilities;

namespace Tests
{
    [TestFixture]
    public class AltoroMutualTests
    {
        private IWebDriver _driver;
        private StringBuilder _verificationErrors;
        private string _baseUrl;
        private SeleniumUtils _seleniumUtils;

        [SetUp]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            //_driver = new ChromeDriver();
            //_driver =  new InternetExplorerDriver();
            _baseUrl = "http://demo.testfire.net/";
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

        [Test]
        public void LoginWithValidCredentialsShouldSucceed()
        {
            // Arrange
            var page = new LoginPage(_driver);

            // Act
            var mainPage = page.LoginAsAdmin(_baseUrl);

            // Assert
            var element = mainPage.GetAccountButton;
            Assert.IsTrue(element.Displayed);
        }

        [Test]
        [ExpectedException(typeof(NoSuchElementException))]
        public void LoginWithInvalidCredentialsShouldFail()
        {
            // Arrange
            var page = new LoginPage(_driver);

            // Act
            var mainPage = page.LoginAsNobody(_baseUrl);

            // Assert
            var element = mainPage.GetAccountButton;
            var displayed = element.Displayed; // throws exception if not found
        }


        [Test]
        public void TransferAmountShouldBeAccepted()
        {
            // Arrange
            var page = new LoginPage(_driver);
            var mainPage = page.LoginAsAdmin(_baseUrl);
            var transferFundsPage = mainPage.NavigateToTransferFunds();

            // Act
            transferFundsPage.Transfer99Dollar();

            // Assert

            // Need to wait until the results are displayed on the web page
            Thread.Sleep(1000);

            Assert.IsTrue(transferFundsPage.TranferMoneyMessage.Text.StartsWith(
                "$99 was successfully transferred from Account 20 into Account 21"
                    , true, CultureInfo.InvariantCulture));
        }
    }
}


