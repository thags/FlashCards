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
        public static List<Flashcard> GetAllCards()
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<Flashcard> { };

            SqlCommand command = new SqlCommand("SELECT * FROM Flashcards", connection);
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
                    StackId = Stack,
                    Front = Front,
                    Back = Back,
                };
                stackList.Add(newStack);
            }

            command.Dispose();
            connection.Close();

            return stackList;
        }
    }
}
