using MarsQA.Helpers;
using MarsQA.Pages;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.Utils
{
    [Binding]
    public class Start : Driver
    {

        [BeforeFeature("@Languages")]
        //[BeforeFeature()]
        public static void Setup()
        {
            //launch the browser
            Initialize();
            LoginPage.LoginSteps();
            HomePage.GoToProfilePage();
        }

        //[AfterFeature("@LanguageSkills")]
        [AfterTestRun]
        public static void TearDown()
        {
            Driver.Close();
        }
    }
}
