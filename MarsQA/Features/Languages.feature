@Languages
Feature: This test suit contains test cases related to languages and skills

Scenario: A. Create a new Language Record
Given I navigate to Languages Tab
When I create a new Language Record 'French' 'Basic'
Then Record should be saved 'French' 'Basic'

Scenario: B. Duplicate a Language Record
Given I navigate to Languages Tab
When I create a new Language Record 'Chinese' 'Native/Bilingual'
And I duplicate Language Record 'Chinese' 'Native/Bilingual'
Then Duplicate record should not be saved 'Chinese'

Scenario Outline: C. Create a Language Record with incomplete data
Given I navigate to Languages Tab
When I create a Language Record with incomplete data <language> <language type>
Then Record should not be saved <language> <language type>

Examples: 
| language   | language type  |
| 'English'  | ''			  |
| ''		 | 'Fluent'       |
| ''		 | ''			  |

Scenario Outline: D. Edit a Language Record
Given I navigate to Languages Tab
When I edit language record <oldLanguage> <oldlanguageType> <newLanguage> <newlanguageType>
Then Record should be saved <newLanguage> <newlanguageType>

Examples: 
| oldLanguage | newLanguage | oldlanguageType | newlanguageType |
| 'French'    | 'Hindi'     | 'Basic'         | 'Fluent'        |

Scenario Outline: E. Edit a Language Record with existing values
Given I navigate to Languages Tab
When I create a new Language Record <oldLanguage> <oldlanguageType>
And I edit language record with exiting values <oldLanguage> <oldlanguageType> <newLanguage> <newlanguageType>
Then Duplicate record should not be saved <newLanguage> 

Examples: 
| oldLanguage | newLanguage | oldlanguageType | newlanguageType |
| 'English'   | 'Hindi'     | 'Fluent'		  | 'Fluent'        |

Scenario Outline: F. Edit a Language Record with partial values
Given I navigate to Languages Tab
When I edit language record with incomplete data <oldLanguage> <oldlanguageType> <newLanguage> <newlanguageType>
Then Record should not be saved <newLanguage> <newlanguageType>

Examples: 
| oldLanguage | newLanguage | oldlanguageType	| newlanguageType |
| 'Chinese'   | ''          | 'Native/Bilingual'| 'Basic'         |
| 'English'   | 'Spanish'   | 'Fluent'			| ''              |
| 'Hindi'     | ''          | 'Fluent'			| ''              |

Scenario: G. Once the fourth language is created Add New button should be invisible
Given I navigate to Languages Tab
When I create the fourth Language Record 'Spanish' 'Basic'
Then Record should be saved 'Spanish' 'Basic'
And Add New button should be invisible

Scenario: H. Delete an existing language record
Given I navigate to Languages Tab
When I delete a language record 'Spanish' 'Basic'
Then Record should be deleted 'Spanish' 'Basic'

Scenario Outline: I. Create a Language Record with many characters
Given I navigate to Languages Tab
When I create a new Language Record 'abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij' 'Native/Bilingual'
Then Record should be saved 'abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij' 'Native/Bilingual'