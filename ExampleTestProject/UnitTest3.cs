using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.IO;
using SharpSauce;
using System.Threading.Tasks;

namespace ExampleTestProject
{

        [TestClass]
        public class UnitTest3
        {
            //UserName and AccessKey must correspond to Sauce Labs Username and Acess Key
            private const string UserName = "userName";
            private const string AccessKey = "accessKey";
            private const string baseURL = "https://www.google.com/";
            //timeoutInMinutes determines how many minutes must pass before timeout exception is thrown. For parallel testing only.
            private const int timeoutInMinutes = 5;


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
            public void ExampleTestRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-San.txt");
                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line), //Do not Change
                        TestName = "Test for Selenium in Google", //Can change
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault, //can Change, although not recommended
                        Timeout = 30, //Can change
                        BuildNumber = "10", //can change
                        Tags = new string[] { "Example", "Simple navigation", "unit test" }, //can change

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, TutorialDemoTestCase);
                   // Assert.IsTrue(results);
                }

            }

            private bool TutorialDemoTestCase(SauceLabsDriver driver)
            {
                //Insert your test script here
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl(baseURL + "/");
                driver.FindElement(By.Id("gbqfq")).Clear();
                driver.FindElement(By.Id("gbqfq")).SendKeys("Selenium");
                wait.Until(ExpectedConditions.ElementExists(By.LinkText("Selenium - Web Browser Automation")));
                driver.FindElement(By.LinkText("Selenium - Web Browser Automation")).Click();
                return true;

            }



    }
}
