using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TestAutomationProject.PageObject.Drawers;

namespace TestAutomationProject.PageObject.Pages
{
    public class ProjectsPage : BasePageObject
    {
        private const string TitleTag = "app-projects h1";
        private const string CreateProjectDrawerTag = ".page-header-row button";
        private const string SearchFieldTag = ".search-field input";
        private const string ProjectCardTag = "app-project-tile";

        public ProjectsPage(IWebDriver driver) : base(driver) { }

        public string GetTitle()
        {
            return GetWebElement(TitleTag).Text;
        }

        public EditProjectDrawer OpenCreateProjectDrawer()
        {
            Click(CreateProjectDrawerTag);
            Wait(AnimationTime);

            return new EditProjectDrawer(driver);
        }

        public ProjectPage OpenProject()
        {
            Click(ProjectCardTag);
            return new ProjectPage(driver);
        }

        public ProjectsPage Search(string name)
        {
            SendKeys(SearchFieldTag, name);

            return new ProjectsPage(driver);
        }

        public int CountOfProject()
        {
            Wait(AnimationTime);

            return GetWebElements(ProjectCardTag).Count;
        }
    }
}
