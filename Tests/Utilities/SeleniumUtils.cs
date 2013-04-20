using OpenQA.Selenium;

namespace Tests.Utilities
{
    public class SeleniumUtils
    {
        private readonly IWebDriver _driver;
        public SeleniumUtils(IWebDriver driver)
        {
            _driver = driver;
        }
        public bool IsElementPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string CloseAlertAndGetItsText()
        {
            bool acceptNextAlert = true;
            
            IAlert alert = _driver.SwitchTo().Alert();
            if (acceptNextAlert)
            {
                alert.Accept();
            }
            else
            {
                alert.Dismiss();
            }
            return alert.Text;
        }
    }
}
