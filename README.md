WORD COUNT 

This is a simple console application that counts the occurrences of each word in a text file and prints the results to the console in descending order based on word count. It also provides unit tests for different components of the application.

- Functionalities
•	Count Words: Counts the occurrences of each word in a given text file.
•	Print to Console: Prints the word count to the console in descending order.
•	Logging: Uses Serilog for logging errors and exceptions.
•	File Handling: Allows creating, reading, writing, and deleting text files.
•	Functional Programming: Utilizes functional programming principles.

- Running the Application
1.	Clone the repository.
2.	Build and run the solution.

- Changing Text
•	You can change the text in the text.txt file to analyze different tests.

COMPONENTS
- Program.cs
• This file contains the main logic of the application.

- Interfaces
•	IFile: Defines methods for file operations.
•	IReport: Defines the method to count words in a file.
•	ISort: Defines the method to sort the word count dictionary.

- Services
•	FileService: Implements file operations.
•	ReportService: Implements word count functionality.
•	SortService: Implements sorting of the word count dictionary.

- Tests
•	FileServiceTests: Unit tests for FileService.
•	ReportServiceTests: Unit tests for ReportService.
•	SortServiceTests: Unit tests for SortService.

- Dependencies
•	Serilog: For logging errors and exceptions.
•	XUnit: For unit testing.
•	Moq: For mocking dependencies in unit tests.
•	FluentAssertions: For more readable assertions in unit tests.

- Example
Input
    Go do that thing that you do so well
Output
    3: do
    2: that
    1: go
    1: so
    1: thing
    1: well
    1: you

