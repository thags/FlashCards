using System.Collections.Generic;
using FlashCards.Models;
using System.Data.SqlClient;

namespace FlashCards
{
    class StudyController
    {
        public static void InsertStudySession(StudySession study)
        {
            SqlConnection connection = DBManager.OpenSql();
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql;

            sql = @$"Insert into Study (StackId, Date, CorrectAnswers, TotalGueses) 
                    values ({study.StackId}, GETDATE() , {study.CorrectAnswers}, {study.TotalGueses}) ";

            command = new SqlCommand(sql, connection);

            adapter.InsertCommand = new SqlCommand(sql, connection);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }
    }
}
