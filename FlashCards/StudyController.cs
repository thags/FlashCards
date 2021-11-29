using System.Collections.Generic;
using FlashCards.Models;
using System.Data.SqlClient;
using System;

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

        public static List<StudySessionToView> GetAllStudySessions()
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<StudySessionToView> { };
            string sqlCommand = $"SELECT * FROM Study";

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string stackName = StackController.GetNameFromId((int)dataReader.GetValue(1));
                DateTime date = (DateTime)dataReader.GetValue(2);
                string dateString = date.ToShortDateString();
                int correctAnswers = (int)dataReader.GetValue(3);
                int totalGueses = (int)dataReader.GetValue(4);
                double scorePercent = Math.Round((double)correctAnswers / (double)totalGueses, 3);
                StudySessionToView study = new StudySessionToView
                {
                    StackName = stackName,
                    Date = dateString,
                    CorrectAnswers = correctAnswers,
                    TotalGueses = totalGueses,
                    ScorePercent = scorePercent
                };
                stackList.Add(study);
            }

            command.Dispose();
            connection.Close();

            return stackList;
        }

    }
}
