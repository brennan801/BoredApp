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
	Then I should get a response with type of <returnedType>, participants of <returnedParticipants>, minPrice of <returnedMinPrice>, and maxPrice of <returnedMaxPrice>

	Examples: 
	| type       | participants | price  | returnedType | returnedParticipants | returnedMinPrice | returnedMaxPrice |
	| cooking    | 1            | low    | cooking      | 1                    | 0                | 0.4              |
	| diy        | 4            | medium | diy          | 4                    | 0.4              | 0.7              |
	| relaxation | 8            | high   | relaxation   | 8                    | 0.7              | 1                |
	| social     | 2            | low    | social       | 2                    | 0                | 0.4              |

Scenario Outline: A price is selected
	Given The selected price is <price>
	When the minPrice and maxPrice are computed
	Then the minPrice and maxPrice should be <minPrice>, and <maxPrice>

	Examples: 
	| price  | minPrice | maxPrice |
	| low    | 0        | .4       |
	| medium | .4       | .7       |
	| high   | .7       | 1        |

