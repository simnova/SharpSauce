using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SharpSauce;
using System.IO;
using System.Windows;

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
            var driver = LocalTest("firefox");
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
            
            driver.FindElement(By.CssSelector("input[type=text]")).Clear();
            driver.FindElement(By.CssSelector("input[type=text]")).SendKeys("Selenium");
            driver.FindElement(By.Id("gbqfb")).Click();
            //var results = driver.FindElement(By.LinkText("Selenium - Web Browser Automation"));
            //var results = driver.FindElement(By.CssSelector("a[href=http://docs.seleniumhq.org/]"));
            var results = driver.FindElement(By.XPath("//*[@id=\"rso\"]/li[1]/div/h3/a"));
            return results != null;
        }
    }
}
