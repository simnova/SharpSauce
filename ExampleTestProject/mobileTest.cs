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
        private const string UserName = "username";
        private const string AccessKey = "access key";

        //Test setup values
        private const string baseURL = "http://localhost:4723/wd/hub"; //base url can be retrieved from selenium script
        private const string TextFile = "OS-Browser-Combo-Mobile.txt"; //name of text file containing browser/os combinations to be tested
        private const bool mobile = true; //Boolean value to use appium for mobile testing

        private const string _TestName = "Remote Mobile Test Alert";
        private const SauceLabs.ScreenResolutions _ScreenResolution = SauceLabs.ScreenResolutions.screenDefault;
        private const int _Timeout = 30;
        private const string _BuildNumber = "20";
        private string[] _Tags_seq = new string[] { "mobile app", "Android 4.2 & 4.3", "Obese extended test"};
        private string[] _Tags_parall = new string[] { "parallel test", "Example", "extended timeout" };

        //The following values should only be included if a mobile application is being tested
        private const string _App_package = "com.ECFMG";
        private const string _Device = "Selendroid";
        private const string _App_activity = ".MyFirstMobileApp";
        private const string _Version = "4.3";
        private const string _DeviceType = "phone";
        private const string _App = "sauce-storage:android.zip";

        //timeoutInMinutes determines how many minutes must pass before timeout exception is thrown. For parallel testing only.
        private int timeoutInMinutes = 5;


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
                    AppPackage = _App_package,
                    Device = _Device,
                    AppActivity = _App_activity,
                    Version = _Version,
                    DeviceType = _DeviceType,
                    App = _App,
                    
                }, mobile);
                var results = sauceLabs.RunRemoteTestCase(driver, RemoteAppTestCase);

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
        public void UnderweightAppRemote()
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
                    AppPackage = _App_package,
                    Device = _Device,
                    AppActivity = _App_activity,
                    Version = _Version,
                    DeviceType = _DeviceType,
                    App = _App,

                }, mobile);
                var results = sauceLabs.RunRemoteTestCase(driver, RemoteAppUnderweight);

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
        public void NormalAppRemote()
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
                    AppPackage = _App_package,
                    Device = _Device,
                    AppActivity = _App_activity,
                    Version = _Version,
                    DeviceType = _DeviceType,
                    App = _App,

                }, mobile);
                var results = sauceLabs.RunRemoteTestCase(driver, RemoteAppNormal);

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
        public void OverweightAppRemote()
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
                    AppPackage = _App_package,
                    Device = _Device,
                    AppActivity = _App_activity,
                    Version = _Version,
                    DeviceType = _DeviceType,
                    App = _App,

                }, mobile);
                var results = sauceLabs.RunRemoteTestCase(driver, RemoteAppOverweight);

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
        public void ObeseAppRemote()
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
                    AppPackage = _App_package,
                    Device = _Device,
                    AppActivity = _App_activity,
                    Version = _Version,
                    DeviceType = _DeviceType,
                    App = _App,

                }, mobile);
                var results = sauceLabs.RunRemoteTestCase(driver, RemoteAppObese);

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
        public void AppRemoteAlert()
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
                    AppPackage = _App_package,
                    Device = _Device,
                    AppActivity = _App_activity,
                    Version = _Version,
                    DeviceType = _DeviceType,
                    App = _App,

                }, mobile);
                var results = sauceLabs.RunRemoteTestCase(driver, RemoteAppAlertTest);

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
            var results = LocalAppAlertTest(driver);

        }

        //Test Script
        private bool RemoteAppTestCase(SauceLabsDriver driver)
        {
            string weight_kg = "68.0";
            string height_cm = "172.7";
            string weight_lb = "150.0";
            string height_in = "68.0";
            string expectedResult = "NORMAL";
            //Insert your test script here
            
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            driver.SwitchTo().Window("WEBVIEW");
            driver.enterValueByID("weight_in", weight_kg);
            driver.enterValueByID("height_in", height_cm);
            driver.FindElement(By.Id("submit")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("classificationValue")));
            string output_metric = driver.FindElement(By.Id("classificationValue")).Text;
            driver.SelectDropDownValueByName("conversion", "Pounds/Inches");
            driver.enterValueByID("weight_in", weight_lb);
            driver.enterValueByID("height_in", height_in);
            driver.FindElement(By.Id("submit")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("classificationValue")));
            string output_standard = driver.FindElement(By.Id("classificationValue")).Text;
            bool results = (output_standard == output_metric) && (output_metric == expectedResult);
            return results;
        }

        private bool RemoteAppAlertTest(SauceLabsDriver driver)
        {
            string weight_kg = "68.Error";
            string height_cm = "172.7";
            string weight_correct = "68.0";
            string expectedAlert = "Entered weight is not a valid number.";
            string expectedClassification = "NORMAL";
            string expectedResult = "22.8";
            //bool alert = false;
            bool alertCorrect = false;
            bool testSuccessful = false;
            //Insert your test script here

            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            driver.SwitchTo().Window("WEBVIEW");
            driver.enterValueByID("weight_in", weight_kg);
            driver.enterValueByID("height_in", height_cm);
            driver.FindElement(By.Id("submit")).Click();
            var alert_0 = driver.WaitGetAlert(10);
            alertCorrect = alert_0.Text == expectedAlert;
            alert_0.Accept();
            driver.enterValueByID("weight_in", weight_correct);
            driver.FindElement(By.Id("submit")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("classificationValue")));
            string result_metric = driver.FindElementById("resultValue").Text;
            string output_metric = driver.FindElement(By.Id("classificationValue")).Text;
            testSuccessful = (result_metric == expectedResult) && (output_metric == expectedClassification);
            return alertCorrect && testSuccessful;

        }

        private bool LocalAppTestCase(RemoteWebDriver driver)
        {
            string weight_kg = "68.0";
            string height_cm = "172.7";
            string weight_lb = "150.0";
            string height_in = "68.0";
            string expectedResult = "You Are Classified As: NORMAL";
            //Insert your test script here

            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));    
            System.Threading.Thread.Sleep(30000);
            driver.SwitchTo().Window("WEBVIEW");
            wait.Until(ExpectedConditions.ElementExists(By.Id("resultText")));
            driver.FindElement(By.Id("weight_in")).Clear();
            driver.FindElement(By.Id("weight_in")).SendKeys(weight_kg);
            driver.FindElement(By.Id("height_in")).Clear();
            driver.FindElement(By.Id("height_in")).SendKeys(height_cm);
            driver.FindElement(By.Id("submit")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("classificationValue")));
            string output_metric = driver.FindElement(By.Id("classificationValue")).Text;
            IWebElement dropDownBox = driver.FindElement(By.Name("conversion"));
            SelectElement clickThis = new SelectElement(dropDownBox);
            clickThis.SelectByText("Pounds/Inches");
            driver.FindElement(By.Id("weight_in")).Clear();
            driver.FindElement(By.Id("weight_in")).SendKeys(weight_lb);
            driver.FindElement(By.Id("height_in")).Clear();
            driver.FindElement(By.Id("height_in")).SendKeys(height_in);
            driver.FindElement(By.Id("submit")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("classificationValue")));
            string output_standard = driver.FindElement(By.Id("classificationValue")).Text;
            bool results = (output_metric == output_standard) && (output_metric == expectedResult);
            return results;
        }

        private bool LocalAppAlertTest(RemoteWebDriver driver)
        {
            string weight_kg = "68.Error";
            string height_cm = "173";
            string expectedAlert = "Entered weight is not a valid number";
            bool alertCorrect;

            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            System.Threading.Thread.Sleep(30000);
            driver.SwitchTo().Window("WEBVIEW");
            wait.Until(ExpectedConditions.ElementExists(By.Id("resultText")));
            driver.FindElement(By.Id("weight_in")).Clear();
            driver.FindElement(By.Id("weight_in")).SendKeys(weight_kg);
            driver.FindElement(By.Id("height_in")).Clear();
            driver.FindElement(By.Id("height_in")).SendKeys(height_cm);
            driver.FindElement(By.Id("submit")).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alertCorrect = alert.Text == expectedAlert;
            return alertCorrect;

        }
        
        private bool RemoteAppUnderweight(SauceLabsDriver driver)
        {
            string weight_kg = "31.3";
            string height_cm = "165";
            string actualResultMetric;
            string actualCLassM;
            string weight_lb = "69.0";
            string height_in = "65";
            string actualResultStandard;
            string actualClassS;
            string expectedResult = "11.5";
            string expectedClass = "UNDERWEIGHT";
            bool resultCorrect = false;
            bool resultsEqual = false;
            bool classCorrect = false;
            bool classesEqual = false;

            driver.SwitchTo().Window("WEBVIEW");
            driver.WaitUntilElementVisibleByID("resultText");
            driver.enterValueByID("weight_in", weight_kg);
            driver.enterValueByID("height_in", height_cm);
            driver.FindElementById("submit").Click();
            driver.WaitUntilElementVisibleByID("resultValue");
            actualResultMetric = driver.FindElementById("resultValue").Text;
            actualCLassM = driver.FindElementById("classificationValue").Text;
            driver.SelectDropDownValueByName("conversion", "Pounds/Inches");
            driver.enterValueByID("weight_in", weight_lb);
            driver.enterValueByID("height_in", height_in);
            driver.FindElementById("submit").Click();
            actualResultStandard = driver.FindElementById("resultValue").Text;
            actualClassS = driver.FindElementById("classificationValue").Text;
            resultsEqual = actualResultMetric == actualResultStandard;
            resultCorrect = actualResultMetric == expectedResult;
            classesEqual = actualCLassM == actualClassS;
            classCorrect = actualCLassM == expectedClass;
            return resultCorrect && resultsEqual && classCorrect && classesEqual;
            
        }

        private bool RemoteAppNormal(SauceLabsDriver driver)
        {
            string weight_kg = "65";
            string height_cm = "170.2";
            string actualResultMetric;
            string actualCLassM;
            string weight_lb = "143.3";
            string height_in = "67";
            string actualResultStandard;
            string actualClassS;
            string expectedResult = "22.4";
            string expectedClass = "NORMAL";
            bool resultCorrect = false;
            bool resultsEqual = false;
            bool classCorrect = false;
            bool classesEqual = false;

            driver.SwitchTo().Window("WEBVIEW");
            driver.WaitUntilElementVisibleByID("resultText");
            driver.enterValueByID("weight_in", weight_kg);
            driver.enterValueByID("height_in", height_cm);
            driver.FindElementById("submit").Click();
            driver.WaitUntilElementVisibleByID("resultValue");
            actualResultMetric = driver.FindElementById("resultValue").Text;
            actualCLassM = driver.FindElementById("classificationValue").Text;
            driver.SelectDropDownValueByName("conversion", "Pounds/Inches");
            driver.enterValueByID("weight_in", weight_lb);
            driver.enterValueByID("height_in", height_in);
            driver.FindElementById("submit").Click();
            actualResultStandard = driver.FindElementById("resultValue").Text;
            actualClassS = driver.FindElementById("classificationValue").Text;
            resultsEqual = actualResultMetric == actualResultStandard;
            resultCorrect = actualResultMetric == expectedResult;
            classesEqual = actualCLassM == actualClassS;
            classCorrect = actualCLassM == expectedClass;
            return resultCorrect && resultsEqual && classCorrect && classesEqual;

        }

        private bool RemoteAppOverweight(SauceLabsDriver driver)
        {
            string weight_kg = "78.6";
            string height_cm = "172";
            string actualResultMetric;
            string actualCLassM;
            string weight_lb = "173.3";
            string height_in = "67.7";
            string actualResultStandard;
            string actualClassS;
            string expectedResult = "26.6";
            string expectedClass = "OVERWEIGHT";
            bool resultCorrect = false;
            bool resultsEqual = false;
            bool classCorrect = false;
            bool classesEqual = false;

            driver.SwitchTo().Window("WEBVIEW");
            driver.WaitUntilElementVisibleByID("resultText");
            driver.enterValueByID("weight_in", weight_kg);
            driver.enterValueByID("height_in", height_cm);
            driver.FindElementById("submit").Click();
            driver.WaitUntilElementVisibleByID("resultValue");
            actualResultMetric = driver.FindElementById("resultValue").Text;
            actualCLassM = driver.FindElementById("classificationValue").Text;
            driver.SelectDropDownValueByName("conversion", "Pounds/Inches");
            driver.enterValueByID("weight_in", weight_lb);
            driver.enterValueByID("height_in", height_in);
            driver.FindElementById("submit").Click();
            actualResultStandard = driver.FindElementById("resultValue").Text;
            actualClassS = driver.FindElementById("classificationValue").Text;
            resultsEqual = actualResultMetric == actualResultStandard;
            resultCorrect = actualResultMetric == expectedResult;
            classesEqual = actualCLassM == actualClassS;
            classCorrect = actualCLassM == expectedClass;
            return resultCorrect && resultsEqual && classCorrect && classesEqual;

        }

        private bool RemoteAppObese(SauceLabsDriver driver)
        {
            string weight_kg = "100";
            string height_cm = "155";
            string actualResultMetric;
            string actualCLassM;
            string weight_lb = "220.4";
            string height_in = "61.0";
            string actualResultStandard;
            string actualClassS;
            string detour_weight = "61.0";
            string detour_height = "220.4";
            string expectedResult = "41.6";
            string expectedClass = "OBESE";
            bool resultCorrect = false;
            bool resultsEqual = false;
            bool classCorrect = false;
            bool classesEqual = false;

            driver.SwitchTo().Window("WEBVIEW");
            driver.WaitUntilElementVisibleByID("resultText");
            driver.enterValueByID("weight_in", weight_kg);
            driver.enterValueByID("height_in", height_cm);
            driver.FindElementById("submit").Click();
            driver.WaitUntilElementVisibleByID("resultValue");
            actualResultMetric = driver.FindElementById("resultValue").Text;
            actualCLassM = driver.FindElementById("classificationValue").Text;
            driver.SelectDropDownValueByName("conversion", "Pounds/Inches");
            driver.enterValueByID("weight_in", detour_weight);
            driver.enterValueByID("height_in", detour_height);
            driver.FindElementById("submit").Click();
            driver.WaitUntilElementVisibleByID("resultValue");
            driver.enterValueByID("weight_in", weight_lb);
            driver.enterValueByID("height_in", height_in);
            driver.FindElementById("submit").Click();
            actualResultStandard = driver.FindElementById("resultValue").Text;
            actualClassS = driver.FindElementById("classificationValue").Text;
            resultsEqual = actualResultMetric == actualResultStandard;
            resultCorrect = actualResultMetric == expectedResult;
            classesEqual = actualCLassM == actualClassS;
            classCorrect = actualCLassM == expectedClass;
            return resultCorrect && resultsEqual && classCorrect && classesEqual;

        }
        
    }
}
