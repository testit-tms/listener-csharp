using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Html5;
using System;
using System.Threading;
using TestAutomationProject.PageObject.Pages;

namespace TestAutomationProject.Tests
{
    [TestClass]
    public class Configuration : BaseTestClass
    {
        [TestMethod]
        public void CreateConfiguration()
        {
            // Arrange
            var login = "Tester";
            var password = "Tester123Tester";
            var projectName = "Demo Project";

            // Act
            var countOfConfiguration = new AuthorizationPage(driver)
                .Login(login, password)
                .Search(projectName)
                .OpenProject()
                .OpenConfigurationPage()
                .Create()
                .Count;

            // Assert
            Assert.AreEqual(1, countOfConfiguration);
        }
    }
}
