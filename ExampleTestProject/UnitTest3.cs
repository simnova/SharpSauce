using System;
using System.Collections.Generic;
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
            private const string UserName = "username";
            private const string AccessKey = "access key";

            //Test setup values
            private const string baseURL = "https://www.google.com/"; //base url can be retrieved from selenium script
            private const string TextFile = "OS-Browser-Combo-San.txt"; //name of text file containing browser/os combinations to be tested
            private const bool mobile = false;

            //Test configuration values

            //_TestName: Sets session name of test ("on browser/os combo" will be added automatically). Should be text string.
            //_ScreenResolution: Sets screen resolution of test. Changing not recommended.
            //_Timeout: Sets command timeout. Should be integer
            //_BuildNumber: Sets build number. Should be text string
            //_Tags_seq: Sets tags for sequential test. Should be list of text strings. Can have as many tags as desired
            //_Tags_parall: Sets tags for parallel test. Should be list of text strings. Can have as many tags as desired

            private const string _TestName = "Test for Selenium in Google"; 
            private const SauceLabs.ScreenResolutions _ScreenResolution = SauceLabs.ScreenResolutions.screenDefault;
            private const int _Timeout = 30;
            private const string _BuildNumber = "11";
            private string[] _Tags_seq = new string[] { "Example", "Simple Navigation", "unit test"};
            private string[] _Tags_parall = new string[] { "parallel test", "Example", "extended timeout"};

            //timeoutInMinutes determines how many minutes must pass before timeout exception is thrown. For parallel testing only.
            private int timeoutInMinutes = 5;


            //Test Templates

            //Local Test Templates
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

             
            //Remote Test Sequential Template
            [TestMethod]
            public void ExampleTestRemote()
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

            //Remote Test Parallel Template
            [TestMethod]
            public void ExampleTestRemoteParallel()
            {

                string[] lines = System.IO.File.ReadAllLines(TextFile);
                List<string> failedCombos = new List<string>();

                Parallel.ForEach(lines, line =>
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line), //Do not change
                        TestName = _TestName,
                        ScreenResolution = _ScreenResolution,
                        Timeout = _Timeout,
                        BuildNumber = _BuildNumber,
                        Tags = _Tags_parall
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
                );

                if (failedCombos.Capacity > 0)
                {
                    string allFailedCombos = string.Join(", ", failedCombos.ToArray());
                    Assert.IsTrue(false, "Test failed these browser/OS combinations: " + allFailedCombos);
                }
            }


            //Test Script
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
