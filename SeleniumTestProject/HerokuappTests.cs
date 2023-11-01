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
            var countDeleteButtons = 10;

            using var driver = CreateChromeDriver(Directory.GetCurrentDirectory(), true);
            driver.Navigate().GoToUrl(pageUrl);
            driver.Title.Should().Be("The Internet");
            driver.Url.Should().Be(pageUrl);

            for(int i = 0; i < countDeleteButtons; i++)
            {
                driver.FindElement(By.XPath(@"//button[text()=""Add Element""]")).Click();
            }

            var allElements = driver.FindElements(By.XPath(@"//button[text()=""Delete""]"));
            allElements.Count.Should().Be(countDeleteButtons);
        }

        private static IWebDriver CreateChromeDriver(string driverDirectory, bool isDebuggerAttached)
        {
            var options = new ChromeOptions();

            if (!isDebuggerAttached)
            {
                options.AddArgument("--headless");
            }

            return new ChromeDriver(driverDirectory, options);
        }
    }
}
