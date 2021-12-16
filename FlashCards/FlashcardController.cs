using System.Collections.Generic;
using FlashCards.Models;
using System.Data.SqlClient;

namespace FlashCards
{
    class FlashcardController
    {
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
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Flashcard> { };
            string sqlCommand = $"SELECT TOP 1 * FROM Flashcards WHERE Id = {Id}";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                int cardId = (int)dataReader["Id"];
                string stackName = StackController.GetNameFromId((int)dataReader["StackId"]);
                string front = (string)dataReader["Front"];
                string back = (string)dataReader["Back"];
                Flashcard newStack = new Flashcard
                {
                    Id = cardId,
                    StackName = stackName,
                    Front = front,
                    Back = back,
                };
                stackList.Add(newStack);
            }

            command.Dispose();
            connection.Close();

            return stackList;
        }
        public static List<Flashcard> GetAllCardsInStack(string stackName)
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Flashcard> { };
            int stackId = StackController.GetIdFromName(stackName);
            string sqlCommand = $"SELECT * FROM Flashcards WHERE StackId = {stackId} ORDER BY Id DESC";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

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

            command.Dispose();
            connection.Close();

            return stackList;
        }
        public static List<Flashcard> GetXCardsInStack(string stackName, int xCards)
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Flashcard> { };
            int stackId = StackController.GetIdFromName(stackName);
            string sqlCommand = $"SELECT TOP {xCards} * FROM Flashcards WHERE StackId = {stackId} ORDER BY Id DESC";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

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

            command.Dispose();
            connection.Close();

            return stackList;
        }
        public static List<Flashcard> GetLastCardInStack(string stackName)
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Flashcard> { };
            int stackId = StackController.GetIdFromName(stackName);
            string sqlCommand = $"SELECT TOP 1 * FROM Flashcards WHERE StackId = {stackId} ORDER BY Id DESC";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

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

            command.Dispose();
            connection.Close();

            return stackList;
        }
        public static void CreateFlashCard(string stackName, string front, string back)
        {
            SqlConnection connection = DBManager.OpenSql();
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();

            string sql;
            int stackId = StackController.GetIdFromName(stackName);

            sql = $"Insert into flashcards (StackId, Front, Back) values ({stackId}, '{front}', '{back}') ";
            command = new SqlCommand(sql, connection);

            adapter.InsertCommand = new SqlCommand(sql, connection);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }
        public static void UpdateCard(int cardId, string newText, string side)
        {
            SqlConnection connection = DBManager.OpenSql();
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;

            sql = $"UPDATE flashcards SET {side} = '{newText}' WHERE Id = {cardId}";
            command = new SqlCommand(sql, connection);

            adapter.DeleteCommand = new SqlCommand(sql, connection);
            adapter.DeleteCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }
        public static void Delete(int Id)
        {
            SqlConnection connection = DBManager.OpenSql();
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;

            sql = $"DELETE FROM flashcards WHERE Id = {Id}";
            command = new SqlCommand(sql, connection);

            adapter.DeleteCommand = new SqlCommand(sql, connection);
            adapter.DeleteCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }
    }
}
