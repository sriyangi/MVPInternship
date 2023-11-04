using MarsQA.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.Pages
{
    public class HomePage: Driver 
    {
        public static void GoToProfilePage()
        {
            driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[1]/div/a[2]")).Click();

            //WebElementAction.LocatorType locatorType = new()
            //{
            //    XPath = true
            //};

            //WebElementAction.WaitType waitType = new WebElementAction.WaitType();
            //waitType.ToBeClickable = true;

            ////Click Profile Tab
            //WebElementAction.ButtonClick(waitType, locatorType, "//*[@id=\"account-profile-section\"]/div/section[1]/div/a[2]", 3);
        }
    }
}
