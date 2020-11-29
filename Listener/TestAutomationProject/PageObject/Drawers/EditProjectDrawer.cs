using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TestAutomationProject.PageObject.Pages;

namespace TestAutomationProject.PageObject.Drawers
{
    public class EditProjectDrawer : BasePageObject
    {
        private const string ProjectNameTag = "app-project-properties input";
        private const string ProjectDescriptionTag = "textarea";
        private const string CreateProjectButtonTag = "footer button[type='submit']";

        public EditProjectDrawer(IWebDriver driver) : base(driver) { }

        public ProjectsPage Create(string name, string description)
        {
            SendKeys(ProjectNameTag, name);
            SendKeys(ProjectDescriptionTag, description);
            Click(CreateProjectButtonTag);

            return new ProjectsPage(driver);
        }
    }
}
