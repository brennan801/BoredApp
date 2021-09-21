Feature: Sample
	Simple calculator for adding two numbers

@mytag
Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the sum should be 120

Scenario: Multiply two numbers
	Given the first number is 5
	And the second number is 4
	When the two numbers are multiplied
	Then the product should be 20