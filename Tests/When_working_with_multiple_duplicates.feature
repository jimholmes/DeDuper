Feature: When_working_with_multiple_duplicates
	In order to get rid of duplicates in a directory
	I want to save ONLY files in the pattern '01 - '

Scenario: No duplicates
	Given The list has '01 - foo.mp3'
	When I select files for deletion
	Then the result should contain nothing

Scenario: Remove one duplicate 
	Given The list has '01 foo.mp3' 
	And  The list has '01 - foo.mp3'
	When I select files for deletion
	Then the result should contain '01 foo.mp3'

Scenario: Remove a dupe with parens number
	Given The list has '01 - foo.mp3' 
	And  The list has '01 - foo(1).mp3'
	When I select files for deletion
	Then the result should contain '01 - foo(1).mp3'

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

Scenario: A dupe with "copy" is deleted
	Given The list has '01 - foo.mp3'
	And The list has '01 - foo - Copy.mp3'
	When I select files for deletion
	Then the result should contain '01 - foo - Copy.mp3'

Scenario: A non-numeric dupe with "copy" is deleted
	Given The list has 'foo.mp3'
	And The list has 'foo - Copy.mp3'
	When I select files for deletion
	Then the result should contain 'foo - Copy.mp3'

Scenario: A "copy" song by itself should not end up on delete list
	Given The list has '01 - foo - Copy.mp3'
	When I select files for deletion
	Then the result should not contain '01 - foo - Copy.mp3'

Scenario: Remove dupes with numerics, no numerics, parens numbers, and numeric plus 'Copy'
	Given The list has '01 - foo.mp3'
	And The list has '01 foo.mp3'
	And The list has '01 foo(1).mp3'
	And The list has 'foo - Copy.mp3'
	And The list has 'foo.mp3'
	And The list has 'foo(2).mp3'
	And The list has '01 - foo - Copy.mp3'
	When I select files for deletion
	Then the result should contain '01 foo.mp3'
	Then the result should contain '01 foo(1).mp3'
	And the result should contain '01 - foo - Copy.mp3'
	And the result should contain 'foo - Copy.mp3'
	And the result should contain 'foo.mp3'
	And the result should contain 'foo(2).mp3'

Scenario: Dupe prefixes on different songs
	Given The list has '01 - foo.mp3'
	And The list has '01 - bar.mp3'
	And The list has '02 foobar.mp3'
	And The list has '02 barfoo.mp3'
	When I select files for deletion
	Then the result should contain nothing

Scenario: No dupes, but not preferred prefix
	Given The list has '01 foo.mp3'
	And The list has '02 bar.mp3'
	When I select files for deletion
	Then the result should contain nothing
	And No exception should be generated

Scenario: Handle three digit prefixes
	Given The list has '111 - bar.mp3'
	When I select files for deletion
	Then the result should contain nothing

Scenario: Handle repeating prefix
	Given The list has '01 - foo.mp3'
	And The list has '01-01 - foo.mp3'
	When I select files for deletion

Scenario: Non-preferred numeric prefix and alpha dupe leaves numeric
	Given The list has '01 foo.mp3'
	And The list has 'foo.mp3'
	When I select files for deletion
	Then the result should contain 'foo.mp3'

Scenario: Differing prefix and file type
	Given The list has '01 - foo.m4a'
	And The list has 'foo.m4p'
	When I select files for deletion
	Then the result should contain '01 - foo.m4a'