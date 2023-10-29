using MarsQA.Helpers;
using MarsQA.Pages;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace MarsQA.StepDefinitions
{
    [Binding]
    public class LanguagesStepDefinitions
    {
        LanguagesPage languageSkillsPage = new LanguagesPage();

        [Given(@"I navigate to Languages Tab")]
        public void GivenINavigateToLanguagesTab()
        {
            languageSkillsPage.GoToLanguageTab();
        }

        [When(@"I create a new Language Record '([^']*)' '([^']*)'")]
        public void WhenICreateANewLanguageRecord(string language, string languageLevel)
        {
            if (!languageSkillsPage.CheckforExistingRecord(language, languageLevel))
            {
                languageSkillsPage.CreateRecord(language, languageLevel);
            }
            else
            {
                Assert.Fail("Lannguage Record: " + language + " already exists");
            }
            //Driver.TurnOnWait();
            Thread.Sleep(3000);
        }

        [When(@"I duplicate Language Record '([^']*)' '([^']*)'")]
        public void WhenIDuplicateLanguageRecord(string language, string languageLevel)
        {
            languageSkillsPage.CreateRecord(language, languageLevel);
            Thread.Sleep(3000);
        }


        [Then(@"Record should be saved '([^']*)' '([^']*)'")]
        public void ThenRecordShouldBeSaved(string language, string languageLevel)
        {
            Assert.That(languageSkillsPage.CheckforExistingRecord(language, languageLevel), "Lannguage Record: " + language + " not created");
        }

        [When(@"I create an existing Language Record '([^']*)' '([^']*)'")]
        public void WhenICreateAnExistingLanguageRecord(string language, string languageLevel)
        {
            languageSkillsPage.CreateRecord(language, languageLevel);
            //Driver.TurnOnWait();
            Thread.Sleep(3000);
        }

        [Then(@"Duplicate record should not be saved '([^']*)'")]
        public void ThenDuplicateRecordShouldNotBeSaved(string language)
        {
            Assert.That(languageSkillsPage.GetExistingRecordCount(language) == 1, "Lannguage Record: " + language + " duplicated");
        }

        [When(@"I create a Language Record with incomplete data '([^']*)' '([^']*)'")]
        public void WhenICreateALanguageRecordWithIncompleteData(string language, string languageLevel)
        {
            languageSkillsPage.CreateRecord(language, languageLevel);
            //Driver.TurnOnWait();
            Thread.Sleep(3000);
        }

        [Then(@"Record should not be saved '([^']*)' '([^']*)'")]
        public void ThenRecordShouldNotBeSaved(string language, string languageLevel)
        {
            Assert.That(!languageSkillsPage.CheckforExistingRecord(language, languageLevel), "Lannguage Record: " + language + " saved with incorrect data");
        }

        [When(@"I create the fourth Language Record '([^']*)' '([^']*)'")]
        public void WhenICreateTheFourthLanguageRecord(string language, string languageLevel)
        {
            if (languageSkillsPage.GetRecordCount(language) < 4)
            {
                if (!languageSkillsPage.CheckforExistingRecord(language, languageLevel))
                {
                    languageSkillsPage.CreateRecord(language, languageLevel);
                    Thread.Sleep(3000);
                }
            }
        }

        [Then(@"Add New button should be invisible")]
        public void ThenAddNewButtonShouldBeInvisible()
        {
            Assert.That(!languageSkillsPage.IsAddNewLanguageBtnVisible(), "Add New Language Button is visible");
        }

        [When(@"I edit language record '([^']*)' '([^']*)' '([^']*)' '([^']*)'")]
        public void WhenIEditLanguageRecord(string oldlanguage, string oldlanguageLevel, string newlanguage, string newlanguageLevel)
        {
            if (languageSkillsPage.CheckforExistingRecord(oldlanguage, oldlanguageLevel))
            {
                languageSkillsPage.EditRecord(oldlanguage,oldlanguageLevel,newlanguage,newlanguageLevel);
                Thread.Sleep(3000);
            }
            else
            {
                Assert.Fail("Lannguage Record: " + oldlanguage + " do not exist.");
            }            
        }

        [When(@"I edit language record with exiting values '([^']*)' '([^']*)' '([^']*)' '([^']*)'")]
        public void WhenIEditLanguageRecordWithExitingValues(string oldlanguage, string oldlanguageLevel, string newlanguage, string newlanguageLevel)
        {
            if (languageSkillsPage.CheckforExistingRecord(oldlanguage, oldlanguageLevel))
            {
                languageSkillsPage.EditRecord(oldlanguage,oldlanguageLevel, newlanguage,newlanguageLevel);
                //Driver.TurnOnWait();
                Thread.Sleep(3000);
            }
            else
            {
                Assert.Fail("Lannguage Record: " + oldlanguage + " do not exist.");
            }
        }

        [When(@"I edit language record with incomplete data '([^']*)' '([^']*)' '([^']*)' '([^']*)'")]
        public void WhenIEditLanguageRecordWithIncompleteData(string oldlanguage, string oldlanguageLevel, string newlanguage, string newlanguageLevel)
        {
            if (languageSkillsPage.CheckforExistingRecord(oldlanguage, oldlanguageLevel))
            {
                languageSkillsPage.EditRecord(oldlanguage, oldlanguageLevel, newlanguage, newlanguageLevel);
                Thread.Sleep(3000);
            }
            else
            {
                Assert.Fail("Lannguage Record: " + oldlanguage + " do not exist.");
            }
        }

        [When(@"I delete a language record '([^']*)' '([^']*)'")]
        public void WhenIDeleteALanguageRecord(string language, string languageLevel)
        {
            if (languageSkillsPage.CheckforExistingRecord(language, languageLevel))
            {
                languageSkillsPage.DeletRecord(language, languageLevel);
                Thread.Sleep(3000);
            }
            else
            {
                Assert.Fail("Lannguage Record: " + language + " do not exist.");
            }
        }

        [Then(@"Record should be deleted '([^']*)' '([^']*)'")]
        public void ThenRecordShouldBeDeleted(string language, string languageLevel)
        {
            Assert.That(!languageSkillsPage.CheckforExistingRecord(language, languageLevel), "Lannguage Record: " + language + " still exists");
        }


    }
}
