using MarsQA.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.Pages
{
    public class LoginPage
    {
        public static void LoginSteps() 
        {
            Driver.NavigateUrl();
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            WebElementAction.WaitType waitType = new WebElementAction.WaitType();
            waitType.ToBeClickable = true;

            WebElementAction.ButtonClick(waitType, locatorType, "//a[contains(text(),'Sign In')]", 3);

            waitType = new WebElementAction.WaitType();
            waitType.ToBeVisible = true;

            WebElementAction.SendKeys(waitType, locatorType, "(//INPUT[@type='text'])[2]", 3, ConstantHelpers.UserName);
            WebElementAction.SendKeys(waitType, locatorType, "//input[@type='password']", 3, ConstantHelpers.Password);

            waitType = new WebElementAction.WaitType();
            waitType.ToBeClickable = true;

            WebElementAction.ButtonClick(waitType, locatorType, "//button[contains(text(),'Login')]", 3);
            
        }

    }
}
