﻿Feature: Site search
	Each user should be able to quickly find information on the site.
	To do this, he can always use the search function implemented in the header.	

@search
Scenario Outline: Search
	Given User open ABBYY web site
	And fill Search field with query <query>
	When User press Search button
	Then the search results should be on the screen

Examples: 
	| query                         |
	| "Languages and Dictionaries"  |
	| "~!@#$%^&*()_+/*-+:";'\<>?~`" |
