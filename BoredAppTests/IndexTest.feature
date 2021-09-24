Feature: IndexTest
	Testing functionality of the index page

@mytag
Scenario: Index button is clicked
	Given the page is index
	And the index button is clicked
	Then the activity should be a valid activity
