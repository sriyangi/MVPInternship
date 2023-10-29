using MarsQA.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using static MarsQA.Helpers.Wait;
using static MarsQA.Helpers.WebElementAction;

namespace MarsQA.Pages
{
    public class LanguagesPage
    {
        //Create Language Record
        public void CreateRecord(string language, string languageLevel)
        {
            string languageLevelXpath = "//option[contains(text(),'" + languageLevel +"')]";
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            WebElementAction.WaitType waitType = new()
            {
                ToBeVisible = true
            };

            //If Cancel button is visible, first click it
            if (IsCancelLanguageBtnVisible()) WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Cancel']", 3);

            //Check whether the "Add New" button iss visible
            if (IsAddNewLanguageBtnVisible())
            {
                WebElementAction.ButtonClick(waitType, locatorType, "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div", 3);

                WebElementAction.SendKeys(waitType, locatorType, "//input[@type='text' and @placeholder='Add Language']", 3, language);
                WebElementAction.ButtonClick(waitType, locatorType, "//select", 3);
                WebElementAction.ButtonClick(waitType, locatorType, languageLevelXpath, 3);
                WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Add']", 3);
            }
            else
            {
                Assert.Fail("Record limit exceeded");
            }
        }

        //Check for exeisting records in the grid
        public bool CheckforExistingRecord(string value, string levelValue)
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            string xPath = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table";

            IWebElement tableElement = WebElementAction.FindWebElement(locatorType, xPath);

            IList<IWebElement> tbodyCollection = tableElement.FindElements(By.TagName("tbody"));
            IList<IWebElement> trCollection;
            IList<IWebElement> tdCollection;

            //loop every row in the table and init the columns to list
            foreach (IWebElement tbodyElement in tbodyCollection)
            {
                trCollection = tbodyElement.FindElements(By.TagName("tr"));

                foreach (IWebElement trElement in trCollection)
                {
                    tdCollection = trElement.FindElements(By.TagName("td"));
                    if (tdCollection[0].Text == value && tdCollection[1].Text == levelValue)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        //Get exisiting record count for a given language 
        public int GetExistingRecordCount(string value)
        {
            int recordCount = 0;
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            string xPath = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table";

            IWebElement tableElement = WebElementAction.FindWebElement(locatorType, xPath);

            IList<IWebElement> tbodyCollection = tableElement.FindElements(By.TagName("tbody"));
            IList<IWebElement> trCollection;
            IList<IWebElement> tdCollection;

            //loop every row in the table and init the columns to list
            foreach (IWebElement tbodyElement in tbodyCollection)
            {
                trCollection = tbodyElement.FindElements(By.TagName("tr"));

                foreach (IWebElement trElement in trCollection)
                {
                    tdCollection = trElement.FindElements(By.TagName("td"));
                    if (tdCollection[0].Text == value)
                    {
                        recordCount = recordCount+1;
                    }
                }
            }
            return recordCount;
        }

        //Get record count in the grid
        public int GetRecordCount(string value)
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            string xPath = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table";

            IWebElement tableElement = WebElementAction.FindWebElement(locatorType, xPath);

            IList<IWebElement> tbodyCollection = tableElement.FindElements(By.TagName("tbody"));

            return tbodyCollection.Count;
        }

        //Common method to return Edit or Delete button depending on the parameter
        public IWebElement GetEditDeletButtonElement(bool isEdit, bool isLanguage, string value, string levelValue)
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            string xPath = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table";

            IWebElement tableElement = WebElementAction.FindWebElement(locatorType, xPath);

            IList<IWebElement> tbodyCollection = tableElement.FindElements(By.TagName("tbody"));
            IList<IWebElement> trCollection;
            IList<IWebElement> tdCollection;
            IList<IWebElement> spanCollection;

            //loop every row in the table and init the columns to list
            foreach (IWebElement tbodyElement in tbodyCollection)
            {
                trCollection = tbodyElement.FindElements(By.TagName("tr"));

                foreach (IWebElement trElement in trCollection)
                {
                    tdCollection = trElement.FindElements(By.TagName("td"));
                    if (tdCollection[0].Text == value && tdCollection[1].Text == levelValue)
                    {
                        spanCollection = tdCollection[2].FindElements(By.TagName("span"));
                        if (isEdit) return spanCollection[0];
                        else return spanCollection[1];
                    }
                }
            }
            return null;
        }

        //Click on Language Tab
        public void GoToLanguageTab()
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            WebElementAction.WaitType waitType = new WebElementAction.WaitType();
            waitType.ToBeClickable = true;

            WebElementAction.ButtonClick(waitType, locatorType, "//a[contains(text(),'Languages')]", 3);
        }

        //Check wheather the "Add New" button is visible
        public bool IsAddNewLanguageBtnVisible()
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            try
            {
                IWebElement addNewLanguageButton = WebElementAction.FindWebElement(locatorType, "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div");
                
                return true; 
            }
            catch(Exception exception)
            {
                return false;
            }
        }

        //Check wheather the "Cancel" button is visible
        public bool IsCancelLanguageBtnVisible()
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            try
            {
                IWebElement cancelLanguageButton = WebElementAction.FindWebElement(locatorType, "//input[@type='button' and @value='Cancel']");

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        //Edit Language Record
        public void EditRecord(string oldLanguage, string oldLanguageLevel, string newLanguage, string newLanguageLevel)
        {
            string languageLevelXpath = "//option[contains(text(),'" + newLanguageLevel + "')]";
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            WebElementAction.WaitType waitType = new WebElementAction.WaitType();
            waitType.ToBeVisible = true;

            //If Cancel button is visible, click on it
            if (IsCancelLanguageBtnVisible()) WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Cancel']", 3);

            GetEditDeletButtonElement(true,true, oldLanguage, oldLanguageLevel).Click();

            WebElementAction.SendKeys(waitType, locatorType, "//input[@type='text' and @placeholder='Add Language']", 3, newLanguage);
            WebElementAction.ButtonClick(waitType, locatorType, "//select", 3);
            WebElementAction.ButtonClick(waitType, locatorType, languageLevelXpath, 3);
            WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Update']", 3);
        }

        //Delete Language Record
        public void DeletRecord(string language, string languageLevel)
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            WebElementAction.WaitType waitType = new WebElementAction.WaitType();
            waitType.ToBeVisible = true;

            //If Cancel button is visible, click on it
            if (IsCancelLanguageBtnVisible()) WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Cancel']", 3);

            GetEditDeletButtonElement(false, true, language, languageLevel).Click();
        }

    }
}
