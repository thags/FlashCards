using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.xmlManager;
using System.Data.SqlClient;

namespace FlashCards
{
    class DBManager
    {
        public static void CreateDatabase()
        {
            try
            {
                String str;
                string server = XmlManager.ReadConfig("dbServer");
                string integratedSecurity = XmlManager.ReadConfig("integratedSecurity");
                string dbFileName = XmlManager.ReadConfig("dbFileName");
                string filePath = XmlManager.ReadConfig("dbFilePath");
                SqlConnection myConn = new SqlConnection($"Server={server};Integrated security={integratedSecurity}");

                str = $"CREATE DATABASE {dbFileName} ON PRIMARY " +
                 "(NAME = Flashcards, " +
                 $"FILENAME = N'{filePath}\\{dbFileName}.mdf', " +
                 "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
                 "LOG ON (NAME = Flashcards_Log, " +
                 $"FILENAME = '{filePath}\\{dbFileName}Log.ldf', " +
                 "SIZE = 1MB, " +
                 "MAXSIZE = 5MB, " +
                 "FILEGROWTH = 10%)";

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
                String str;
                SqlConnection myConn = OpenSql();

                str = $"CREATE TABLE [dbo].[Stacks]("+
                        "[Id][int] IDENTITY(1, 1) NOT NULL,"+
                        "[Name] [nvarchar](max)NOT NULL," +
                        "CONSTRAINT[PK_Stacks] PRIMARY KEY CLUSTERED([Id] ASC)" +
                        "WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON,"+
                        "OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]";

                SqlCommand myCommand = new SqlCommand(str, myConn);
                try
                {
                    myCommand.ExecuteNonQuery();
                    Console.WriteLine("Stacks table created Successfully");
                }
                catch (System.Exception ex)
                {
                    if (ex.HResult == -2146232060)
                    {
                        Console.WriteLine("Stacks Table Exists!");
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
                Console.WriteLine("Stacks table Exists!");
            }
        }

        public static void CreateFlashCardTable()
        {
            try
            {
                String str;
                SqlConnection myConn = OpenSql();

                str = $"CREATE TABLE [dbo].[Flashcards](" +
                        "[Id][int] IDENTITY(1, 1) NOT NULL," +
                        "[StackId] [int] NOT NULL,"+
                        "[Front][nvarchar](max) NOT NULL," +
                        "[Back] [nvarchar](max)NOT NULL," +
                        "CONSTRAINT[PK_Flashcards] PRIMARY KEY CLUSTERED" +
                        "([Id] ASC)WITH" +
                        "(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)" +
                        "ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]";

                SqlCommand myCommand = new SqlCommand(str, myConn);
                try
                {
                    myCommand.ExecuteNonQuery();
                    Console.WriteLine("Flashcards table created Successfully");
                }
                catch (System.Exception ex)
                {
                    if (ex.HResult == -2146232060)
                    {
                        Console.WriteLine("Flashcards Table Exists!");
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
                Console.WriteLine("FlashCards table Exists!");
            }
        }

        public static void CreateFlashCardForeignKey()
        {
            try
            {
                String str;
                SqlConnection myConn = OpenSql();

                str = $"ALTER TABLE [dbo].[Flashcards] WITH CHECK ADD CONSTRAINT [FK_Flashcards_Stacks] FOREIGN KEY([StackId]) "+
                        "REFERENCES[dbo].[Stacks]([Id]) " +
                        "ON UPDATE CASCADE " +
                        "ON DELETE CASCADE";

                SqlCommand myCommand = new SqlCommand(str, myConn);
                try
                {
                    myCommand.ExecuteNonQuery();
                    Console.WriteLine("Flashcards key created Successfully");
                }
                catch (System.Exception ex)
                {
                    if (ex.HResult == -2146232060)
                    {
                        Console.WriteLine("Flashcards FK  Exists!");
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
                Console.WriteLine("FlashCards FK Exists!");
            }
        }

        private static string GetConnectionString()
        {
            string server = XmlManager.ReadConfig("dbServer");
            
            string integratedSecurity = XmlManager.ReadConfig("integratedSecurity");
            string dbFileName = XmlManager.ReadConfig("dbFileName");
            string connectionString = $"Server={server};database={dbFileName};Integrated Security={integratedSecurity}";
            return connectionString;
        }
        public static SqlConnection OpenSql()
        {
            string connectionString = GetConnectionString();
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }


    }
}
