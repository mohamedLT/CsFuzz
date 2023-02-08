# CsFuzz
A command-line tool inspired by grep for searching for files directories or text within files using fuzzy string matching, built in C#. The tool runs concurrently for efficient search results.

# Prerequisites
You will need to have the following software installed on your computer:

- .NET 7

# Installing
To install the project, follow these steps:

Clone the repository to your local machine

`git clone https://github.com/mohamedLT/CsFuzz.git`

Navigate to the project directory

`cd CsFuzz`

Run the following command to build the project

`dotnet build`

Run the following command to run the tool

`dotnet run`

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
# License
This project is licensed under the MIT license.
