using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Tests.PageObjects
{
    public class TransferFundsPage
    {
        private readonly IWebDriver _driver;

        // demonstrating XPath query
        [FindsBy(How = How.XPath, Using = "//table/tbody/tr/td/input[@name='transferAmount']")]
        public IWebElement AmountToTransferField { get; set; }

        [FindsBy(How = How.Id, Using = "transfer")]
        public IWebElement TransferMoneyButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@id='soapResp']/span")]
        public IWebElement TranferMoneyMessage { get; set; }

        public TransferFundsPage(IWebDriver _driver)
        {
            this._driver = _driver;
            PageFactory.InitElements(_driver, this);
        }

        public TransferFundsPage Transfer99Dollar()
        {
            AmountToTransferField.Clear();
            AmountToTransferField.SendKeys("99");

            TransferMoneyButton.Click();
            return this;
        }

    }
}
