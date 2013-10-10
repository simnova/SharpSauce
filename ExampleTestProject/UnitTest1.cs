using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SharpSauce;

namespace ExampleTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private const string UserName = "yourSeleniumUserName";
        private const string AccessKey = "yourSeleniumAccessKey";

        public IWebDriver LocalTest(string browser)
        {
            IWebDriver driver;
            switch (browser)
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "ie":
                default:
                    driver = new InternetExplorerDriver();
                    break;

            }
            return driver;
        }


        [TestMethod]
        public void TestFirefoxLocal()
        {
            var driver = LocalTest("firefox");
            Assert.IsTrue(RunTestCase(driver));
        }

        [TestMethod]
        public void TestChromelocal()
        {
            var driver = LocalTest("chrome");
            Assert.IsTrue(RunTestCase(driver));
        }

        [TestMethod]
        public void TestIeRemote()
        {
            var sauceLabs = new SauceLabs(UserName, AccessKey);
            var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig{
                BrowserVersion = SauceLabs.BrowserVersions.ie9win7,
                TestName = "Google search for Selenium",
                ScreenResolution = SauceLabs.ScreenResolutions.screen1024x768,
                Timeout = 40
            });
            var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
            Assert.IsTrue(results);
        }

        private bool RunTestCase(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://www.google.com/");
            driver.FindElement(By.CssSelector("input[type=text]")).Clear();
            driver.FindElement(By.CssSelector("input[type=text]")).SendKeys("Selenium");
            driver.FindElement(By.Id("gbqfb")).Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            var results = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Selenium - Web Browser Automation")));
           // var results = driver.FindElement(By.LinkText("Selenium - Web Browser Automation"));
            return results != null;
        }
    }
}
