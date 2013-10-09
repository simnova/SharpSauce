using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace SharpSauce
{
    public class SauceLabs
    {
        private readonly string _userName;
        private readonly string _accessKey;

        public enum BrowserVersions
        {
            // ReSharper disable InconsistentNaming
            ie10win8,
            ie8win7,
            ie9win7,
            ie8winXP,
            ie7winXP,
            ie6winXP,
            safari6,
            safari5,
            ff21winXP,
            ff17winXP,
            ff14winXP,
            ff10winXP,
            andriod4,
            andriod4tablet,
            iphone43,
            iphone5,
            iphone51,
            iphone6,
            ipad43,
            ipad5,
            ipad51,
            ipad6,

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
            gc27win8,

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
            gc27win7,
            opera11win7,
            opera12win7,
            safari5win7,

            ff3winXP,
            ff35winXP,
            ff36winXP,
            ff4winXP,
            ff5winXP,
            ff6winXP,
            ff7winXP,
            ff8winXP,
            ff9winXP,
            ff11winXP,
            ff12winXP,
            ff13winXP,
            ff15winXP,
            ff16winXP,
            ff18winXP,
            ff19winXP,
            ff20winXP,
            gc27winXP,
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
            gc27SnowLeopard,

            gc27MountainLion,

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
            opera12linux,
            gc27linux
            // ReSharper restore InconsistentNaming

        }
        public SauceLabs(string userName, string accessKey )
        {
            _userName = userName;
            _accessKey = accessKey;
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

        public SauceLabsDriver GetRemoteDriver(BrowserVersions version, string name, string screenRes, int timeout)
        {
            
            DesiredCapabilities caps;
            
            var versionName = version.ToString();
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


            caps.SetCapability("name", name + " on " + versionName);

            switch (version)
            {
                case BrowserVersions.ie10win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.ie8win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "8");
                    break;
                case BrowserVersions.ie9win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "9");
                    break;
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
                case BrowserVersions.safari6:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.safari5:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                case BrowserVersions.ff21winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "21");
                    break;
                case BrowserVersions.ff17winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "17");
                    break;
                case BrowserVersions.ff14winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "14");
                    break;
                case BrowserVersions.ff10winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "10");
                    break;
                case BrowserVersions.andriod4:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "4");
                    break;
                case BrowserVersions.andriod4tablet:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "4");
                    caps.SetCapability("deviceType", "tablet");
                    break;
                case BrowserVersions.iphone6:
                    caps.SetCapability(CapabilityType.BrowserName, "iphone");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.iphone51:
                    caps.SetCapability(CapabilityType.BrowserName, "iphone");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "5.1");
                    break;
                case BrowserVersions.iphone5:
                    caps.SetCapability(CapabilityType.BrowserName, "iphone");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                case BrowserVersions.iphone43:
                    caps.SetCapability(CapabilityType.BrowserName, "iphone");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "4.3");
                    break;
                case BrowserVersions.ipad6:
                    caps.SetCapability(CapabilityType.BrowserName, "ipad");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "6");
                    break;
                case BrowserVersions.ipad51:
                    caps.SetCapability(CapabilityType.BrowserName, "ipad");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "5.1");
                    break;
                case BrowserVersions.ipad5:
                    caps.SetCapability(CapabilityType.BrowserName, "ipad");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "5");
                    break;
                case BrowserVersions.ipad43:
                    caps.SetCapability(CapabilityType.BrowserName, "ipad");
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "4.3");
                    break;
                    //Added Browsers
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
                case BrowserVersions.gc27win8:
                    caps.SetCapability(CapabilityType.Platform, "Windows 8");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                    //Added Windows 7 browers
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
                case BrowserVersions.gc27win7:
                    caps.SetCapability(CapabilityType.Platform, "Windows 7");
                    caps.SetCapability(CapabilityType.Version, "");
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
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                //Added Windows XP browers
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
                case BrowserVersions.ff15winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "15");
                    break;
                case BrowserVersions.ff16winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "16");
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
                case BrowserVersions.gc27winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                case BrowserVersions.opera11winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "11");
                    break;
                case BrowserVersions.opera12winXP:
                    caps.SetCapability(CapabilityType.Platform, "Windows XP");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
                    //Added Snow Leopard Browsers
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
                case BrowserVersions.gc27SnowLeopard:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.6");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                    //Mountain Lion Browsers
                case BrowserVersions.gc27MountainLion:
                    caps.SetCapability(CapabilityType.Platform, "OS X 10.8");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                    //Linux Browsers
                case BrowserVersions.ff3linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "3.0");
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
                case BrowserVersions.gc27linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "");
                    break;
                case BrowserVersions.opera12linux:
                    caps.SetCapability(CapabilityType.Platform, "Linux");
                    caps.SetCapability(CapabilityType.Version, "12");
                    break;
            }

            caps.SetCapability("username", _userName);
            caps.SetCapability("accessKey", _accessKey);
            caps.SetCapability("screen-resolution", screenRes);
            caps.SetCapability("idle-timeout", timeout);
            var driver = new SauceLabsDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps);
            return driver;
        }

    }
}
