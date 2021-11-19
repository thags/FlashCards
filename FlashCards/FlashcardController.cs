using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashCards.Models;
using System.Data.SqlClient;

namespace FlashCards
{
    class FlashcardController
    {
        public static List<Flashcard> GetAllCardsInStack(string stackName)
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Flashcard> { };
            int stackId = StackController.GetIdFromName(stackName);
            string sqlCommand = $"SELECT * FROM Flashcards WHERE StackId = {stackId}";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                int Id = (int)dataReader.GetValue(0);
                int Stack = (int)dataReader.GetValue(1);
                string Front = (string)dataReader.GetValue(2);
                string Back = (string)dataReader.GetValue(3);
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
    }
}
