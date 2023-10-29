using MarsQA.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.Pages
{
    public class SkillsPage
    {
        //Create Skills Record
        public void CreateRecord(string skill, string skillLevel)
        {
            string skillLevelXpath = "//option[contains(text(),'" + skillLevel + "')]";
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            WebElementAction.WaitType waitType = new()
            {
                ToBeVisible = true
            };

            //If Cancel button is visible, first click it
            if (IsCancelSkillBtnVisible()) WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Cancel']", 3);

            WebElementAction.ButtonClick(waitType, locatorType, "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/thead/tr/th[3]/div", 3);
            WebElementAction.SendKeys(waitType, locatorType, "//input[@type='text' and @placeholder='Add Skill']", 3, skill);
            WebElementAction.ButtonClick(waitType, locatorType, "//select", 3);
            WebElementAction.ButtonClick(waitType, locatorType, skillLevelXpath, 3);
            WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Add']", 3);
        }

        //Click Skills Tab
        public void GoToSkillTab()
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            WebElementAction.WaitType waitType = new WebElementAction.WaitType();
            waitType.ToBeClickable = true;

            WebElementAction.ButtonClick(waitType, locatorType, "//a[contains(text(),'Skills')]", 3);
        }

        //Common method to check cancel button is visible
        public bool IsCancelSkillBtnVisible()
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            try
            {
                IWebElement cancelSkillButton = WebElementAction.FindWebElement(locatorType, "//input[@type='button' and @value='Cancel']");

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        //Common method to check "Add New Skill" button is visible
        public bool IsAddNewSkillBtnVisible()
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            try
            {
                IWebElement addNewLanguageButton = WebElementAction.FindWebElement(locatorType, "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/thead/tr/th[3]/div");

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        //Check for exeisting records in the grid
        public bool CheckforExistingRecord(string value, string levelValue)
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            string xPath = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table";

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

        //Get exisiting record count for a given skill 
        public int GetExistingRecordCount(string value)
        {
            int recordCount = 0;
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            string xPath = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table";

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
                        recordCount = recordCount + 1;
                    }
                }
            }
            return recordCount;
        }

        //Common method to return Edit or Delete button depending on the parameter
        public IWebElement GetEditDeletButtonElement(bool isEdit, bool isLanguage, string value, string levelValue)
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            string xPath = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table";

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

        //Edit Skill Record
        public void EditRecord(string oldSKill, string oldSKillLevel, string newSKill, string newSKillLevel)
        {
            string sKillLevelXpath = "//option[contains(text(),'" + newSKillLevel + "')]";
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            WebElementAction.WaitType waitType = new WebElementAction.WaitType();
            waitType.ToBeVisible = true;

            //If Cancel button is visible, click on it
            if (IsCancelSkillBtnVisible()) WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Cancel']", 3);

            GetEditDeletButtonElement(true, true, oldSKill, oldSKillLevel).Click();

            WebElementAction.SendKeys(waitType, locatorType, "//input[@type='text' and @placeholder='Add Skill']", 3, newSKill);
            WebElementAction.ButtonClick(waitType, locatorType, "//select", 3);
            WebElementAction.ButtonClick(waitType, locatorType, sKillLevelXpath, 3);
            WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Update']", 3);
        }

        //Delete Skill Record
        public void DeletRecord(string skill, string skillLevel)
        {
            WebElementAction.LocatorType locatorType = new()
            {
                XPath = true
            };

            WebElementAction.WaitType waitType = new WebElementAction.WaitType();
            waitType.ToBeVisible = true;

            //If Cancel button is visible, click on it
            if (IsCancelSkillBtnVisible()) WebElementAction.ButtonClick(waitType, locatorType, "//input[@type='button' and @value='Cancel']", 3);

            GetEditDeletButtonElement(false, true, skill, skillLevel).Click();
        }
    }
}
