using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Tests.PageObjects
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.Id, Using = "_ctl0__ctl0_LoginLink")]
        public IWebElement SignInLink { get; set; }

        [FindsBy(How = How.Id, Using = "uid")]
        public IWebElement UserIdField { get; set; }

        [FindsBy(How = How.Id, Using = "passw")]
        public IWebElement PasswordField { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name=\"btnSubmit\"]")]
        public IWebElement LoginButton { get; set; }

        public MainPage LoginAsAdmin(string baseUrl)
        {
            _driver.Navigate().GoToUrl(baseUrl);
            SignInLink.Click();
            UserIdField.Clear();
            // sending a single quote is not possible with the Chrome Driver, it sends two single quotes!
            UserIdField.SendKeys("admin'--");

            PasswordField.Clear();
            PasswordField.SendKeys("blah");

            LoginButton.Click();
            return new MainPage(_driver);
        }

        public MainPage LoginAsNobody(string baseUrl)
        {
            _driver.Navigate().GoToUrl(baseUrl);
            SignInLink.Click();
            UserIdField.Clear();
            // sending a single quote is not possible with the Chrome Driver, it sends two single quotes!
            UserIdField.SendKeys("nobody");

            PasswordField.Clear();
            PasswordField.SendKeys("blah");

            LoginButton.Click();
            return new MainPage(_driver);
        }
    }
}

