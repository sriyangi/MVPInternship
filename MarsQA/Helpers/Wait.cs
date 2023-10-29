using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.Helpers
{
    public class Wait
    {
        //Common method to wait till the Element is visible
        public static void WaitToBeVisible(WebElementAction.LocatorType locatorType, string locatorValue, int seconds)
        {
            var wait = new WebDriverWait(Driver.driver, new TimeSpan(0, 0, seconds));

            if (locatorType.Id == true)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(locatorValue)));
            }
            else if (locatorType.XPath == true)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(locatorValue)));
            }
        }

        //Common method to wait till the Element is clickable
        public static void WaitToBeClickable(WebElementAction.LocatorType locatorType, string locatorValue, int seconds)
        {
            var wait = new WebDriverWait(Driver.driver, new TimeSpan(0, 0, seconds));

            if (locatorType.Id == true)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(locatorValue)));
            }
            else if (locatorType.XPath == true)
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(locatorValue)));
            }
        }
    }
}
