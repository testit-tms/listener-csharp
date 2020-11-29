using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Html5;
using System.Threading;
using TestAutomationProject.PageObject.Pages;

namespace TestAutomationProject.Tests
{
    [TestClass]
    public class Authorization : BaseTestClass
    {
        [TestMethod]
        public void Login_CorrectСredentials_SuccessfulAuthorization()
        {
            // Arrange
            var login = "Tester";
            var password = "Tester123Tester";

            // Act
            var title = new AuthorizationPage(driver)
                .Login(login, password)
                .GetTitle();

            // Assert
            Assert.AreEqual(title, "Projects");
        }

        [TestMethod]
        public void Login_IncorrectСredentials_AuthorisationError()
        {
            // Arrange
            var password = "Tester";
            var login = "Tester123Tester";
            var expectedErrorMessage = "Wrong credentials";

            // Act
            var errorMassege = new AuthorizationPage(driver)
                .TryLogin(login, password)
                .GetErrorMessage();

            // Assert
            Assert.AreEqual(expectedErrorMessage, errorMassege);
        }

        [TestMethod]
        public void Login_WithoutСredentials_LoginButtonDisabled()
        {
            // Arrange

            // Act
            var isDisable = new AuthorizationPage(driver)
                .ClickOnLoginButton()
                .LoginButtonIsDisable();

            // Assert
            Assert.AreEqual(true, isDisable);
        }
    }
}
