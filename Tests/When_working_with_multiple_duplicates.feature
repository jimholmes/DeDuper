Feature: When_working_with_multiple_duplicates
	In order to get rid of duplicates in a directory
	I want to save ONLY files in the pattern '01 - '

@mytag
Scenario: Remove one duplicate 
	Given The list has '01 foo.mp3' 
	And  The list has '01 - foo.mp3'
	When I select files for deletion
	Then the result should contain '01 foo.mp3'

Scenario: Remove two different dupes
	Given The list has '01 foo.mp3'
	And The list has '01 - foo.mp3'
	And The list has '02 bar.mp3'
	And The list has '02 - bar.mp3'
	When I select files for deletion
	Then the result should contain '01 foo.mp3'
	And the result should contain '02 bar.mp3'

Scenario: Remove dupe songs with no numeric prefix
	Given The list has '01 - foo.mp3'
	And The list has '01 foo.mp3'
	And The list has 'foo.mp3'
	When I select files for deletion
	Then the result should contain '01 foo.mp3'
	And the result should contain 'foo.mp3'

Scenario: A non-numeric song by itself should not end up on delete list
	Given The list has 'foo.mp3'
	When I select files for deletion
	Then the result should not contain 'foo.mp3'

Scenario: A numeric song by itself should not end up on delete list
	Given The list has '01 - foo.mp3'
	When I select files for deletion
	Then the result should not contain '01 - foo.mp3'

