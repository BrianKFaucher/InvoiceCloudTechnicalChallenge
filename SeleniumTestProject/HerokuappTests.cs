using Xunit;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace SeleniumTestProject
{
    public class HerokuappTests
    {
        [Fact]
        public void VerifyAddElementButtonAddsDesiredElementsToPage()
        {
            var pageUrl = "https://the-internet.herokuapp.com/add_remove_elements/";
            var pageTitle = "The Internet";
            var countDeleteButtons = 10;
            using var driver = CreateChromeDriver(Directory.GetCurrentDirectory());

            GivenAddRemoveElementsPage(driver, pageUrl, pageTitle);

            WhenAddElementButtonIsClickedNtimes(driver, countDeleteButtons);

            ThenPageContainsCorrespondingNumberOfElements(driver, countDeleteButtons);
        }

        private static void GivenAddRemoveElementsPage(IWebDriver driver, string pageUrl, string pageTitle)
        {
            driver.Navigate().GoToUrl(pageUrl);
            driver.Url.Should().Be(pageUrl);
            driver.Title.Should().Be(pageTitle);
        }

        private static void WhenAddElementButtonIsClickedNtimes(IWebDriver driver, int countDeleteButtons)
        {
            for (int i = 0; i < countDeleteButtons; i++)
            {
                driver.FindElement(By.XPath(@"//button[text()=""Add Element""]")).Click();
            }
        }

        private static void ThenPageContainsCorrespondingNumberOfElements(IWebDriver driver, int countDeleteButtons)
        {
            var allElements = driver.FindElements(By.XPath(@"//button[text()=""Delete""]"));
            allElements.Count.Should().Be(countDeleteButtons);
        }

        private static IWebDriver CreateChromeDriver(string driverDirectory)
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            return new ChromeDriver(driverDirectory, options);
        }
    }
}
