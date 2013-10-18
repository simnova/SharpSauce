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
        private const string UserName = "matthew_walker";
        private const string AccessKey = "e2ac55e0-9e26-47f6-96ae-3d4ffcc37c47";

        public IWebDriver LocalTest(string browser)
        {
            IWebDriver driver;
            switch (browser)
            {
                case "chrome":
                    driver = new ChromeDriver(@"C:\Files\GitHub\SharpSauce\ExampleTestProject");
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
        public void TestIelocal()
        {
            var driver = LocalTest("ie");
            Assert.IsTrue(RunTestCase(driver));
        }

        [TestMethod]
        public void TestIeRemote()
        {
            //SauceLabsDriver driver;
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
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
           // var delay = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("input[autocomplete=\"off\"]")));
            driver.FindElement(By.CssSelector("input[type=text]")).Clear();
            driver.FindElement(By.CssSelector("input[type=text]")).SendKeys("Selenium");
            driver.FindElement(By.Id("gbqfb")).Click();
            var results = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Selenium - Web Browser Automation")));
           // var results = driver.FindElement(By.LinkText("Selenium - Web Browser Automation"));
            return results != null;
        }
    }
}
