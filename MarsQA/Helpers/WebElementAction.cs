using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarsQA.Helpers.Wait;
using static MarsQA.Helpers.WebElementAction;

namespace MarsQA.Helpers
{
    //Common methods related to IWebElements
    public class WebElementAction
    {
        //Public variable to keep IWebElement wait type
        public struct WaitType
        {
            public bool ToBeVisible;
            public bool ToBeClickable;

            public WaitType()
            {
                ToBeVisible = false;
                ToBeClickable = false;
            }
        }

        //Public variable to keep locator type when finding the IWebElement
        public struct LocatorType
        {
            public bool XPath;
            public bool Id;

            public LocatorType()
            {
                XPath = false;
                Id = false;
            }
        }

        //Common method to get IWebElement
        public static IWebElement FindWebElement(LocatorType locatorType, string locatorValue)
        {
            if (locatorType.Id == true)
            {
                IWebElement webElement = Driver.driver.FindElement(By.Id(locatorValue));
                return webElement;
            }
            else if (locatorType.XPath == true)
            {
                IWebElement webElement = Driver.driver.FindElement(By.XPath(locatorValue));
                return webElement;
            }
            return null;
        }

        //Common method to Send Keys in IWebElement
        public static void SendKeys(WaitType waitType, LocatorType locatorType, string locatorValue, int seconds, string keyValue)
        {
            if (waitType.ToBeVisible == true)
            {
                Wait.WaitToBeVisible(locatorType, locatorValue, seconds);
            }

            IWebElement webElement = FindWebElement(locatorType, locatorValue);
            webElement.Clear();
            webElement.SendKeys(keyValue);
        }

        //Common method to do Button Click in IWebElement
        public static void ButtonClick(WaitType waitType, LocatorType locatorType, string locatorValue, int seconds)
        {
            if (waitType.ToBeVisible == true)
            {
                Wait.WaitToBeVisible(locatorType, locatorValue, seconds);
            }
            else if (waitType.ToBeClickable == true)
            {
                Wait.WaitToBeClickable(locatorType, locatorValue, seconds);
            }

            IWebElement webElement = FindWebElement(locatorType, locatorValue);
            webElement.Click();
        }
    }
}
