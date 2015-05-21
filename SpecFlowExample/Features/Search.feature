Feature: Site search
	Each user should be able to quickly find information on the site.
	To do this, he can always use the search function implemented in the header.	

@search
Scenario: Add two numbers
	Given User open global web site
	And fill Search field with query "Languages and Dictionaries"
	When User press Search button
	Then the search results should be on the screen
