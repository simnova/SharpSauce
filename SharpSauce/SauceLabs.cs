using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace SharpSauce
{
    public class SauceLabs
    {
        private readonly string _userName;
        private readonly string _accessKey;
        public int _timeout;

        public enum ScreenResolutions
        {
            screen800x600,
            screen1024x768,
            screen1280x1024,
            screenDefault
        }

        public enum BrowserVersions
        {
            // ReSharper disable InconsistentNaming
            ie11win81,
            ff3win81,
            ff35win81,
            ff36win81,
            ff4win81,
            ff5win81,
            ff6win81,
            ff7win81,
            ff8win81,
            ff9win81,
            ff10win81,
            ff11win81,
            ff12win81,
            ff13win81,
            ff14win81,
            ff15win81,
            ff16win81,
            ff17win81,
            ff18win81,
            ff19win81,
            ff20win81,
            ff21win81,
            ff22win81,
            ff23win81,
            ff24win81,
            ff25win81,
            gc26win81,
            gc27win81,
            gc28win81,
            gc29win81,
            gc30win81,
            gc31win81,

            android4,
            android4tablet,
            iphone43,
            iphone5,
            iphone51,
            iphone6,
            iphone61,
            ipad43,
            ipad5,
            ipad51,
            ipad6,
            ipad61,

            ie10win8,
            ff3win8,
            ff35win8,
            ff36win8,
            ff4win8,
            ff5win8,
            ff6win8,
            ff7win8,
            ff8win8,
            ff9win8,
            ff10win8,
            ff11win8,
            ff12win8,
            ff13win8,
            ff14win8,
            ff15win8,
            ff16win8,
            ff17win8,
            ff18win8,
            ff19win8,
            ff20win8,
            ff21win8,
            ff22win8,
            ff23win8,
            ff24win8,
            ff25win8,
            gc26win8,
            gc27win8,
            gc28win8,
            gc29win8,
            gc30win8,
            gc31win8,

            ie8win7,
            ie9win7,
            ie10win7,
            ff3win7,
            ff35win7,
            ff36win7,
            ff4win7,
            ff5win7,
            ff6win7,
            ff7win7,
            ff8win7,
            ff9win7,
            ff10win7,
            ff11win7,
            ff12win7,
            ff13win7,
            ff14win7,
            ff15win7,
            ff16win7,
            ff17win7,
            ff18win7,
            ff19win7,
            ff20win7,
            ff21win7,
            ff22win7,
            ff23win7,
            ff24win7,
            ff25win7,
            gc26win7,
            gc27win7,
            gc28win7,
            gc29win7,
            gc30win7,
            gc31win7,
            opera11win7,
            opera12win7,
            safari5win7,

            ie8winXP,
            ie7winXP,
            ie6winXP,
            ff3winXP,
            ff35winXP,
            ff36winXP,
            ff4winXP,
            ff5winXP,
            ff6winXP,
            ff7winXP,
            ff8winXP,
            ff9winXP,
            ff10winXP,
            ff11winXP,
            ff12winXP,
            ff13winXP,
            ff14winXP,
            ff15winXP,
            ff16winXP,
            ff17winXP,
            ff18winXP,
            ff19winXP,
            ff20winXP,
            ff21winXP,
            ff22winXP,
            ff23winXP,
            ff24winXP,
            ff25winXP,
            gc26winXP,
            gc27winXP,
            gc28winXP,
            gc29winXP,
            gc30winXP,
            gc31winXP,
            opera11winXP,
            opera12winXP,


            ff4SnowLeopard,
            ff5SnowLeopard,
            ff6SnowLeopard,
            ff7SnowLeopard,
            ff8SnowLeopard,
            ff9SnowLeopard,
            ff10SnowLeopard,
            ff11SnowLeopard,
            ff12SnowLeopard,
            ff13SnowLeopard,
            ff14SnowLeopard,
            ff15SnowLeopard,
            ff16SnowLeopard,
            ff17SnowLeopard,
            ff18SnowLeopard,
            ff19SnowLeopard,
            ff20SnowLeopard,
            ff21SnowLeopard,
            ff22SnowLeopard,
            ff23SnowLeopard,
            ff24SnowLeopard,
            ff25SnowLeopard,
            gc28SnowLeopard,
            safari5SnowLeopard,

            gc27MountainLion,
            safari6MountainLion,

            gc31Mavericks,
            safari7Mavericks,

            ff3linux,
            ff4linux,
            ff5linux,
            ff6linux,
            ff7linux,
            ff8linux,
            ff9linux,
            ff10linux,
            ff11linux,
            ff12linux,
            ff13linux,
            ff14linux,
            ff15linux,
            ff16linux,
            ff17linux,
            ff18linux,
            ff19linux,
            ff20linux,
            ff21linux,
            ff22linux,
            ff23linux,
            ff24linux,
            ff25linux,
            opera12linux,
            gc26linux,
            gc27linux,
            gc28linux,
            gc29linux,
            gc30linux
            // ReSharper restore InconsistentNaming

        }

        public SauceLabs(string userName, string accessKey, int timeoutInMin)
        {
            _userName = userName;
            _accessKey = accessKey;
            _timeout = timeoutInMin;
            
        }

        public bool RunRemoteTestCase(SauceLabsDriver driver, Func<IWebDriver,bool> testCaseToRun )
        {
            var sauceRest = new SauceRest(_userName, _accessKey);
            var results = testCaseToRun(driver);
            if (results)
            {
                sauceRest.JobPassed(driver.GetExecutionId());
            }
            else
            {
                sauceRest.JobFailed(driver.GetExecutionId());
            }
            driver.Quit();

            return results;
        }

        public bool RunRemoteTestCase(SauceLabsDriver driver, Func<SauceLabsDriver, bool> testCaseToRun)
        {
            var sauceRest = new SauceRest(_userName, _accessKey);
            var results = testCaseToRun(driver);
            if (results)
            {
                sauceRest.JobPassed(driver.GetExecutionId());
            }
            else
            {
                sauceRest.JobFailed(driver.GetExecutionId());
            }
            driver.Quit();

            return results;
        }

        public class SauceLabsConfig
        {
            private ScreenResolutions _sceenResolution;
            private ScreenOrientation _screenOrientation;
            private BrowserVersions _broswerVersion;
            private int _timeout;
            private string _testName;
            private string _buildNumber;
            private string[] _tags;
            private bool _recordVideo = true;
            private bool _captureHTML = false;
            private bool _recordScreenshots = true;
            private customData _customData;

            public ScreenResolutions ScreenResolution
            {
                get { return _sceenResolution; }
                set { _sceenResolution = value; }
            }

            public ScreenOrientation ScreenOrientation
            {
                get { return _screenOrientation; }
                set { _screenOrientation = value; }
            }
            
            public BrowserVersions BrowserVersion
            {
                get { return _broswerVersion; }
                set { _broswerVersion = value; }
            }

            public int Timeout
            {
                get { return _timeout; }
                set { _timeout = value; }
            }
            public string TestName
            {
                get { return _testName; }
                set { _testName = value; }
            }
            public string BuildNumber
            {
                get { return _buildNumber; }
                set { _buildNumber = value; }
            }
            public string[] Tags
            {
                get { return _tags; }
                set { _tags = value; }
            }
            public bool RecordVideo
            {
                get { return _recordVideo; }
                set { _recordVideo = value; }
            }
            public bool CaptureHTML
            {
                get { return _captureHTML; }
                set { _captureHTML = value; }
            }
            public bool RecordScreenshots
            {
                get { return _recordScreenshots; }
                set { _recordScreenshots = value; }
            }
            public customData CustomData
            {
                get { return _customData; }
                set { _customData = value; }
            }
        }

        public class customData
        {
            private string _release;
            private string _commit;
            private string _server;
            private int _executionNumber;
            private bool _staging;

            public string Release
            {
                get { return _release; }
                set { _release = value; }
            }
            public string Commit
            {
                get { return _commit; }
                set { _commit = value;}
            }
            public string Server
            {
                get { return _server; }
                set { _server = value; }
            }
            public int ExecutionNumber
            {
                get { return _executionNumber; }
                set { _executionNumber = value; }
            }
            public bool Staging
            {
                get { return _staging; }
                set { _staging = value; }
            }
        }


        public SauceLabsDriver GetRemoteDriver(SauceLabsConfig config)
        {
            
            DesiredCapabilities caps;
            
            var versionName = config.BrowserVersion.ToString();
            if (versionName.StartsWith("ie"))
            {
                caps = DesiredCapabilities.InternetExplorer();
                
            }
            else if (versionName.StartsWith("safari"))
            {
                caps = DesiredCapabilities.Safari();
            }
            else if (versionName.StartsWith("ff"))
            {
                caps = DesiredCapabilities.Firefox();
            }
            else if (versionName.StartsWith("andriod"))
            {
                caps = DesiredCapabilities.Android();
            }
            else if (versionName.StartsWith("iphone"))
            {
                caps = DesiredCapabilities.IPhone();
            }
            else if (versionName.StartsWith("ipad"))
            {
                caps = DesiredCapabilities.IPad();
            }
            else if (versionName.StartsWith("gc"))
            {
                caps = DesiredCapabilities.Chrome();
            }
            else if (versionName.StartsWith("opera"))
            {
                caps = DesiredCapabilities.Opera();
            }
            else
            {
                throw new ArgumentException("Browser Version not Supported");
            }


            caps.SetCapability("name", config.TestName + " on " + versionName);

            switch (config.BrowserVersion)
            {
                //Mobile and tablet browsers
                case BrowserVersions.android4:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "4");
                    break;
                case BrowserVersions.android4tablet:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "4");
                    caps.SetCapability("deviceType", "tablet");
                    break;
                case BrowserVersions.iphone61:
                    caps.SetCapability(CapabilityType.BrowserName, "iphone");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "6.1");
                    break;
                case BrowserVersions.iphone6:
                    caps.SetCapability(CapabilityType.BrowserName, "iphone");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "6.0");
                    break;
                case BrowserVersions.iphone51:
                    caps.SetCapability(CapabilityType.BrowserName, "iphone");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "5.1");
                    break;
                case BrowserVersions.iphone5:
                    caps.SetCapability(CapabilityType.BrowserName, "iphone");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "5.0");
                    break;
                case BrowserVersions.iphone43:
                    caps.SetCapability(CapabilityType.BrowserName, "iphone");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "4.3");
                    break;
                case BrowserVersions.ipad61:
                    caps.SetCapability(CapabilityType.BrowserName, "ipad");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "6.1");
                    break;
                case BrowserVersions.ipad6:
                    caps.SetCapability(CapabilityType.BrowserName, "ipad");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "6.0");
                    break;
                case BrowserVersions.ipad51:
                    caps.SetCapability(CapabilityType.BrowserName, "ipad");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "5.1");
                    break;
                case BrowserVersions.ipad5:
                    caps.SetCapability(CapabilityType.BrowserName, "ipad");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "5.0");
                    break;
                case BrowserVersions.ipad43:
                    caps.SetCapability(CapabilityType.BrowserName, "ipad");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "4.3");
                    break;
                    //Windows 8.1 browsers
                case BrowserVersions.ie11win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.ff3win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "3.0");
                    break;
                case BrowserVersions.ff35win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "3.5");
                    break;
                case BrowserVersions.ff36win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "3.6");
                    break;
                case BrowserVersions.ff4win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "4");
                    break;
                case BrowserVersions.ff5win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                case BrowserVersions.ff6win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.ff7win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "7");
                    break;
                case BrowserVersions.ff8win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "8");
                    break;
                case BrowserVersions.ff9win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "9");
                    break;
                case BrowserVersions.ff10win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.ff11win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.ff12win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                case BrowserVersions.ff13win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "13");
                    break;
                case BrowserVersions.ff14win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "14");
                    break;
                case BrowserVersions.ff15win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "15");
                    break;
                case BrowserVersions.ff16win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "16");
                    break;
                case BrowserVersions.ff17win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "17");
                    break;
                case BrowserVersions.ff18win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "18");
                    break;
                case BrowserVersions.ff19win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "19");
                    break;
                case BrowserVersions.ff20win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "20");
                    break;
                case BrowserVersions.ff21win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "21");
                    break;
                case BrowserVersions.ff22win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "22");
                    break;
                case BrowserVersions.ff23win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "23");
                    break;
                case BrowserVersions.ff24win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "24");
                    break;
                case BrowserVersions.ff25win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "25");
                    break;
                case BrowserVersions.gc26win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "26");
                    break;
                case BrowserVersions.gc27win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "27");
                    break;
                case BrowserVersions.gc28win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "28");
                    break;
                case BrowserVersions.gc29win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "29");
                    break;
                case BrowserVersions.gc30win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "30");
                    break;
                case BrowserVersions.gc31win81:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8.1");
                    caps.SetCapability(CapabilityType.Version, "31");
                    break;
                    //Windows 8 Browsers
                case BrowserVersions.ie10win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.ff3win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "3.0");
                    break;
                case BrowserVersions.ff35win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "3.5");
                    break;
                case BrowserVersions.ff36win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "3.6");
                    break;
                case BrowserVersions.ff4win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "4");
                    break;
                case BrowserVersions.ff5win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                case BrowserVersions.ff6win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.ff7win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "7");
                    break;
                case BrowserVersions.ff8win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "8");
                    break;
                case BrowserVersions.ff9win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "9");
                    break;
                case BrowserVersions.ff10win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.ff11win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.ff12win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                case BrowserVersions.ff13win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "13");
                    break;
                case BrowserVersions.ff14win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "14");
                    break;
                case BrowserVersions.ff15win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "15");
                    break;
                case BrowserVersions.ff16win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "16");
                    break;
                case BrowserVersions.ff17win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "17");
                    break;
                case BrowserVersions.ff18win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "18");
                    break;
                case BrowserVersions.ff19win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "19");
                    break;
                case BrowserVersions.ff20win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "20");
                    break;
                case BrowserVersions.ff21win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "21");
                    break;
                case BrowserVersions.ff22win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "22");
                    break;
                case BrowserVersions.ff23win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "23");
                    break;
                case BrowserVersions.ff24win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "24");
                    break;
                case BrowserVersions.ff25win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "25");
                    break;
                case BrowserVersions.gc26win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "26");
                    break;
                case BrowserVersions.gc27win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                case BrowserVersions.gc28win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "28");
                    break;
                case BrowserVersions.gc29win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "29");
                    break;
                case BrowserVersions.gc30win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "30");
                    break;
                case BrowserVersions.gc31win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "31");
                    break;
                    //Windows 7 browers
                case BrowserVersions.ie8win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "8");
                    break;
                case BrowserVersions.ie9win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "9");
                    break;
                case BrowserVersions.ie10win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.ff3win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "3.0");
                    break;
                case BrowserVersions.ff35win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "3.5");
                    break;
                case BrowserVersions.ff36win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "3.6");
                    break;
                case BrowserVersions.ff4win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "4");
                    break;
                case BrowserVersions.ff5win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                case BrowserVersions.ff6win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.ff7win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "7");
                    break;
                case BrowserVersions.ff8win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "8");
                    break;
                case BrowserVersions.ff9win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "9");
                    break;
                case BrowserVersions.ff10win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.ff11win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.ff12win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                case BrowserVersions.ff13win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "13");
                    break;
                case BrowserVersions.ff14win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "14");
                    break;
                case BrowserVersions.ff15win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "15");
                    break;
                case BrowserVersions.ff16win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "16");
                    break;
                case BrowserVersions.ff17win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "17");
                    break;
                case BrowserVersions.ff18win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "18");
                    break;
                case BrowserVersions.ff19win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "19");
                    break;
                case BrowserVersions.ff20win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "20");
                    break;
                case BrowserVersions.ff21win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "21");
                    break;
                case BrowserVersions.ff22win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "22");
                    break;
                case BrowserVersions.ff23win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "23");
                    break;
                case BrowserVersions.ff24win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "24");
                    break;
                case BrowserVersions.ff25win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "25");
                    break;
                case BrowserVersions.gc26win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "26");
                    break;
                case BrowserVersions.gc27win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                case BrowserVersions.gc28win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "28");
                    break;
                case BrowserVersions.gc29win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "29");
                    break;
                case BrowserVersions.gc30win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "30");
                    break;
                case BrowserVersions.gc31win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "31");
                    break;
                case BrowserVersions.opera11win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.opera12win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                case BrowserVersions.safari5win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                //Windows XP browers
                case BrowserVersions.ie6winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.ie8winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "8");
                    break;
                case BrowserVersions.ie7winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "7");
                    break;
                case BrowserVersions.ff10winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.ff3winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "3.0");
                    break;
                case BrowserVersions.ff35winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "3.5");
                    break;
                case BrowserVersions.ff36winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "3.6");
                    break;
                case BrowserVersions.ff4winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "4");
                    break;
                case BrowserVersions.ff5winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                case BrowserVersions.ff6winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.ff7winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "7");
                    break;
                case BrowserVersions.ff8winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "8");
                    break;
                case BrowserVersions.ff9winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "9");
                    break;
                case BrowserVersions.ff11winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.ff12winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                case BrowserVersions.ff13winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "13");
                    break;
                case BrowserVersions.ff14winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "14");
                    break;
                case BrowserVersions.ff15winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "15");
                    break;
                case BrowserVersions.ff16winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "16");
                    break;
                case BrowserVersions.ff17winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "17");
                    break;
                case BrowserVersions.ff18winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "18");
                    break;
                case BrowserVersions.ff19winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "19");
                    break;
                case BrowserVersions.ff20winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "20");
                    break;
                case BrowserVersions.ff21winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "21");
                    break;
                case BrowserVersions.ff22winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "22");
                    break;
                case BrowserVersions.ff23winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "23");
                    break;
                case BrowserVersions.ff24winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "24");
                    break;
                case BrowserVersions.ff25winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "25");
                    break;
                case BrowserVersions.gc26winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "26");
                    break;
                case BrowserVersions.gc27winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "27");
                    break;
                case BrowserVersions.gc28winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "28");
                    break;
                case BrowserVersions.gc29winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "29");
                    break;
                case BrowserVersions.gc30winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "30");
                    break;
                case BrowserVersions.gc31winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "31");
                    break;
                case BrowserVersions.opera11winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.opera12winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                    //Snow Leopard Browsers
                case BrowserVersions.ff4SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "4");
                    break;
                case BrowserVersions.ff5SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                case BrowserVersions.ff6SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.ff7SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "7");
                    break;
                case BrowserVersions.ff8SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "8");
                    break;
                case BrowserVersions.ff9SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "9");
                    break;
                case BrowserVersions.ff10SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.ff11SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.ff12SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                case BrowserVersions.ff13SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "13");
                    break;
                case BrowserVersions.ff14SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "14");
                    break;
                case BrowserVersions.ff15SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "15");
                    break;
                case BrowserVersions.ff16SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "16");
                    break;
                case BrowserVersions.ff17SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "17");
                    break;
                case BrowserVersions.ff18SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "18");
                    break;
                case BrowserVersions.ff19SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "19");
                    break;
                case BrowserVersions.ff20SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "20");
                    break;
                case BrowserVersions.ff21SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "21");
                    break;
                case BrowserVersions.ff22SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "22");
                    break;
                case BrowserVersions.ff23SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "23");
                    break;
                case BrowserVersions.ff24SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "24");
                    break;
                case BrowserVersions.ff25SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "25");
                    break;
                case BrowserVersions.gc28SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                case BrowserVersions.safari5SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                    //Mountain Lion Browsers
                case BrowserVersions.gc27MountainLion:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                case BrowserVersions.safari6MountainLion:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                    //Mavericks Browsers
                case BrowserVersions.gc31Mavericks:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.9");
                    caps.SetCapability(CapabilityType.Version, "31");
                    break;
                case BrowserVersions.safari7Mavericks:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.9");
                    caps.SetCapability(CapabilityType.Version, "7");
                    break;
                    //Linux Browsers
                case BrowserVersions.ff3linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "3");
                    break;
                case BrowserVersions.ff4linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "4");
                    break;
                case BrowserVersions.ff5linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                case BrowserVersions.ff6linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.ff7linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "7");
                    break;
                case BrowserVersions.ff8linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "8");
                    break;
                case BrowserVersions.ff9linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "9");
                    break;
                case BrowserVersions.ff10linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.ff11linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.ff12linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                case BrowserVersions.ff13linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "13");
                    break;
                case BrowserVersions.ff14linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "14");
                    break;
                case BrowserVersions.ff15linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "15");
                    break;
                case BrowserVersions.ff16linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "16");
                    break;
                case BrowserVersions.ff17linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "17");
                    break;
                case BrowserVersions.ff18linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "18");
                    break;
                case BrowserVersions.ff19linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "19");
                    break;
                case BrowserVersions.ff20linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "20");
                    break;
                case BrowserVersions.ff21linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "21");
                    break;
                case BrowserVersions.ff22linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "22");
                    break;
                case BrowserVersions.ff23linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "23");
                    break;
                case BrowserVersions.ff24linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "24");
                    break;
                case BrowserVersions.ff25linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "25");
                    break;
                case BrowserVersions.gc26linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "26");
                    break;
                case BrowserVersions.gc27linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                case BrowserVersions.gc28linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "28");
                    break;
                case BrowserVersions.gc29linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "29");
                    break;
                case BrowserVersions.gc30linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "30");
                    break;
                case BrowserVersions.opera12linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
            }

            switch (config.ScreenResolution)
            {
                case ScreenResolutions.screen1024x768:
                    caps.SetCapability("screen-resolution", "1024x768");
                    break;
                case ScreenResolutions.screen1280x1024:
                    caps.SetCapability("screen-resolution", "1280x1024");
                    break;
                case ScreenResolutions.screen800x600:
                    caps.SetCapability("screen-resolution", "800x600");
                    break;
                case ScreenResolutions.screenDefault:
                default:
                    break;
            }
            
            switch (config.ScreenOrientation)
            {
                case ScreenOrientation.Landscape:
                    caps.SetCapability("device-orientation", "landscape");
                    break;
                case ScreenOrientation.Portrait:
                    caps.SetCapability("device-orientation", "portrait");
                    break;
            }
            
            caps.SetCapability("username", _userName);
            caps.SetCapability("accessKey", _accessKey);
            caps.SetCapability("idle-timeout", config.Timeout);
            caps.SetCapability("build", config.BuildNumber);
            caps.SetCapability("tags", config.Tags);
            caps.SetCapability("record-video", config.RecordVideo);
            caps.SetCapability("capture-html", config.CaptureHTML);
            caps.SetCapability("record-screenshots", config.RecordScreenshots);
            caps.SetCapability("custom-data", config.CustomData);
            //TimeSpan timeout = DateTime.Now.AddMinutes(5)-DateTime.Now;
            TimeSpan timeout = TimeSpan.FromMinutes(_timeout);
            var driver = new SauceLabsDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, timeout);
            return driver;
        }

    }
}
