using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace DSoft.AgileSprinter.Unit.Tests.features
{
    [Binding]
    public class TestWebLinksSteps
    {
        private IWebDriver driver;
        private string[] arrUriList;
        private int testCount;

        private const int NUM_LINKS = 3;
        private const string URL_CONTROLLER_VIEW = "https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-2.1&tabs=aspnetcore2x";
        private const string URL_USER_SECRETS = "https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=windows";
        private const string URL_LOGGING = "https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1&tabs=aspnetcore2x";

        public TestWebLinksSteps()
        {
            testCount = 0;
            arrUriList = new string[NUM_LINKS];
            arrUriList[0] = URL_CONTROLLER_VIEW;
            arrUriList[1] = URL_USER_SECRETS;
            arrUriList[2] = URL_LOGGING;
        }

        [Given(@"The splash page is loaded")]
        public void GivenTheSplashPageIsLoaded()
        {
            driver = new ChromeDriver();
            driver.Url = "localhost:51096";
        }
        
        [When(@"user clicks the (.*) web link")]
        public void WhenUserClicksTheWebLink(string p0)
        {
            driver.FindElement(By.LinkText(p0)).Click();
        }
        
        [Then(@"The web app redirects to new uri")]
        public void ThenTheWebAppRedirectsToNewUri()
        {
            string newUrl = driver.Url;
            driver.Close();
            Assert.AreEqual(arrUriList[testCount], newUrl, "Url should be");
        }
    }
}
