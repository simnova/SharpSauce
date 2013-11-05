using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.IO;
using SharpSauce;

namespace ExampleTestProject
{
    //UnitTest1: Basic Google search on local browsers and current browsers across all operating systems
    [TestClass]
    public class UnitTest1
    {
        //UserName and AccessKey must correspond to Sauce Labs Username and Acess Key
        private const string UserName = "username";
        private const string AccessKey = "access key";

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
                BrowserVersion = SauceLabs.BrowserVersions.ie10win8,
                TestName = "Google search for Selenium",
                ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                Timeout = 40
            });
             
            var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
            Assert.IsTrue(results);

        }

        [TestMethod]
        public void TestMobile()
        {
            string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");

            foreach (string line in lines)
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                    TestName = "Simple navigation using login",
                    ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                    ScreenOrientation = OpenQA.Selenium.ScreenOrientation.Landscape,
                    Timeout = 30,
                    BuildNumber = "3",
                    Tags = new string[] { "Video", "no Capture HTML", "Test" },
                    RecordVideo = true,
                    CaptureHTML = false,
                    CustomData = new SauceLabs.customData
                    {
                        Release = "0.5",
                        Staging = true
                    }
                });

                var results = sauceLabs.RunRemoteTestCase(driver, RunAdvancedTestCase);
                Assert.IsTrue(results);

            }
        }

        [TestMethod]
        public void TestRemote()
        {

            string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Desktop.txt");

            foreach (string line in lines)
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                    TestName = "Google search for Selenium",
                    ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                    Timeout = 40,
                    BuildNumber = "2",
                    Tags = new string[] { "Google", "Selenium" }
                });

                var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
                Assert.IsTrue(results);

            }
        }

        private IWebDriver login(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://newtours.demoaut.com/");
            driver.FindElement(By.CssSelector("input[type=text]")).Clear();
            driver.FindElement(By.CssSelector("input[type=text]")).SendKeys("tutorial");
            driver.FindElement(By.CssSelector("input[type=password]")).Clear();
            driver.FindElement(By.CssSelector("input[type=password]")).SendKeys("tutorial");
            driver.FindElement(By.Name("login")).Click();
            return driver;

        }
        private bool RunTestCase(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://www.google.com/");
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            driver.FindElement(By.Name("q")).Clear();
            driver.FindElement(By.Name("q")).SendKeys("Selenium");
            if(driver.FindElements(By.Id("gbqfb")).Count < 1)
            {
                driver.FindElement(By.CssSelector("button[type=submit]")).Click();
            }
            else
            {
                driver.FindElement(By.Id("gbqfb")).Click();
            }
             
            var results = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Selenium - Web Browser Automation")));
            return results != null;
        }

        private bool RunAdvancedTestCase(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            driver = login(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[value=First]")));
            driver.FindElement(By.CssSelector("input[value=First]")).Click();
            driver.FindElement(By.Name("findFlights")).Click();
            var results = driver.Title.Equals("Select a Flight: Mercury Tours");
            return results;
        }


    }

}
