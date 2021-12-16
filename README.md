# FlashCards

## Requirements
  ### Part 1: 
  - [x] You should be able to create stacks of flashcards.
  - [x] You should be able to change the stack's name and delete stack
  - [x] You should be able to create, update, delete flashcards
  - [x] All flashcards need to be part of at least one stack
  - [x] If you delete a stack, all flashcards will be deleted
  - [x] SQLite isn't allowed. You have to use SQL Server.
  
  ### Part 2: 
  - [x] You should have a Study area
  - [x] In the study area you should be able to loop through the flashcards in a stack and get an "answer" input for each question.
  - [x] You should store your "Study Sessions" and be able to retrieve them.
  
## Technical Considerations
  - [x] You need to look into "One to many" relationships in SQL, where you'll use a foreign key to link the "stacks" and "flashcards"
  - [x] You need to have classes with clear separation of concerns: UserInput.cs, TableVisualisationEngine.cs
  - [x] You need to map your data into "Models". Example: Once you get your stack from the database, it will be stored into a List<Stack>()
  - [x] Connection strings and databasepaths need to be in a web.config file. 

  ## Environment

- Visual Studio
- SQL server
- SQL server management studio

## Items learned

- Models
- XML config file
- SQL server management studio
    - Foreign key, one to many
- C# connection to SQL
- C# Debugger
- Connecting to SQL server through C#
- Creating a Database through C# code
- Creating DB tables through C# code
- Creating FK through C# code

## Packages used:

- [Configuration Manager](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager.connectionstrings?view=dotnet-plat-ext-6.0)
- [ConsoleTableExt](https://github.com/minhhungit/ConsoleTableExt)

## Helpful Resources

- The help and advice of my mentor [Cappuccinocodes](https://github.com/cappuccinocodes)
- [SQL one to many](https://www.youtube.com/watch?v=3grhQWDpFm0)
- [Foreign key in SQL management studio](https://www.youtube.com/watch?v=TpKcAmaaBts)
- [W3 schools SQL commands](https://www.w3schools.com/sql)
- [Setting up SQL connection in C#](https://www.guru99.com/c-sharp-class-object.html)
- [Separation of concerns](https://www.youtube.com/watch?v=0ZNIQOO2sfA)
- [Creating SQL Database through C# code](https://docs.microsoft.com/en-US/troubleshoot/dotnet/csharp/create-sql-server-database-programmatically)
- [Configuration Manager](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager.connectionstrings?view=dotnet-plat-ext-6.0)

## Mistakes made

- Going overboard with separation of concern and trying to divide everything into another class
- Creating messy looking code at the beginning - it makes it more difficult to make nice looking code later on.
- Made the web.config XML parsing a bit more difficult than it needed to be
- Not using DTO's earlier on and kind of forcing them on, instead of thiking about how to incorporate them better. 
