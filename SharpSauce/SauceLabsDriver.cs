using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System.Threading;


namespace SharpSauce
{
    public class SauceLabsDriver : RemoteWebDriver
    {
        public SauceLabsDriver(Uri uri, DesiredCapabilities capabilities, TimeSpan timeout) : base(uri, capabilities, timeout)
        {
            
            SauceLabsDriver.DefaultCommandTimeout.Add(timeout).Subtract(TimeSpan.FromMinutes(1));
        } 

        public String GetExecutionId() 
       {
           return this.SessionId.ToString(); 
       }

        public Boolean IsAlertPresent()
        {
            try
            {
                this.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }


        //Login to a page by given username and password; Uses IDs to locate webelements
        //note: This method logs in by pressing "ENTER", it does not click a button.
        //note: Don't use this method if you intend to test a login button or if pressing "ENTER" does not submit text fields.
        public void loginByID(String Username, String Password, String usernameID, String passwordID)
        {
            this.enterValueByID(usernameID, Username);
            this.enterValueByID(passwordID, Password);
            this.Keyboard.SendKeys(Keys.Enter);
        }

        //Login to a page by given username and password; Uses names to locate webelements
        //note: This method logs in by pressing "ENTER", it does not click a button.
        //note: Don't use this method if you intend to test a login button or if pressing "ENTER" does not submit text fields.
        public void loginByName(String Username, String Password, String usernameName, String passwordName)
        {
            this.enterValueByName(usernameName, Username);
            this.enterValueByName(passwordName, Password);
            this.Keyboard.SendKeys(Keys.Enter);
        }

        //Login to a page by given username and password; Uses tags and attributes to locate webelements
        //note: This method logs in by pressing "ENTER", it does not click a button.
        //note: Don't use this method if you intend to test a login button or if pressing "ENTER" does not submit text fields.
        public void loginByAttribute(String Username, String Password,
            String usernameAttribute, String usernameValue, String passwordAttribute, String passwordValue)
        {
            this.enterValueByAttribute(usernameAttribute, usernameValue, Username);
            this.enterValueByAttribute(passwordAttribute, passwordValue, Password);
            this.Keyboard.SendKeys(Keys.Enter);
        }

        //click on a link of a page; Uses link text to locate webelement
        //Originally used for logging out by links
        public void ClickonLink(String linkingText)
        {
            this.FindElementByLinkText(linkingText).Click();

        }

        // Tells browser that 'Enter' button has been pressed
        public void PressEnter()
        {
            this.Keyboard.SendKeys(Keys.Enter);
        }

        //Selects an element from a drop down table; Drop down table located by given ID.
        public void SelectDropDownValueById(String DropDownID, String Value)
        {
            IWebElement dropDownBox = this.FindElement(By.Id(DropDownID));
            SelectElement clickThis = new SelectElement(dropDownBox);
            clickThis.SelectByText(Value);
        }

        //Selects an element from a drop down table; Drop down table located by given Name.
        public void SelectDropDownValueByName(String DropDownName, String Value)
        {
            IWebElement dropDownBox = this.FindElement(By.Name(DropDownName));
            SelectElement clickThis = new SelectElement(dropDownBox);
            clickThis.SelectByText(Value);
        }

        //Selects an element from a drop down table; Drop down table located by given Tag and attribute.
        public void SelectDropDownValueByAttribute(String DropDownAttribute, String DropDownAttributeValue, String Value)
        {
            IWebElement dropDownBox = this.FindElement(By.CssSelector("select" + "[" + DropDownAttribute + "=" + DropDownAttributeValue + "]"));
            SelectElement clickThis = new SelectElement(dropDownBox);
            clickThis.SelectByText(Value);
        }

        //selects a radio button option
        public void clickRadioButtonByName(String buttonName, String buttonValue)
        {
            IList<IWebElement> radioButtonOptions = this.FindElements(By.Name(buttonName));
            foreach (IWebElement option in radioButtonOptions)
            {
                if (option.GetAttribute("value").Equals(buttonValue))
                {
                    option.Click();
                }
            }
                
        }

        public void clickRadioButtonByID(String buttonID, String buttonValue)
        {
            IList<IWebElement> radioButtonOptions = this.FindElements(By.Id(buttonID));
            foreach (IWebElement option in radioButtonOptions)
            {
                if (option.GetAttribute("value").Equals(buttonValue))
                {
                    option.Click();
                }
            }

        }

        //selects a checkbox element
        public void clickCheckBoxByName(String checkBoxName, String checkBoxValue)
        {
            IList<IWebElement> radioButtonOptions = this.FindElements(By.Name(checkBoxName));
            foreach (IWebElement option in radioButtonOptions)
            {
                if (option.GetAttribute("value").Equals(checkBoxValue))
                {
                    option.Click();
                }
            }

        }

        public void clickCheckBoxByID(String checkBoxID, String checkBoxValue)
        {
            IList<IWebElement> radioButtonOptions = this.FindElements(By.Id(checkBoxID));
            foreach (IWebElement option in radioButtonOptions)
            {
                if (option.GetAttribute("value").Equals(checkBoxValue))
                {
                    option.Click();
                }
            }

        }

        //Returns the value of a webelement; Uses ID to locate webelement
        public String getElementValueByID(String ID)
        {
           return this.FindElementById(ID).GetAttribute("value");
        }

        //Returns the value of a webelement; Uses name to locate webelement
        public String getElementValueByName(String Name)
        {
            return this.FindElementByName(Name).GetAttribute("value");
        }

        //Enters a string to a textfield webelement; Uses ID to locate webelement
        public void enterValueByID(String ID, String Value)
        {
            this.FindElementById(ID).Clear();
            this.FindElementById(ID).SendKeys(Value);
        }

        //Enters a string to a textfield webelement; Uses name to locate webelement
        public void enterValueByName(String name, String Value)
        {
            this.FindElementByName(name).Clear();
            this.FindElementByName(name).SendKeys(Value);
        }

        //Enters a string to a textfield webelement; Uses "tag[Attribute=value]" locator to locate webelement
        public void enterValueByAttribute(String Attribute, String attributeValue, String Value)
        {
            this.FindElementByCssSelector("input" + "[" + Attribute + "=" + attributeValue + "]").Clear();
            this.FindElementByCssSelector("input" + "[" + Attribute + "=" + attributeValue + "]").SendKeys(Value);
        }

        //Driver sleeps for a set given time before moving on with the test.
        public void WaitUntilTime(int TimetoWaitinSeconds)
        {
            Thread.Sleep(TimetoWaitinSeconds * 1000);
        }

        public void WaitUntilElementVisibleByID(String id)
        {
            var wait = new WebDriverWait(this, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
        }

        public void WaitUntilElementVisibleByID(String id, int minutes)
        {
            var wait = new WebDriverWait(this, TimeSpan.FromMinutes(minutes));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        }

        public void WaitUntilElementVisibleByName(String name)
        {
            var wait = new WebDriverWait(this, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name(name)));
        }

        public void WaitUntilElementVisibleByName(String name, int minutes)
        {
            var wait = new WebDriverWait(this, TimeSpan.FromMinutes(minutes));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name(name)));
        }
    }
}
