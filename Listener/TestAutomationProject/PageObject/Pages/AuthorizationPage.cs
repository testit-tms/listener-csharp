using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestAutomationProject.PageObject.Pages
{
    public class AuthorizationPage : BasePageObject
    {
        private const string LoginInputTag = "#username input";
        private const string PasswordInputTag = "#password input";
        private const string LoginButtonTag = ".auth-page__login-form button";
        private const string ErrorMessageTag = ".helper-text";

        public AuthorizationPage(IWebDriver driver) : base(driver) { }

        public ProjectsPage Login(string login, string password)
        {
            TryLogin(login, password);

            return new ProjectsPage(driver);
        }

        public AuthorizationPage TryLogin(string login, string password)
        {
            SendKeys(LoginInputTag, login);
            SendKeys(PasswordInputTag, password);
            Click(LoginButtonTag);

            Wait(5000);

            return this;
        }

        public AuthorizationPage ClickOnLoginButton()
        {
            Click(LoginButtonTag);

            return this;
        }

        public bool LoginButtonIsDisable()
        {
            return GetWebElement(LoginButtonTag).Displayed;
        }

        public string GetErrorMessage()
        {
            return GetWebElement(ErrorMessageTag).Text;
        }
    }
}
