# CsFuzz
A command-line tool inspired by grep for searching for files directories or text within files using fuzzy string matching, built in C#. The tool runs concurrently for efficient search results.

- made a simple algorithm to fuzzily match strings 
- works like linux grep and helps to search for files/directories 



# Usage
```
CsFuzz is a fuzzy finder made in Csharp

Usage:

	CsFuzz <args> -[t|f|d] string -D [directory|file name] 

options :

    -t 	Search for text in file ( used with file path)
	
    -T 	Search for text in every file in directory 
	
    -f 	Search for file in directory and sub directorys
	
    -d 	Search for directory in directory and sub directorys
	
    -D 	Path to directory or . for current directory

args:	
	
    -s 	Case sensitive search 
```
