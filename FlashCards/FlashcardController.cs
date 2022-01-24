using FlashCards.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace FlashCards
{
    class FlashcardController
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public static bool CheckCardExists(int Id)
        {
            var result = GetCardById(Id);
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<Flashcard> GetCardById(int Id)
        {
            var stackList = new List<Flashcard> { };
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT TOP 1 * FROM Flashcards WHERE Id = {Id}";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int cardId = (int)reader["Id"];
                            string stackName = StackController.GetNameFromId((int)reader["StackId"]);
                            string front = (string)reader["Front"];
                            string back = (string)reader["Back"];
                            Flashcard newStack = new Flashcard
                            {
                                Id = cardId,
                                StackName = stackName,
                                Front = front,
                                Back = back,
                            };
                            stackList.Add(newStack);
                        }
                    }
                }
            }

            return stackList;
        }
        public static List<Flashcard> GetAllCardsInStack(string stackName)
        {
            var stackList = new List<Flashcard> { };
            int stackId = StackController.GetIdFromName(stackName);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT * FROM Flashcards WHERE StackId = {stackId} ORDER BY Id DESC";
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int Id = (int)dataReader["Id"];
                            string Front = (string)dataReader["Front"];
                            string Back = (string)dataReader["Back"];
                            Flashcard newStack = new Flashcard
                            {
                                Id = Id,
                                StackName = stackName,
                                Front = Front,
                                Back = Back,
                            };
                            stackList.Add(newStack);
                        }
                    }
                }
            }
            return stackList;
        }

        public static List<Flashcard> GetXCardsInStack(string stackName, int xCards)
        {
            var stackList = new List<Flashcard> { };
            int stackId = StackController.GetIdFromName(stackName);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT TOP {xCards} * FROM Flashcards WHERE StackId = {stackId} ORDER BY Id DESC";
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int Id = (int)dataReader["Id"];
                            string Front = (string)dataReader["Front"];
                            string Back = (string)dataReader["Back"];
                            Flashcard newStack = new Flashcard
                            {
                                Id = Id,
                                StackName = stackName,
                                Front = Front,
                                Back = Back,
                            };
                            stackList.Add(newStack);
                        }
                    }
                }
            }
            return stackList;
        }
        
        public static List<Flashcard> GetLastCardInStack(string stackName)
        {
            var stackList = new List<Flashcard> { };
            int stackId = StackController.GetIdFromName(stackName);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT TOP 1 * FROM Flashcards WHERE StackId = {stackId} ORDER BY Id DESC";
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int Id = (int)dataReader["Id"];
                            string Front = (string)dataReader["Front"];
                            string Back = (string)dataReader["Back"];
                            Flashcard newStack = new Flashcard
                            {
                                Id = Id,
                                StackName = stackName,
                                Front = Front,
                                Back = Back,
                            };
                            stackList.Add(newStack);
                        }
                    }
                }
            }
            return stackList;
        }
        public static void CreateFlashCard(string stackName, string front, string back)
        {
            int stackId = StackController.GetIdFromName(stackName);
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"Insert into flashcards (StackId, Front, Back) values ({stackId}, '{front}', '{back}') ";
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateCard(int cardId, string newText, string side)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"UPDATE flashcards SET {side} = '{newText}' WHERE Id = {cardId}";
                    command.ExecuteNonQuery();
                }
            }
        }
        public static void Delete(int Id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM flashcards WHERE Id = {Id}";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
