﻿Feature: When_working_with_multiple_duplicates
	In order to get rid of duplicates in a directory
	I want to save ONLY files in the pattern '01 - '

@mytag
Scenario: Remove one duplicate 
	Given The list has '01 foo.mp3' and '01 - foo.mp3'
	When I select files for deletion
	Then the result should contain '01 - foo.mp3'

Scenario: Remove two different dupes
	Given The list has '01 foo.mp3', '01 - foo.mp3', '02 bar', and '02 - bar.mp3'
	When I select files for deletion
	Then the result should contain '01 - foo.mp3'
	And the result should contain '02 - bar.mp3'