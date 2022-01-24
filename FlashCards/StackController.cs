using FlashCards.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace FlashCards
{
    class StackController
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public static int GetIdFromName(string name)
        {
            int answer = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT TOP 1 * FROM Stacks WHERE Name = '{name}'";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            answer = (int)reader.GetValue(0);
                        }
                    }
                }
            }
            return answer;
        }
        public static string GetNameFromId(int Id)
        {
            string answer = "null";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText= $"SELECT TOP 1 * FROM Stacks WHERE Id = {Id}";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            answer = (string)reader.GetValue(1);
                        }
                    }
                }
            }
            return answer;
        }
        public static bool CheckStackExists(string name)
        {
            var result = GetStacks(name);
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<Stack> GetLastStack()
        {
            var stackList = new List<Stack> { };
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT TOP 1 * FROM Stacks ORDER BY Id DESC";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = (int)reader.GetValue(0);
                            string Name = (string)reader.GetValue(1);
                            Stack newStack = new Stack
                            {
                                Id = Id,
                                Name = Name
                            };
                            stackList.Add(newStack);
                        }
                    }
                }
            }
            return stackList;
        }
        public static List<Stack> GetStacks()
        {
            var stackList = new List<Stack> { };
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT * FROM Stacks";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = (int)reader.GetValue(0);
                            string Name = (string)reader.GetValue(1);
                            Stack newStack = new Stack
                            {
                                Id = Id,
                                Name = Name
                            };
                            stackList.Add(newStack);
                        }
                    }
                }
            }
            return stackList;
        }
        public static List<Stack> GetXStacks(int XAmount, string order = "ASC")
        {
            var stackList = new List<Stack> { };
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT TOP {XAmount} * FROM Stacks ORDER BY Id {order}";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = (int)reader.GetValue(0);
                            string Name = (string)reader.GetValue(1);
                            Stack newStack = new Stack
                            {
                                Id = Id,
                                Name = Name
                            };
                            stackList.Add(newStack);
                        }
                    }
                }
            }
            return stackList;
        }
        public static List<Stack> GetStacks(int stackId)
        {
            var stackList = new List<Stack> { };
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT TOP 1 * FROM Stacks WHERE Id = {stackId}";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = (int)reader.GetValue(0);
                            string Name = (string)reader.GetValue(1);
                            Stack newStack = new Stack
                            {
                                Id = Id,
                                Name = Name
                            };
                            stackList.Add(newStack);
                        }
                    }
                }
            }
            return stackList;
        }
        public static List<Stack> GetStacks(string name)
        {
            var stackList = new List<Stack> { };
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT TOP 1 * FROM Stacks WHERE Name = '{name}'";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = (int)reader.GetValue(0);
                            string Name = (string)reader.GetValue(1);
                            Stack newStack = new Stack
                            {
                                Id = Id,
                                Name = Name
                            };
                            stackList.Add(newStack);
                        }
                    }
                }
            }
            return stackList;
        }
        public static void InsertStack(string stackName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"Insert into Stacks (Name) values ('{stackName}') ";
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteStack(string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM Stacks WHERE Name = '{name}'";
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateStackName(string stackName, string updatedName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"UPDATE Stacks SET Name = '{updatedName}' WHERE Name = '{stackName}'";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
