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
    //UnitTest1: Basic Google search on local browsers and current browsers across all operating systems
    [TestClass]
    public class UnitTest1
    {
        //UserName and AccessKey must correspond to Sauce Labs Username and Acess Key
        private const string UserName = "username";
        private const string AccessKey = "access key";
        private const bool recordvideo = false;
        private const bool mobile = false;
        //timeoutInMinutes determines how many minutes must pass before timeout exception is thrown.
        private int timeoutInMinutes = 5;
 

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
                    driver = new InternetExplorerDriver(@"C:\Files\GitHub\SharpSauce\ExampleTestProject");
                    break;

            }
            return driver;
        }


        [TestMethod]
        public void parallelTestChromeFoxLocal()
        {
            IWebDriver driver;
            Parallel.For(0, 2, i =>
                {
                    if (i == 0)
                    {
                        driver = LocalTest("chrome");
                    }
                    else
                    {
                        driver = LocalTest("firefox");
                    }
                    Assert.IsTrue(RunloginTestCase(driver));
                }
                
            );
        }

        [TestMethod]
        public void serialTestChromFoxLocal()
        {
            IWebDriver driver;
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    driver = LocalTest("chrome");
                }
                else
                {
                    driver = LocalTest("firefox");
                }
                Assert.IsTrue(RunloginTestCase(driver));
            }
        }

        [TestMethod]
        public void TestFirefoxLocal()
        {
            var driver = LocalTest("firefox");
            Assert.IsTrue(RunTempTestCase(driver));
        }

        [TestMethod]
        public void TestChromelocal()
        {
            var driver = LocalTest("chrome");
            Assert.IsTrue(RunTempTestCase(driver));
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
            timeoutInMinutes = 1;
            var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
            var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig{
                BrowserVersion = SauceLabs.BrowserVersions.ie10win8,
                TestName = "Google search for Selenium",
                ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                Timeout = 40
            }, mobile);
             
            var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
            Assert.IsTrue(results);

        }

        [TestMethod]
        public void TestCustomData()
        {
            string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
            timeoutInMinutes = 1;

            foreach (string line in lines)
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                    TestName = "Unit Test CustomData",
                    ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                    Timeout = 30,
                    BuildNumber = "4",
                    Tags = new string[] { "custom data", "screenshots", "video" },
                    CustomData = new SauceLabs.customData{Staging = false, Release = "1.0" }

                }, mobile);
                string jobID = driver.GetExecutionId();
                var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
                var ending = new SauceRest(UserName, AccessKey);
                var confirmation = ending.RetrieveResults("jobs/" + jobID);
                Assert.AreEqual("\"custom-data\": {\"Release\": \"1.0\", \"Commit\": null, \"Staging\": false, \"ExecutionNumber\": 0, \"Server\": null}", 
                    confirmation.Substring(145, 105));
            }
        }


        [TestMethod]
        public void TestTags()
        {
            string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
            timeoutInMinutes = 1;

            foreach (string line in lines)
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                    TestName = "Unit Test Tags",
                    ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                    Timeout = 30,
                    BuildNumber = "5",
                    Tags = new string[] { "Tag test", "Retrieve Results" },
                    RecordVideo = true,
                    RecordScreenshots = true,

                }, mobile);
                string jobID = driver.GetExecutionId();
                var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
                var ending = new SauceRest(UserName, AccessKey);
                var confirmation = ending.RetrieveResults("jobs/" + jobID);
                Assert.AreEqual("\"tags\": [\"Tag test\", \"Retrieve Results\"]", confirmation.Substring(427, 40));
            }
        }

        [TestMethod]
        public void TestAllDesktopBrowsers()
        {

            string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Desktop.txt");
            timeoutInMinutes = 1;

            foreach (string line in lines)
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                    TestName = "Google search for Selenium",
                    ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                    Timeout = 40,
                    BuildNumber = "2",
                    Tags = new string[] { "Google", "Selenium", "All Desktop OS/browsers" }
                }, mobile);
                var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
                Assert.IsTrue(results);

            }
        }

        [TestMethod]
        public void TestSmallDesktopBrowsersParallel()
        {

            string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-small.txt");

            Parallel.ForEach(lines, line =>
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                    TestName = "Google search for Selenium",
                    ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                    Timeout = 40,
                    BuildNumber = "6",
                    Tags = new string[] { "parallel test", "compressed list", "extended timeout" }
                }, mobile);

                var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
                Assert.IsTrue(results);

            }
            );
        }

        [TestMethod]
        public void TestSmallDesktopBrowsersSequential()
        {

            string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Small.txt");
            timeoutInMinutes = 1;

            foreach(string line in lines)
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                    TestName = "Google search for Selenium",
                    ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                    Timeout = 40,
                    BuildNumber = "6",
                    Tags = new string[] {"sequential test", "compressed list", "extended timeout" }
                }, mobile);

                var results = sauceLabs.RunRemoteTestCase(driver, RunTestCase);
                Assert.IsTrue(results);

            }

        }

        [TestMethod]
        public void TestBuildNumber()
        {
            string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
            timeoutInMinutes = 1;

            foreach (string line in lines)
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                    TestName = "Unit Test BuildNumber",
                    ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                    Timeout = 30,
                    BuildNumber = "5",
                    Tags = new string[] { "Build Number test" },

                }, mobile);
                string jobID = driver.GetExecutionId();
                var results = sauceLabs.RunRemoteTestCase(driver, RunloginTestCase);
                var ending = new SauceRest(UserName, AccessKey);
                var confirmation = ending.RetrieveResults("jobs/" + jobID);
                Assert.AreEqual("\"build\": \"5\"", confirmation.Substring(356, 12));

            }
        }

        [TestMethod]
        public void TestpersonalRemote()
        {
            string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-San.txt");
            timeoutInMinutes = 1;

            foreach (string line in lines)
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                    TestName = "test for jsfiddle.net",
                    ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                    Timeout = 30,
                    BuildNumber = "7",
                    Tags = new string[] { "jsfiddle", "edit/confirm personal info", "safari" },

                }, mobile);
                var results = sauceLabs.RunRemoteTestCase(driver, RuntextEntryTestCase);
                Assert.IsTrue(results);
            }

        }

        [TestMethod]
        public void TestLoginFirefoxLocal()
        {
            var driver = new FirefoxDriver();
            login(driver);
            var results = driver.FindElement(By.CssSelector("input[value=First]"));
            Assert.IsTrue(results != null);
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
                driver.FindElement(By.CssSelector("input[type=submit]")).Click();
            }
            else
            {
                driver.FindElement(By.Id("gbqfb")).Click();
            }
             
            var results = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Selenium - Web Browser Automation")));
            return results != null;
        }

        private bool RunloginTestCase(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            driver = login(driver);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[value=First]")));
            driver.FindElement(By.CssSelector("input[value=First]")).Click();
            driver.FindElement(By.Name("findFlights")).Click();
            var results = driver.Title.Equals("Select a Flight: Mercury Tours");
            return results;
        }
        private bool RunTempTestCase(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            driver.Navigate().GoToUrl("http://jsfiddle.net/simnova/GnSU9/1/embedded/result/");
            driver.SwitchTo().Frame(0);
            //wait.Until(ExpectedConditions.ElementExists(By.Id("personalInfoPhone")));
            driver.FindElement(By.Id("personalInfoPhone")).Clear();
            driver.FindElement(By.Id("personalInfoPhone")).SendKeys("8015555555p");
            //var results = driver.FindElement(By.Id("personalInfoPhone")).GetAttribute("value");
            driver.FindElement(By.Id("personalInfoEmail")).Clear();
            driver.FindElement(By.Id("personalInfoEmail")).SendKeys("fakeEmailnotreal.com");
            driver.FindElement(By.Id("personalInfoConfirmButton")).Click();
            var results = driver.FindElement(By.Id("personalInfoPhone")).GetAttribute("value");
            return results == "8015555555p";
        }

        private bool RuntextEntryTestCase(SauceLabsDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            driver.Navigate().GoToUrl("http://jsfiddle.net/simnova/GnSU9/1/embedded/result/");
            driver.SwitchTo().Frame(0);
            driver.loginByID("8015555555", "fakeEmail@notreal.com", "personalInfoPhone", "personalInfoEmail");
            var results = driver.getElementValueByID("personalInfoPhone");
            return results == "";
        }

    }

}
