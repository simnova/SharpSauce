using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
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
    public class mobileTest
    {
        //UserName and AccessKey must correspond to Sauce Labs Username and Acess Key
        private const string UserName = "matthew_walker";
        private const string AccessKey = "feab444a-ea79-46ae-a71c-689a0d7a8078";

        //Test setup values
        private const string baseURL = "http://localhost:4723/wd/hub"; //base url can be retrieved from selenium script
        private const string TextFile = "OS-Browser-Combo-Mobile.txt"; //name of text file containing browser/os combinations to be tested
        private const bool mobile = true;

        private const string _TestName = "Remote Mobile Test";
        private const SauceLabs.ScreenResolutions _ScreenResolution = SauceLabs.ScreenResolutions.screenDefault;
        private const int _Timeout = 30;
        private const string _BuildNumber = "17";
        private string[] _Tags_seq = new string[] { "Example", "mobile app", "appium" };
        private string[] _Tags_parall = new string[] { "parallel test", "Example", "extended timeout" };
        

        //timeoutInMinutes determines how many minutes must pass before timeout exception is thrown. For parallel testing only.
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
                    driver = new InternetExplorerDriver();
                    break;

            }
            return driver;
        }

        [TestMethod]
        public void ExampleAppRemote()
        {
            string[] lines = System.IO.File.ReadAllLines(TextFile);
            List<string> failedCombos = new List<string>();
            timeoutInMinutes = 1;

            foreach (string line in lines)
            {
                var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                {
                    BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line), //Do not change
                    TestName = _TestName,
                    ScreenResolution = _ScreenResolution,
                    Timeout = _Timeout,
                    BuildNumber = _BuildNumber,
                    Tags = _Tags_seq,

                }, mobile);
                var results = sauceLabs.RunRemoteTestCase(driver, TutorialDemoTestCase);

                try
                {
                    Assert.IsTrue(results);
                }

                catch (AssertFailedException)
                {
                    failedCombos.Add(line);
                }
            }

            if (failedCombos.Capacity > 0)
            {
                string allFailedCombos = string.Join(", ", failedCombos.ToArray());
                Assert.IsTrue(false, "Test failed these browser/OS combinations: " + allFailedCombos);
            }
        }

        [TestMethod]
        public void ExampleAppLocal()
        {
            timeoutInMinutes = 1;
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability("app-package", "com.ECFMG");
            caps.SetCapability("browserName", "");
            caps.SetCapability("device", "Android");
            caps.SetCapability("app-activity", "com.ECFMG.MyFirstMobileApp");
            caps.SetCapability("takeScreenshot", true);
            caps.SetCapability("version", "4.1.9");
            caps.SetCapability("device ID", "null");
            caps.SetCapability("app", @"C:\MobileApps\MyFirstMobileApp\out\production\android\android.apk");
            RemoteWebDriver driver = new RemoteWebDriver(new Uri(baseURL), caps);
            var results = TutorialDemoTestCase(driver);

        }

        //Test Script
        private bool TutorialDemoTestCase(RemoteWebDriver driver)
        {
            //Insert your test script here
            
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            return true;

        }

    }
}
