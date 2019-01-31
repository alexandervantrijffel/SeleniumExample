using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Tests.SeleniumHelpers;

namespace Structura.GuiTests.PageObjects
{
    public class RequestGoldVisaPage
    {
        private readonly IWebDriver _driver;

        public RequestGoldVisaPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public IWebElement SubmitButton => _driver.FindElementByJQuery("form[name='Credit'] input[type='submit']");

        public IWebElement PasswordField => _driver.FindElementByJQuery("input[name='passwd']");

        [FindsBy(How = How.Id, Using = "_ctl0__ctl0_Content_Main_lblMessage")]
        public IWebElement SuccessMessage { get; set; }

        public void PerformRequest()
        {
            PasswordField.Clear();
            // sending a single quote is not possible with the Chrome Driver, it sends two single quotes!
            PasswordField.SendKeys("pass'--");
            SubmitButton.Click();
        }
    }
}
