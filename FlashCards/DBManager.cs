using System;
using System.Configuration;
using System.Data.SqlClient;

namespace FlashCards
{
    class DBManager
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        public static void CreateDatabase()
        {

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "CREATE DATABASE flashcards";
                        try
                        {
                            command.ExecuteNonQuery();
                            Console.WriteLine("DataBase is Created Successfully");
                        }
                        catch (System.Exception ex)
                        {
                            if (ex.HResult == -2146232060)
                            {
                                Console.WriteLine("Database Exists!");
                            }
                            else
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        finally
                        {
                            command.Dispose();
                            connection.Dispose();
                        }
                }
            }
        }

        public static void CreateStackTable()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $@"CREATE TABLE [dbo].[Stacks](
                        [Id][int] IDENTITY(1, 1) NOT NULL,
                        [Name] [nvarchar](max)NOT NULL,
                        CONSTRAINT[PK_Stacks] PRIMARY KEY CLUSTERED([Id] ASC))";
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Stacks table created Successfully");
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Message == "There is already an object named 'Stacks' in the database.")
                        {
                            Console.WriteLine("Stacks Table already existed!");
                        }
                        else
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Dispose();
                    }

                }
            }
        }
        public static void CreateFlashCardTable()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $@"CREATE TABLE [dbo].[Flashcards](
                        [Id][int] IDENTITY(1, 1) NOT NULL,
                        [StackId] [int] NOT NULL,
                        [Front][nvarchar](max) NOT NULL,
                        [Back] [nvarchar](max) NOT NULL,
                        CONSTRAINT[PK_Flashcards] PRIMARY KEY CLUSTERED([Id] ASC),
                        CONSTRAINT[FK_Flashcards_Stacks] FOREIGN KEY([StackId]) 
                        REFERENCES[dbo].[Stacks]([Id]) 
                        ON UPDATE CASCADE 
                        ON DELETE CASCADE)";

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Flashcards table created Successfully");
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Message == "There is already an object named 'Flashcards' in the database.")
                        {
                            Console.WriteLine("Flashcards Table already Existed!");
                        }
                        else
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Dispose();
                    }
                }
            }
        }
        public static void CreateStudyTable()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $@"CREATE TABLE [dbo].[Study](
                        [Id][int] IDENTITY(1, 1) NOT NULL,
                        [StackId] [int] NOT NULL,
                        [Date][date] NOT NULL,
                        [CorrectAnswers] [int] NOT NULL,
                        [TotalGueses] [int] NOT NULL,
                        CONSTRAINT[PK_Study] PRIMARY KEY CLUSTERED([Id] ASC),
                        CONSTRAINT[FK_Study_Stacks] FOREIGN KEY([StackId]) 
                        REFERENCES[dbo].[Stacks]([Id]) 
                        ON UPDATE CASCADE 
                        ON DELETE CASCADE)";
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Study table created Successfully");
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Message == "There is already an object named 'Study' in the database.")
                        {
                            Console.WriteLine("Study Table already Existed!");
                        }
                        else
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Dispose();
                    }
                }
            }
        }
    }
}
