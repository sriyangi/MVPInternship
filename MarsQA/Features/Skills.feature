@Skills
Feature: This test suit contains test cases related to skills

Scenario: A. Create a new Skill Record
Given I navigate to Skills Tab
When I create a new Skill Record 'Writing' 'Beginner'
Then Skill Record should be saved 'Writing' 'Beginner'

Scenario: B. Duplicate a Skill Record
Given I navigate to Skills Tab
When I create a new Skill Record 'Reading' 'Expert'
And I duplicate Skill Record 'Reading' 'Expert'
Then Duplicate Skill record should not be saved 'Reading'

Scenario Outline: C. Create a Skill Record with incomplete data
Given I navigate to Skills Tab
When I create a Skill Record with incomplete data <skill> <skill type>
Then Skill Record should not be saved <skill> <skill type>

Examples: 
| skill		| skill type	|
| 'Cooking' | ''			|
| ''		| 'Beginner'    |
| ''		| ''			|

Scenario Outline: D. Edit a Skill Record
Given I navigate to Skills Tab
When I edit skill record <oldSkill> <oldSkillType> <newSkill> <newSkillType>
Then Skill Record should be saved <newSkill> <newSkillType>

Examples: 
| oldSkill		| newSkill		| oldSkillType		| newSkillType |
| 'Writing'		| 'Cooking'     | 'Beginner'        | 'Expert'     |

Scenario Outline: E. Edit a Skill Record with existing values
Given I navigate to Skills Tab
When I create a new Skill Record <oldSkill> <oldSkillType>
And I edit skill record with exiting values <oldSkill> <oldSkillType> <newSkill> <newSkillType>
Then Duplicate Skill record should not be saved <newSkill> 

Examples: 
| oldSkill	| newSkill	| oldSkillType	| newSkillType |
| 'Dancing' | 'Cooking' | 'Beginner'	| 'Expert'     |

Scenario Outline: F. Edit a Skill Record with partial values
Given I navigate to Skills Tab
When I edit skill record with incomplete data <oldSkill> <oldSkillType> <newSkill> <newSkillType>
Then Skill Record should not be saved <newSkill> <newSkillType>

Examples: 
| oldSkill	| newSkill	| oldSkillType	| newSkillType |
| 'Dancing'	| ''        | 'Beginner'	| 'Expert'     |
| 'Cooking' | 'Speaking'| 'Expert'		| ''           |
| 'Reading' | ''        | 'Expert'		| ''           |

Scenario: H. Delete an existing Skill record
Given I navigate to Skills Tab
When I delete a Skill record 'Cooking' 'Expert'
Then Skill Record should be deleted 'Cooking' 'Expert'

Scenario Outline: I. Create a SKill Record with many characters
Given I navigate to Skills Tab
When I create a new Skill Record 'abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij' 'Expert'
Then Skill Record should be saved 'abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij' 'Expert'
