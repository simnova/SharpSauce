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
        //UserName and AccessKey must correspond to Sauce Labs Username and Acess Key
        private const string UserName = "UserName";
        private const string AccessKey = "Access Key";

        public IWebDriver LocalTest(string browser)
        {
            IWebDriver driver;
            switch (browser)
            {
                    //The chrome driver requires that the driver be initialized with the path to the directory the chromedriver.exe is in
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
            var sauceLabs = new SauceLabs(UserName, AccessKey);
            var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig{
                BrowserVersion = SauceLabs.BrowserVersions.ie9win7,
                TestName = "Google search for Selenium",
                ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                Timeout = 40
            });
             
            var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
            Assert.IsTrue(results);

        }

        private bool RunTestCase(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://www.google.com/");
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            driver.FindElement(By.CssSelector("input[type=text]")).Clear();
            driver.FindElement(By.CssSelector("input[type=text]")).SendKeys("Selenium");
            driver.FindElement(By.Id("gbqfb")).Click();
            var results = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Selenium - Web Browser Automation")));
            return results != null;
        }
    }
}
