using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Configuration;

namespace DSoft.AgileSprinter.Unit.Tests.features
{
    [Binding]
    public class TestWebLinksSteps
    {
        private static IWebDriver driver;
        private static Dictionary<string, string> ResultingUrlMap;
        private static string url;

        private const string URL_MVC = "Add a Controller and View";
        private const string URL_USER_SECRETS = "Manage User Secrets using Secret Manager.";
        private const string URL_LOGGING = "Use logging to log a message.";
        private const string URL_RAZOR_PAGES = "Razor Pages";
        private const string URL_RICK_ANDERSON = "Rick Anderson";
        private const string URL_IIS_EXPRESS = "Port to EF Core";

        [BeforeFeature]
        public static void InitializeData()
        {
            driver = new ChromeDriver();
            ResultingUrlMap = new Dictionary<string, string>();
            ResultingUrlMap.Add(URL_MVC, "https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-2.1&tabs=aspnetcore2x");
            ResultingUrlMap.Add(URL_USER_SECRETS, "https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=windows");
            ResultingUrlMap.Add(URL_LOGGING, "https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1&tabs=aspnetcore2x");
            ResultingUrlMap.Add(URL_RAZOR_PAGES, "https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start?view=aspnetcore-2.1");
            ResultingUrlMap.Add(URL_RICK_ANDERSON, "https://twitter.com/RickAndMSFT");
            ResultingUrlMap.Add(URL_IIS_EXPRESS, "https://docs.microsoft.com/en-us/iis/extensions/introduction-to-iis-express/iis-express-overview");

        }

        [Given(@"The splash page is loaded")]
        public void GivenTheSplashPageIsLoaded()
        {
            url = ConfigurationManager.AppSettings["splash_url"];
            driver.Url = url;
        }
        
        [Given(@"The Entity Framework page is loaded")]
        public void GivenTheEntityFrameworkPageIsLoaded()
        {
            url = ConfigurationManager.AppSettings["entity_url"];
            driver.Url = url;
        }
        
        [When(@"user clicks ""(.*)""")]
        public void WhenUserClicks(string p0)
        {
            var element = By.LinkText(p0);
            driver.FindElement(element).Click();
        }
        
        [Then(@"The web app redirects to new ""(.*)""")]
        public void ThenTheWebAppRedirectsToNew(string p0)
        {
            string expected = ResultingUrlMap[p0];
            string actual = driver.Url;
            driver.Close();
            Assert.AreEqual(expected, actual, "Url should be");
        }
    }
}
