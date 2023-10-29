using MarsQA.Pages;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace MarsQA.StepDefinitions
{
    [Binding]
    public class SkillsStepDefinitions
    {
        SkillsPage skillsPage = new SkillsPage();
        LanguagesPage languagesPage = new LanguagesPage();

        [Given(@"I navigate to Skills Tab")]
        public void GivenINavigateToSkillsTab()
        {
            skillsPage.GoToSkillTab();
        }

        [When(@"I create a new Skill Record '([^']*)' '([^']*)'")]
        public void WhenICreateANewSkillRecord(string skill, string skillType)
        {
            if (!languagesPage.CheckforExistingRecord(skill, skillType))
            {
                skillsPage.CreateRecord(skill, skillType);
            }
            else
            {
                Assert.Fail("SKill Record: " + skill + " already exists");
            }
            //Driver.TurnOnWait();
            Thread.Sleep(3000);
        }

        [Then(@"Skill Record should be saved '([^']*)' '([^']*)'")]
        public void ThenSkillRecordShouldBeSaved(string skill, string skillType)
        {
            Assert.That(skillsPage.CheckforExistingRecord(skill, skillType), "Skill Record: " + skill + " not created");
        }

        [When(@"I duplicate Skill Record '([^']*)' '([^']*)'")]
        public void WhenIDuplicateSkillRecord(string skill, string skillType)
        {
            skillsPage.CreateRecord(skill, skillType);
            Thread.Sleep(3000);
        }

        [Then(@"Duplicate Skill record should not be saved '([^']*)'")]
        public void ThenDuplicateSkillRecordShouldNotBeSaved(string skill)
        {
            Assert.That(skillsPage.GetExistingRecordCount(skill) == 1, "Skill Record: " + skill + " duplicated");
        }

        [When(@"I create a Skill Record with incomplete data '([^']*)' '([^']*)'")]
        public void WhenICreateASkillRecordWithIncompleteData(string skill, string skillType)
        {
            skillsPage.CreateRecord(skill, skillType);
            //Driver.TurnOnWait();
            Thread.Sleep(3000);
        }

        [Then(@"Skill Record should not be saved '([^']*)' '([^']*)'")]
        public void ThenSkillRecordShouldNotBeSaved(string skill, string skillType)
        {
            Assert.That(!skillsPage.CheckforExistingRecord(skill, skillType), "Skill Record: " + skill + " saved with incorrect data");
        }

        [When(@"I edit skill record '([^']*)' '([^']*)' '([^']*)' '([^']*)'")]
        public void WhenIEditSkillRecord(string oldSkill, string oldSkillType, string newSkill, string newSkillType)
        {
            if (skillsPage.CheckforExistingRecord(oldSkill, oldSkillType))
            {
                skillsPage.EditRecord(oldSkill, oldSkillType, newSkill, newSkillType);
                Thread.Sleep(3000);
            }
            else
            {
                Assert.Fail("Skill Record: " + oldSkill + " do not exist.");
            }
        }

        [When(@"I edit skill record with exiting values '([^']*)' '([^']*)' '([^']*)' '([^']*)'")]
        public void WhenIEditSkillRecordWithExitingValues(string oldSkill, string oldSkillType, string newSkill, string newSkillType)
        {
            if (skillsPage.CheckforExistingRecord(oldSkill, oldSkillType))
            {
                skillsPage.EditRecord(oldSkill, oldSkillType, newSkill, newSkillType);
                //Driver.TurnOnWait();
                Thread.Sleep(3000);
            }
            else
            {
                Assert.Fail("Skill Record: " + oldSkill + " do not exist.");
            }
        }

        [When(@"I edit skill record with incomplete data '([^']*)' '([^']*)' '([^']*)' '([^']*)'")]
        public void WhenIEditSkillRecordWithIncompleteData(string oldSkill, string oldSkillType, string newSkill, string newSkillType)
        {
            if (skillsPage.CheckforExistingRecord(oldSkill, oldSkillType))
            {
                skillsPage.EditRecord(oldSkill, oldSkillType, newSkill, newSkillType);
                Thread.Sleep(3000);
            }
            else
            {
                Assert.Fail("Skill Record: " + oldSkill + " do not exist.");
            }
        }

        [When(@"I delete a Skill record '([^']*)' '([^']*)'")]
        public void WhenIDeleteASkillRecord(string skill, string skillType)
        {
            if (skillsPage.CheckforExistingRecord(skill, skillType))
            {
                skillsPage.DeletRecord(skill, skillType);
                Thread.Sleep(3000);
            }
            else
            {
                Assert.Fail("Skill Record: " + skill + " do not exist.");
            }
        }

        [Then(@"Skill Record should be deleted '([^']*)' '([^']*)'")]
        public void ThenSkillRecordShouldBeDeleted(string skill, string skillType)
        {
            Assert.That(!skillsPage.CheckforExistingRecord(skill, skillType), "Skill Record: " + skill + " still exists");
        }


    }
}
