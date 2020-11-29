using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TestAutomationProject.PageObject.Drawers;

namespace TestAutomationProject.PageObject.Pages
{
    public class ConfigurationPage : BasePageObject
    {
        private const string ConfigurationCardTag = "app-configuration-tile";
        public ConfigurationPage(IWebDriver driver) : base(driver) { }

        public int Count => GetWebElements(ConfigurationCardTag).Count;
        public ConfigurationPage Create()
        {
            throw new NotImplementedException("This is a demo project. This is a sample error.");
        }
    }
}
