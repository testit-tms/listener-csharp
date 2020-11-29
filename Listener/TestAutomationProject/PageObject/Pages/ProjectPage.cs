using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TestAutomationProject.PageObject.Drawers;

namespace TestAutomationProject.PageObject.Pages
{
    public class ProjectPage : BasePageObject
    {
        private const string ConfigurationTabTag = "a[href*='config']";

        public ProjectPage(IWebDriver driver) : base(driver) { }

        public ConfigurationPage OpenConfigurationPage()
        {
            Click(ConfigurationTabTag);
            return new ConfigurationPage(driver);
        }
    }
}
