using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SharpSauce;

namespace ExampleTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private const string UserName = "yourSeleniumUserName";
        private const string AccessKey = "yourSeleniumAccessKey";
        private const int Timeout = 45;


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

        public SauceLabsDriver RemoteTest(SauceLabs.BrowserVersions version, string screenResolution, string testName)
        {
            var sauceLabs = new SauceLabs(UserName, AccessKey);
            var driver = sauceLabs.GetRemoteDriver(version, testName, screenResolution, Timeout);
            return driver;
        }

        [TestMethod]
        public void TestIeLocal()
        {
            var driver = LocalTest("ie");
            Assert.IsTrue(RunTestCase(driver));
        }

        [TestMethod]
        public void TestIeRemote()
        {
            var sauceLabs = new SauceLabs(UserName, AccessKey);
            var driver = RemoteTest(SauceLabs.BrowserVersions.ie9win7, "1024x768","Google search for Selenium");
            var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
            Assert.IsTrue(results);
        }

        private bool RunTestCase(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://www.google.com/");
            driver.FindElement(By.Id("gbqfq")).Clear();
            driver.FindElement(By.Id("gbqfq")).SendKeys("Selenium");
            driver.FindElement(By.Id("gbqfb")).Click();
            var results = driver.FindElement(By.LinkText("Selenium - Web Browser Automation"));

            return results != null;
        }
    }
}
