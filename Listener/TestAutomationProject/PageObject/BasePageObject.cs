using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestAutomationProject.PageObject
{
    public class BasePageObject
    {
        protected readonly IWebDriver driver;
        public const int AnimationTime = 2000;
        public const int SmallAnimationTime = 200;

        public BasePageObject(IWebDriver driver) => this.driver = driver;

        public ReadOnlyCollection<IWebElement> GetWebElements(string tag, int timeoutInMilliseconds = 5000)
        {
            var webElements = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());

            for (int i = timeoutInMilliseconds; i >= 0; i -= SmallAnimationTime)
            {
                webElements = driver.FindElements(By.CssSelector(tag));
                if (webElements.Any())
                    return webElements;

                Wait(SmallAnimationTime);
            }

            return webElements;
        }

        public IWebElement GetWebElement(string tag, int timeoutInMilliseconds = 5000)
        {
            return GetWebElements(tag, timeoutInMilliseconds).First();
        }

        public void SendKeys(string tag, string text)
        {
            GetWebElement(tag).SendKeys(text);
        }

        public void Click(string tag)
        {
            var numberOfAttempts = 5;
            for (int i = numberOfAttempts - 1; i >= 0; i--)
            {
                try
                {
                    GetWebElement(tag).Click();
                    return;
                }
                catch (Exception)
                {
                    Wait(SmallAnimationTime);
                }
            }
        }

        public void Wait(int timInMilliseconds) => Thread.Sleep(timInMilliseconds);
    }
}
