using MarsQA.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.Pages
{
    public class LoginPage: Driver
    {
        private static IWebElement loginButton;
        private static IWebElement userNameTextbox;
        private static IWebElement passwordTextbox;

        public static void LoginSteps() 
        {
            Driver.NavigateUrl();

            driver.FindElement(By.XPath("//a[contains(text(),'Sign In')]")).Click();
            renderPageElements();

            userNameTextbox.SendKeys(ConstantHelpers.UserName);
            passwordTextbox.SendKeys(ConstantHelpers.Password);
            loginButton.Click();

            //waitType = new WebElementAction.WaitType();
            //waitType.ToBeVisible = true;

            //WebElementAction.SendKeys(waitType, locatorType, "(//INPUT[@type='text'])[2]", 3, ConstantHelpers.UserName);
            //WebElementAction.SendKeys(waitType, locatorType, "//input[@type='password']", 3, ConstantHelpers.Password);

            //waitType = new WebElementAction.WaitType();
            //waitType.ToBeClickable = true;

            //WebElementAction.ButtonClick(waitType, locatorType, "//button[contains(text(),'Login')]", 3);

        }

        public static void renderPageElements()
        {
            loginButton = driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));
            userNameTextbox = driver.FindElement(By.XPath("(//INPUT[@type='text'])[2]"));
            passwordTextbox = driver.FindElement(By.XPath("//input[@type='password']"));
        }

    }
}
