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

namespace MarsQA.Pages
{
    public class LanguagesPage: Driver
    {
        private IWebElement languageTab;
        private IWebElement languageTable;
        private IWebElement addNewButton;
        private IWebElement languageTextbox;
        private IWebElement languageLevelDropdownSelect;
        private IWebElement languageLevelDropdown;
        private IWebElement languageAddButton;
        private IWebElement languageUpdateButton;
        private IWebElement languageCancelButton;
        private IWebElement messageWindow;
        private IWebElement closeMessageIcon;

        #region Render Elements

        public void renderAddElements()
        {
            languageTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Add Language']"));
            languageLevelDropdownSelect = driver.FindElement(By.XPath("//select"));
            languageAddButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Add']"));
            languageCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));
        }

        public void renderEditlements()
        {
            languageTextbox = driver.FindElement(By.XPath("//input[@type='text' and @placeholder='Add Language']"));
            languageLevelDropdownSelect = driver.FindElement(By.XPath("//select"));
            languageUpdateButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Update']"));
            languageCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));
        }

        #endregion

        #region Page Data Manipulation

        //Clear State
        public void DeleteAllRecords()
        {
            IList<IWebElement> tbodyCollection = languageTable.FindElements(By.TagName("tbody"));
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

        //Create Language Record
        public void CreateRecord(string skill, string skillLevel)
        {
            string skillLevelXpath = "//option[contains(text(),'" + skillLevel + "')]";

            //If Cancel button is visible, first click it
            if (IsCancelLanguageBtnVisible()) languageCancelButton.Click();

            if (IsAddNewLanguageBtnVisible())
            {
                addNewButton.Click();
                renderAddElements();

                languageTextbox.SendKeys(skill);
                languageLevelDropdownSelect.Click();
                languageLevelDropdown = driver.FindElement(By.XPath(skillLevelXpath));
                languageLevelDropdown.Click();
                languageAddButton.Click();
            }
            else
            {
                Assert.Fail("Record limit exceeded");
            }
        }

        //Edit Language Record
        public void EditRecord(string oldLanguage, string oldLanguageLevel, string newLanguage, string newLanguageLevel)
        {
            string languageLevelXpath = "//option[contains(text(),'" + newLanguageLevel + "')]";

            //If Cancel button is visible, first click it
            if (IsCancelLanguageBtnVisible()) languageCancelButton.Click();

            GetEditDeletButtonElement(true, true, oldLanguage, oldLanguageLevel).Click();
            renderEditlements();

            languageTextbox.Clear();
            languageTextbox.SendKeys(newLanguage);
            languageLevelDropdownSelect.Click();
            languageLevelDropdown = driver.FindElement(By.XPath(languageLevelXpath));
            languageLevelDropdown.Click();
            languageUpdateButton.Click();
        }

        //Delete Language Record
        public void DeletRecord(string language, string languageLevel)
        {
            //If Cancel button is visible, click on it
            if (IsCancelLanguageBtnVisible()) languageCancelButton.Click();

            GetEditDeletButtonElement(false, true, language, languageLevel).Click();
        }

        #endregion

        #region Supporting Methods

        //Check for exeisting records in the grid
        public bool CheckforExistingRecord(string value, string levelValue)
        {
            IList<IWebElement> tbodyCollection = languageTable.FindElements(By.TagName("tbody"));
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

        //Get record count in the grid
        public int GetRecordCount()
        {
            IList<IWebElement> tbodyCollection = languageTable.FindElements(By.TagName("tbody"));

            return tbodyCollection.Count;
        }

        //Common method to return Edit or Delete button depending on the parameter
        public IWebElement GetEditDeletButtonElement(bool isEdit, bool isLanguage, string value, string levelValue)
        {
            IList<IWebElement> tbodyCollection = languageTable.FindElements(By.TagName("tbody"));
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
            languageTab = driver.FindElement(By.XPath("//a[contains(text(),'Languages')]"));
            languageTab.Click();
            languageTable = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table"));

            if (IsAddNewLanguageBtnVisible())
            {
                addNewButton = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div"));
            }
        }

        //Check wheather the "Add New" button is visible
        public bool IsAddNewLanguageBtnVisible()
        {
            try
            {
                languageCancelButton = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div"));

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        //Check wheather the "Cancel" button is visible
        public bool IsCancelLanguageBtnVisible()
        {
            try
            {
                languageCancelButton = driver.FindElement(By.XPath("//input[@type='button' and @value='Cancel']"));

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
        #endregion

        #region Assertions

        public void AssertInsertedRecord(string language)
        {
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelLanguageBtnVisible())
            {
                languageCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo(language + " has been added to your languages"), "succes message is not correct for add language");
        }

        public void AssertDuplicatedRecord(string language)
        {
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelLanguageBtnVisible())
            {
                languageCancelButton.Click();
            }

            Assert.That(message == "This language is already exist in your language list." || message == "Duplicated data" || message == "This language is already added to your language list.", "succes message is not correct for duplicated language");
        }

        public void AssertUpdatedRecord(string language)
        {
            //Please enter language and level

            Wait.WaitToBeExists(LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));

            string message = messageWindow.Text;

            //Wait.WaitToBeExists(LocatorType.XPath, "//*[@class='ns-close']", 3);
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelLanguageBtnVisible())
            {
                languageCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo(language + " has been updated to your languages"), "succes message is not correct for update language");
        }

        public void AssertIncompleteRecord(string language)
        {
            Wait.WaitToBeExists(LocatorType.XPath, "//div[@class='ns-box-inner']", 3);
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));

            string message = messageWindow.Text;

            //Wait.WaitToBeExists(LocatorType.XPath, "//*[@class='ns-close']", 3);
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelLanguageBtnVisible())
            {
                languageCancelButton.Click();
            }

            Assert.That(message, Is.EqualTo("Please enter language and level"), "succes message is not correct for not language record with partial data");
        }

        public void AssertDeletedRecord(string language)
        {
            messageWindow = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            closeMessageIcon = driver.FindElement(By.XPath("//*[@class='ns-close']"));

            string message = messageWindow.Text;

            //If any message visible close it
            closeMessageIcon.Click();

            if (IsCancelLanguageBtnVisible())
            {
                languageCancelButton.Click();
            }
            Assert.That(message, Is.EqualTo(language + " has been deleted from your languages"), "succes message is not correct for delete language");
        }

        public void AssertEmptySkills()
        {
            Assert.That(GetRecordCount() != 0, "succes message is not correct for delete all languages");
        }

        #endregion

    }
}
