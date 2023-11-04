using MarsQA.Helpers;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.CommonModels;
using static MarsQA.Helpers.Wait;

namespace MarsQA.Pages
{
    public class SkillsPage: Driver
    {
        private IWebElement skillTab;
        private IWebElement skillTable;
        private IWebElement addNewButton;
        private IWebElement skillTextbox;
        private IWebElement skillLevelDropdownSelect;
        private IWebElement skillLevelDropdown;
        private IWebElement skillAddButton;
        private IWebElement skillUpdateButton;
        private IWebElement skillCancelButton;
        private IWebElement messageWindow;
        private IWebElement closeMessageIcon;

        #region Render Elements
        public void renderAddElements()
        {
            skillTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Add Skill']"));
            skillLevelDropdownSelect = driver.FindElement(By.XPath("//select"));
            skillAddButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Add']"));
            skillCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));
        }

        public void renderEditlements()
        {
            skillTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Add Skill']"));
            skillLevelDropdownSelect = driver.FindElement(By.XPath("//select"));
            skillUpdateButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Update']"));
            skillCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));
        }
        #endregion

        #region Page Data Manipulation

        //Clear State
        public void DeleteAllRecords()
        {
            IList<IWebElement> tbodyCollection = skillTable.FindElements(By.TagName("tbody"));
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
                    spanCollection = tdCollection[2].FindElements(By.TagName("span"));
                    spanCollection[1].Click();
                }
            }
        }

        //Create Skills Record
        public void CreateRecord(string skill, string skillLevel)
        {
            string skillLevelXpath = "//option[contains(text(),'" + skillLevel + "')]";

            //If Cancel button is visible, first click it
            if (IsCancelSkillBtnVisible()) skillCancelButton.Click();

            addNewButton.Click();
            renderAddElements();

            skillTextbox.SendKeys(skill);
            skillLevelDropdownSelect.Click();
            skillLevelDropdown = driver.FindElement(By.XPath(skillLevelXpath));
            skillLevelDropdown.Click();
            skillAddButton.Click();
        }

        //Edit Skill Record
        public void EditRecord(string oldSKill, string oldSKillLevel, string newSKill, string newSKillLevel)
        {
            string skillLevelXpath = "//option[contains(text(),'" + newSKillLevel + "')]";

            //If Cancel button is visible, first click it
            if (IsCancelSkillBtnVisible()) skillCancelButton.Click();

            GetEditDeletButtonElement(true, true, oldSKill, oldSKillLevel).Click();
            renderEditlements();

            skillTextbox.Clear();
            skillTextbox.SendKeys(newSKill);
            skillLevelDropdownSelect.Click();
            skillLevelDropdown = driver.FindElement(By.XPath(skillLevelXpath));
            skillLevelDropdown.Click();
            skillUpdateButton.Click();
        }

        //Delete Skill Record
        public void DeletRecord(string skill, string skillLevel)
        {

            //If Cancel button is visible, click on it
            if (IsCancelSkillBtnVisible()) skillCancelButton.Click();

            GetEditDeletButtonElement(false, true, skill, skillLevel).Click();
        }

        #endregion

        #region Supporting Methods

        //Click Skills Tab
        public void GoToSkillTab()
        {
            skillTab = driver.FindElement(By.XPath("//a[contains(text(),'Skills')]"));
            skillTab.Click();
            skillTable = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table"));
            addNewButton = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/thead/tr/th[3]/div"));
        }

        //Common method to check cancel button is visible
        public bool IsCancelSkillBtnVisible()
        {
            try
            {
                skillCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool CheckforExistingRecord(string value, string levelValue)
        {
            IList<IWebElement> tbodyCollection = skillTable.FindElements(By.TagName("tbody"));
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

        //Common method to return Edit or Delete button depending on the parameter
        public IWebElement GetEditDeletButtonElement(bool isEdit, bool isLanguage, string value, string levelValue)
        {
            IList<IWebElement> tbodyCollection = skillTable.FindElements(By.TagName("tbody"));
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

        //Get record count in the grid
        public int GetRecordCount()
        {
            IList<IWebElement> tbodyCollection = skillTable.FindElements(By.TagName("tbody"));
            return tbodyCollection.Count;
        }

        #endregion

        #region Assertions

        public void AssertInsertedRecord(string skill)
        {
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelSkillBtnVisible())
            {
                skillCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo(skill + " has been added to your skills"), "succes message is not correct for add skill");
        }

        public void AssertDuplicatedRecord(string skill)
        {
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelSkillBtnVisible())
            {
                skillCancelButton.Click();
            }

            Assert.That(message == "This skill is already exist in your skill list." || message == "Duplicated data" || message == "This skill is already added to your skill list.", "succes message is not correct for duplicated skill");
        }

        public void AssertUpdatedRecord(string skill)
        {
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelSkillBtnVisible())
            {
                skillCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo(skill + " has been updated to your skills"), "succes message is not correct for update skill");
        }

        public void AssertIncompleteRecord(string skill)
        {
            Wait.WaitToBeExists(LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));

            string message = messageWindow.Text;

            //Wait.WaitToBeExists(LocatorType.XPath, "//*[@class='ns-close']", 3);
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelSkillBtnVisible())
            {
                skillCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo("Please enter skill and experience level"), "succes message is not correct for not skill record with partial data");
        }

        public void AssertDeletedRecord(string skill)
        {
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelSkillBtnVisible())
            {
                skillCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo(skill + " has been deleted"), "succes message is not correct for delete skill");
        }

        public void AssertEmptySkills()
        {
            Assert.That(GetRecordCount()!=0, "succes message is not correct for delete all skills");
        }

        #endregion
    }
}
