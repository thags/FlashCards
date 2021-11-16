# FlashCards

# Requirements
  ### Part 1: 
  - You should be able to create stacks of flashcards.
  - You should be able to change the stack's name and delete stack
  - You should be able to create, update, delete flashcards
  - All flashcards need to be part of at least one stack
  - If you delete a stack, all flashcards will be deleted
  - SQLite isn't allowed. You have to use SQL Server.
  
  ### Part 2: 
  - You should have a Study area
  - In the study area you should be able to loop through the flashcards in a stack and get an "answer" input for each question.
  - You should store your "Study Sessions" and be able to retrieve them.
  
# Technical Considerations
  - You need to look into "One to many" relationships in SQL, where you'll use a foreign key to link the "stacks" and "flashcards"
  - You need to have classes with clear separation of concerns: UserInput.cs, TableVisualisationEngine.cs
  - You need to map your data into "Models". Example: Once you get your stack from the database, it will be stored into a List<Stack>()
  - Connection strings and databasepaths need to be in a web.config file. 
