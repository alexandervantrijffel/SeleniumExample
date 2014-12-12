using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Structura.GuiTests.PageObjects
{
    public class TransferFundsPage
    {
        // demonstrating XPath query
        [FindsBy(How = How.XPath, Using = "//table/tbody/tr/td/input[@name='transferAmount']")]
        public IWebElement AmountToTransferField { get; set; }

        [FindsBy(How = How.Id, Using = "transfer")]
        public IWebElement TransferMoneyButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@id='soapResp']/span")]
        public IWebElement TranferMoneyMessage { get; set; }

        public TransferFundsPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public void Transfer99Dollar()
        {
            AmountToTransferField.Clear();
            AmountToTransferField.SendKeys("99");
            TransferMoneyButton.Click();
        }
    }
}
