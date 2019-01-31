using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Tests.PageObjects;

namespace Structura.GuiTests.PageObjects
{
    public class MainPage
    {
        private readonly IWebDriver _driver;

        public MainPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.Id, Using = "btnGetAccount")]
        public IWebElement GetAccountButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"_ctl0__ctl0_Content_Main_promo\"]/table/tbody/tr[3]/td/a")]
        public IWebElement TransferFundsButton { get; set; }

        public RequestGoldVisaPage NavigateToTransferFunds()
        {
            TransferFundsButton.Click();
            return new RequestGoldVisaPage(_driver);
        }
    }
}
