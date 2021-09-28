Feature: IndexTest
	Testing functionality of the index page

@mytag
Scenario: Index button is clicked
	Given I make a generic call
	When the index button is clicked
	Then the activity should be a valid activity

Scenario Outline: Different calls are sent to bored api
	Given I make a specified call
	And the activity type is: <type>
	And the number of participants is: <participants>
	And the price is: <price>
	When the specifed button is clicked
	Then I should send a request with: <sentType> , <sentParticipants>, and <sentPrice>

	Examples: 
	| type        | participants | price | sentType    | sentParticipants | sentPrice |
	| educational | null         | null  | educational | null             | null      |
	|             | 4            | null  |             | 4                | null      |
	|             | null         | .05   |             | null             | .05       |
	| social      | 2            | .2    | social      | 2                | .2        |

