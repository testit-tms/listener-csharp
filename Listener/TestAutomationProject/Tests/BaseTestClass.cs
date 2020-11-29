using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Html5;
using System.Threading;

namespace TestAutomationProject.Tests
{
    [TestClass]
    public class BaseTestClass
    {
        protected static IWebDriver driver;

        [AssemblyInitialize]
        public static void ClassInit(TestContext context)
        {
            driver = new ChromeDriver();
        }

        [TestInitialize]
        public void Initialize()
        {
            driver.Navigate().GoToUrl("http://demo.testit.software/");
            Thread.Sleep(5000);
        }

        [TestCleanup]
        public void Cleanup()
        {
            var hasWebStorage = (driver as IHasWebStorage);
            if (hasWebStorage.HasWebStorage)
            {
                var webStorage = hasWebStorage.WebStorage;
                webStorage.LocalStorage.RemoveItem("access_token");
                webStorage.LocalStorage.RemoveItem("refresh_token");
                webStorage.SessionStorage.RemoveItem("access_token");
                webStorage.SessionStorage.RemoveItem("refresh_token");
            }
            else
            {
                IJavaScriptExecutor jsExec = (IJavaScriptExecutor)driver;
                jsExec.ExecuteScript($"localStorage.removeItem('access_token');");
                jsExec.ExecuteScript($"localStorage.removeItem('refresh_token');");
                jsExec.ExecuteScript($"sessionStorage.removeItem('access_token');");
                jsExec.ExecuteScript($"sessionStorage.removeItem('refresh_token');");
            }
        }

        [AssemblyCleanup()]
        public static void ClassCleanup()
        {
            driver.Quit();
        }
    }
}
