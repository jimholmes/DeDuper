# DeDuper

Gets rid of duplicate songs in a directory tree. Or formats your hard drive. 
I never can remember which.
(Just kidding about the drive reformatting. Honest.)

This util is meant to remove dupe songs from a folder hierarchy. Somehow I 
ended up with a crapload of duplicated stuff in my music folder archive after
migrating from one system to another. This util is meant to clean that mess up.

# **WARNING**

Make a backup of your music directory **before** running this. You may notice
I did not say please. A backup is not optional.

## Usage

Point the exe at the root of the folder structure you want, fire it off.
  d:> DeDuper.exe d:\music_archives

**Note** DeDuper will process *all* files in the tree. It doesn't (currently)
try to figure out what's a music file and what's not. If you have two text 
files that match the algorithm one's going to be ground into bits and thrown to
the wolves. To mix bad metaphors.

## Examples

Preferred Prefix is two digits, a space, a dash, and another space, eg
"01 - " which plays out like "01 - foo.mp3". If other filenames are similar, 
the others will be deleted.

**Given**

	01 - foo.mp3
	01 - foo - Copy.mp3
	01 foo.mp3
	foo(1).mp3
	foo.mp3
	foo - Copy.mp3
	02 - bar.mp3
	02 bar.mp3
	bar.mp3

**Result**

	01 - foo.mp3
	02 - bar.mp3

Or you could like, go read the specs in the Test folder and see exactly what's going on!