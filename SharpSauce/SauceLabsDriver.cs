using System;
using OpenQA.Selenium.Remote;

namespace SharpSauce
{
    public class SauceLabsDriver : RemoteWebDriver
    {
        public SauceLabsDriver(Uri uri, DesiredCapabilities capabilities) : base(uri, capabilities)
        { 
        } 

        public String GetExecutionId() 
       {
           return this.SessionId.ToString(); 
       }


    }
}
