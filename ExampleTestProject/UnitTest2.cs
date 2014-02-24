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
        public class UnitTest2
        {
            //UserName and AccessKey must correspond to Sauce Labs Username and Acess Key
            private const string UserName = "username";
            private const string AccessKey = "access key";
            private const string phone = "8015555555";
            private const string email = "fakeperson@notreal.com";
            private const string loginName = "tutorial";
            private const string loginPassword = "tutorial";
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
                        driver = new InternetExplorerDriver();
                        break;

                }
                return driver;
            }

            [TestMethod]
            public void TestloginByNameMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-San.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "Login by Name test for NewTours",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "NewTours", "login by name", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, LoginByNameTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestloginByIdMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-San.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "Login by ID test for jsfiddle",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "jsfiddle", "login by id", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, LoginByIDTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestGetValueByIDMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "get value by Id test for jsfiddle",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "jsfiddle", "get value by id", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, GetValueByIdTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestGetValueByNameMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "get value by name test for new tours",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "new tours demo", "get value by name", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, GetValueByNameTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestEnterValueByIdMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "enter value by id test for jsfiddle",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "jsfiddle", "enter value by id", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, EnterValueByIdTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestEnterValueByNameMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "enter value by name test for new tours",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "new tours demo", "enter value by name", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, EnterValueByNameTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestEnterValueByTagandAttributeMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "enter value by tag and attribute test for jsfiddle",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "jsfiddle", "enter value by tag and attribute", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, EnterValueByTagandAttributeTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestClickonLinkMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "click on link test for new tours",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "new tours demo", "click on link", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, NavigateByLinkTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestSelectDropDownByNameMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "select drop down option for new tours",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "new tours demo", "drop down by name", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, SelectDropDownByNameTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestSelectDropDownByAttributeMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "select drop down option for custom page",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "custom drop downs", "drop down by attribute", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, SelectDropDownByAttributeTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestSelectDropDownByIdMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "select drop down option for custom page",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "custom drop downs", "drop down by Id", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, SelectDropDownByIdTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestWaitUntilTimedMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-San.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "wait until time has passed for e.ggtimer",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 180,
                        BuildNumber = "9",
                        Tags = new string[] { "e.ggtimer", "wait until time", "unit test" },

                    });
                    var results = sauceLabs.RunRemoteTestCase(driver, WaitUntilTimeTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestWaitUntilElementVisiblebyIdMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-WaitElVis.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "wait until element appears on google.com",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 180,
                        BuildNumber = "9",
                        Tags = new string[] { "selenium search", "wait until element is visible", "unit test" },

                    });

                    var results = sauceLabs.RunRemoteTestCase(driver, WaitUntilElementVisiblebyIdTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestClickRadioButtonByIDMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-san.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "click a radio button",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "w3.org", "radio buttons", "unit test" },

                    });

                    var results = sauceLabs.RunRemoteTestCase(driver, ClickRadioButtonByIDTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestClickRadioButtonByNameMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-san.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "click a radio button on W3schools",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "w3schools.com", "radio buttons", "unit test" },

                    });

                    var results = sauceLabs.RunRemoteTestCase(driver, ClickRadioButtonByNameTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestClickCheckBoxByIDMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "click a check box",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "w3.org", "check box", "unit test" },

                    });

                    var results = sauceLabs.RunRemoteTestCase(driver, ClickCheckBoxByIDTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void PressEnterMethodRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "Press enter testing",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "google", "press enter", "unit test" },

                    });

                    var results = sauceLabs.RunRemoteTestCase(driver, PressEnterTestCase);
                    Assert.IsTrue(results);
                }

            }

            [TestMethod]
            public void TestIsAlertPresentRemote()
            {
                string[] lines = System.IO.File.ReadAllLines("OS-Browser-Combo-Indiv.txt");
                timeoutInMinutes = 1;

                foreach (string line in lines)
                {
                    var sauceLabs = new SauceLabs(UserName, AccessKey, timeoutInMinutes);
                    var driver = sauceLabs.GetRemoteDriver(new SauceLabs.SauceLabsConfig
                    {
                        BrowserVersion = (SauceLabs.BrowserVersions)Enum.Parse(typeof(SauceLabs.BrowserVersions), line),
                        TestName = "Test IsAlertPresent() method",
                        ScreenResolution = SauceLabs.ScreenResolutions.screenDefault,
                        Timeout = 30,
                        BuildNumber = "9",
                        Tags = new string[] { "small method", "alerts", "unit test" },

                    });

                    var results = sauceLabs.RunRemoteTestCase(driver, WaitUntilElementVisiblebyIdTestCase);
                    Assert.IsTrue(results);
                }

            }


            private bool RunLoginbyIdTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://jsfiddle.net/simnova/GnSU9/1/embedded/result/");
                driver.SwitchTo().Frame(0);
                driver.loginByID(phone, email, "personalInfoPhone", "personalInfoEmail");
                var results = driver.FindElement(By.Id("personalInfoPhone")).GetAttribute("value");
                return results == phone;
            }

            private bool GetValueByIdTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://jsfiddle.net/simnova/GnSU9/1/embedded/result/");
                driver.SwitchTo().Frame(0);
                driver.FindElement(By.Id("personalInfoPhone")).Clear();
                driver.FindElement(By.Id("personalInfoPhone")).SendKeys(phone);
                var afterSendKeys = driver.getElementValueByID("personalInfoPhone");
                var results = afterSendKeys == phone;
                return results;
            }

            private bool GetValueByNameTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://newtours.demoaut.com/");
                driver.FindElement(By.Name("userName")).Clear();
                driver.FindElement(By.Name("userName")).SendKeys(loginName);
                var afterSendKeys = driver.getElementValueByName("userName");
                var results = afterSendKeys == loginName;
                return results;
            }

            private bool EnterValueByIdTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://jsfiddle.net/simnova/GnSU9/1/embedded/result/");
                driver.SwitchTo().Frame(0);
                driver.enterValueByID("personalInfoPhone", phone);
                var afterEnterValue = driver.FindElement(By.Id("personalInfoPhone")).GetAttribute("value");
                var results = afterEnterValue == phone;
                return results;
            }

            private bool EnterValueByNameTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://newtours.demoaut.com/");
                driver.enterValueByName("userName", loginName);
                var afterEnterValue = driver.FindElement(By.Name("userName")).GetAttribute("value");
                var results = afterEnterValue == loginName;
                return results;
            }

            private bool EnterValueByTagandAttributeTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://jsfiddle.net/simnova/GnSU9/1/embedded/result/");
                driver.SwitchTo().Frame(0);
                driver.enterValueByAttribute("type", "tel", phone);
                var afterEnterValue = driver.FindElement(By.CssSelector("input[type=tel]")).GetAttribute("value");
                var results = afterEnterValue == phone;
                return results;
            }

            private bool NavigateByLinkTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://newtours.demoaut.com/");
                driver.ClickonLink("REGISTER");
                var expectedTitle = "Register: Mercury Tours";
                var results = expectedTitle == driver.Title;
                return results;
            }

            private bool SelectDropDownByNameTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://newtours.demoaut.com/mercuryregister.php?osCsid=617f5f0c72f025b410db3c61c46e2b4e");
                driver.SelectDropDownValueByName("country", "JAPAN");
                var actualValue = driver.FindElement(By.Name("country")).GetAttribute("value");
                var expectedValue = "100";
                var results = expectedValue == actualValue;
                return results;
            }

            private bool SelectDropDownByAttributeTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://www.w3schools.com/html/tryit.asp?filename=tryhtml_select2");
                driver.FindElement(By.Id("textareaCode")).Clear();
                string[] lines = System.IO.File.ReadAllLines("CustomDropDown.txt");
                foreach (string line in lines)
                {
                    driver.FindElement(By.Id("textareaCode")).SendKeys(line + Keys.Enter);
                }
                driver.FindElement(By.Id("submitBTN")).Click();
                driver.FindElement(By.CssSelector("input#submitBTN")).Click();
                driver.SwitchTo().Frame("iframeResult");
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("select[type=Awesome]")));
                driver.SelectDropDownValueByAttribute("type", "Amazing", "Dream of the Sky");
                var actualValue = driver.FindElement(By.CssSelector("select[type=Amazing]")).GetAttribute("value");
                var expectedValue = "6";
                var results = expectedValue == actualValue;
                return results;
            }

            private bool SelectDropDownByIdTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://www.w3schools.com/html/tryit.asp?filename=tryhtml_select2");
                driver.FindElement(By.Id("textareaCode")).Clear();
                string[] lines = System.IO.File.ReadAllLines("CustomDropDown.txt");
                foreach (string line in lines)
                {
                    driver.FindElement(By.Id("textareaCode")).SendKeys(line + Keys.Enter);
                }
                driver.FindElement(By.Id("submitBTN")).Click();
                driver.FindElement(By.CssSelector("input#submitBTN")).Click();
                driver.SwitchTo().Frame("iframeResult");
                wait.Until(ExpectedConditions.ElementExists(By.Id("Album4")));
                driver.SelectDropDownValueById("Album3", "Dream of the Sky");
                var actualValue = driver.FindElement(By.Id("Album3")).GetAttribute("value");
                var expectedValue = "6";
                var results = expectedValue == actualValue;
                return results;
            }

            private bool WaitUntilTimeTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://e.ggtimer.com/30");
                driver.WaitUntilTime(30);
                var alert = driver.SwitchTo().Alert();
                alert.Accept();
                IWebElement timerProgress = driver.FindElement(By.Id("progressText"));
                String actualTime = timerProgress.Text;
                var expectedTime = "Time Expired:";
                var results = expectedTime == actualTime;
                return results;
            }

            private bool WaitUntilElementVisiblebyIdTestCase(SauceLabsDriver driver)
            {
                driver.Navigate().GoToUrl("https://www.google.com/");
                driver.FindElement(By.Id("gbqfq")).Clear();
                driver.FindElement(By.Id("gbqfq")).SendKeys("Selenium");
                driver.WaitUntilElementVisibleByID("res");
                return driver.FindElementByLinkText("Selenium - Web Browser Automation").Displayed;
            }


            private bool IsAlertPresentTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://newtours.demoaut.com/");
                driver.FindElement(By.Name("userName")).Clear();
                return !driver.IsAlertPresent();
            }

            private bool ClickRadioButtonByIDTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://www.w3.org/WAI/UA/TS/html401/cp0101/0101-RADIO.html");
                driver.clickRadioButtonByID("button2", "2");
                var results = driver.FindElement(By.Id("button2")).Selected;
                return results;
            }

            private bool ClickRadioButtonByNameTestCase(SauceLabsDriver driver)
            {
                driver.Navigate().GoToUrl("http://www.w3schools.com/html/tryit.asp?filename=tryhtml_radio");
                driver.SwitchTo().Frame("iframeResult");
                driver.clickRadioButtonByName("sex", "male");
                var results = driver.FindElement(By.Name("sex")).Selected;
                return results;
            }

            private bool ClickCheckBoxByIDTestCase(SauceLabsDriver driver)
            {
                driver.Navigate().GoToUrl("http://www.utexas.edu/learn/forms/checkboxes.html");
                driver.ExecuteScript("window.scrollBy(0, " + 800 + ");");
                driver.clickCheckBoxByID("programming", "yes");
                driver.clickCheckBoxByID("writing", "yes");
                var results = driver.FindElement(By.Id("writing")).Selected;
                return results;
            }

            private bool PressEnterTestCase(SauceLabsDriver driver)
            {
                driver.Navigate().GoToUrl("https://www.google.com/");
                driver.FindElement(By.Id("gbqfq")).Clear();
                driver.FindElement(By.Id("gbqfq")).SendKeys("SharpSauce");
                driver.PressEnter();
                var results = driver.Title.Contains("SharpSauce");
                return results;
                
            }
            
            private bool LoginByNameTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://newtours.demoaut.com/");
                driver.loginByName(loginName, loginPassword, "userName", "password");
                driver.WaitUntilElementVisibleByName("passCount");
                string expectedTitle = "Find a Flight: Mercury Tours:";
                string actualTitle = driver.Title;
                var results = actualTitle == expectedTitle;
                return results;
            }

            private bool LoginByIDTestCase(SauceLabsDriver driver)
            {
                driver.Navigate().GoToUrl("http://jsfiddle.net/simnova/GnSU9/1/embedded/result/");
                driver.SwitchTo().Frame(0);
                driver.FindElement(By.Id("personalInfoPhone")).Clear();
                driver.FindElement(By.Id("personalInfoPhone")).SendKeys("Overwrite me!");
                driver.loginByID(phone, email, "personalInfoPhone", "personalInfoEmail");
                var phoneFieldActual=driver.FindElement(By.Id("personalInfoPhone")).GetAttribute("value");
                var phoneFieldExpected = "";
                var results = phoneFieldActual == phoneFieldExpected;
                return results;
            }

            private bool TutorialDemoTestCase(SauceLabsDriver driver)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                driver.Navigate().GoToUrl("http://newtours.demoaut.com/");
                driver.loginByName(loginName, loginPassword, "userName", "password");
                String titleActual = driver.Title;
                wait.Until(ExpectedConditions.TitleContains("Find a Flight: Mercury Tours:"));
                driver.SelectDropDownValueByName("fromPort", "Seattle");
                driver.clickRadioButtonByName("tripType", "oneway");
                driver.SelectDropDownValueByName("passCount", "3");
                driver.clickRadioButtonByName("servClass", "Business");
                driver.SelectDropDownValueByName("airline", "Unified Airlines");

                var results = driver.getElementValueByName("fromPort") == "Seattle";
                    return results;
                }
        }
}
