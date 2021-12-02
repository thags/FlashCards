﻿using System;
using System.Data.SqlClient;
using System.Configuration;

namespace FlashCards
{
    class DBManager
    {
        public static void CreateDatabase()
        {
            try
            {
                string str;
                string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
                SqlConnection myConn = new SqlConnection(connectionString);

                str = "CREATE DATABASE flashcards";

                SqlCommand myCommand = new SqlCommand(str, myConn);
                try
                {
                    myConn.Open();
                    myCommand.ExecuteNonQuery();
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
                        Console.WriteLine(ex.ToString());
                    }  
                }
                finally
                {
                        myConn.Close();
                }
            }
            catch
            {
                Console.WriteLine("Database Exists!");
            }
        }
        public static void CreateStackTable()
        {
            try
            {
                string str;
                SqlConnection myConn = OpenSql();

                str = $@"CREATE TABLE [dbo].[Stacks](
                        [Id][int] IDENTITY(1, 1) NOT NULL,
                        [Name] [nvarchar](max)NOT NULL,
                        CONSTRAINT[PK_Stacks] PRIMARY KEY CLUSTERED([Id] ASC))";

                SqlCommand myCommand = new SqlCommand(str, myConn);
                try
                {
                    myCommand.ExecuteNonQuery();
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
                    myConn.Close();
                }
            }
            catch
            {
                Console.WriteLine("Connection could not establish");
            }
        }
        public static void CreateFlashCardTable()
        {
            try
            {
                string str;
                SqlConnection myConn = OpenSql();

                str = $@"CREATE TABLE [dbo].[Flashcards](
                        [Id][int] IDENTITY(1, 1) NOT NULL,
                        [StackId] [int] NOT NULL,
                        [Front][nvarchar](max) NOT NULL,
                        [Back] [nvarchar](max) NOT NULL,
                        CONSTRAINT[PK_Flashcards] PRIMARY KEY CLUSTERED([Id] ASC),
                        CONSTRAINT[FK_Flashcards_Stacks] FOREIGN KEY([StackId]) 
                        REFERENCES[dbo].[Stacks]([Id]) 
                        ON UPDATE CASCADE 
                        ON DELETE CASCADE)";

                SqlCommand myCommand = new SqlCommand(str, myConn);
                try
                {
                    myCommand.ExecuteNonQuery();
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
                    myConn.Close();
                }
            }
            catch
            {
                Console.WriteLine("Connection could not establish");
            }
        }

        public static void CreateStudyTable()
        {
            try
            {
                string str;
                SqlConnection myConn = OpenSql();

                str = $@"CREATE TABLE [dbo].[Study](
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

                SqlCommand myCommand = new SqlCommand(str, myConn);
                try
                {
                    myCommand.ExecuteNonQuery();
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
                    myConn.Close();
                }
            }
            catch
            {
                Console.WriteLine("Connection could not establish");
            }
        }

        public static void ReIndexFlashCards()
        {
                string str;
                SqlConnection myConn = OpenSql();

            str = @"ALTER TABLE [dbo].[Flashcards] DROP CONSTRAINT [PK_Flashcards]
                    ALTER TABLE [dbo].[Flashcards] DROP COLUMN [Id]
                    ALTER TABLE [dbo].[Flashcards] ADD [Id][int] IDENTITY(1, 1) NOT NULL
                    ALTER TABLE [dbo].[Flashcards] ADD  CONSTRAINT [PK_Flashcards] PRIMARY KEY CLUSTERED 
                    ([Id] ASC)";

                SqlCommand myCommand = new SqlCommand(str, myConn);
                    myCommand.ExecuteNonQuery();
                    myConn.Close();

        }
        public static SqlConnection OpenSql()
        {
            string connectionString = ConfigurationManager.AppSettings.Get("ConnectionStringWithDatabase");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
