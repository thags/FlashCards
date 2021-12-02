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
                double scorePercent = Math.Round(((double)correctAnswers / (double)totalGueses) *100, 2);
                StudySessionToView study = new StudySessionToView
                {
                    StackName = stackName,
                    Date = dateString,
                    CorrectAnswers = correctAnswers,
                    TotalGueses = totalGueses,
                    ScorePercent = $"{scorePercent}%"
                };
                stackList.Add(study);
            }

            command.Dispose();
            connection.Close();

            return stackList;
        }
        public static List<AverageScoreByMonth> GetAverageByMonthPivot(string yearChoice)
        {
            SqlConnection connection = DBManager.OpenSql();

            var stackList = new List<AverageScoreByMonth> { };

            string sqlCommand = $@"SELECT * FROM
                (
                    SELECT
                        DATENAME(MONTH, dbo.Study.Date) AS StudyMonth
                        , ROUND((CAST(dbo.Study.CorrectAnswers AS FLOAT) / CAST(dbo.Study.TotalGueses AS FLOAT)) * 100, 0) 
                        AS Grade
                        , dbo.Stacks.Name AS[StackName]
                        FROM
                        dbo.Study INNER JOIN
                        dbo.Stacks ON dbo.Study.StackId = dbo.Stacks.Id
                        WHERE DATENAME(YEAR, Date) = '{yearChoice}'
                ) AS Src
                PIVOT 
                (
                    AVG([Grade])
                    FOR[StudyMonth] 
                    IN([January], [February], [March], [April], [May], [June], [July], [August], [September], [October], [November], [December])
                ) AS Pvt";


            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                AverageScoreByMonth study = new AverageScoreByMonth
                {
                    StackName = dataReader["StackName"].ToString(),
                    January = CheckIfVoidOrEmpty(dataReader["January"].ToString()),
                    February = CheckIfVoidOrEmpty(dataReader["February"].ToString()),
                    April = CheckIfVoidOrEmpty(dataReader["April"].ToString()),
                    March = CheckIfVoidOrEmpty(dataReader["March"].ToString()),
                    May = CheckIfVoidOrEmpty(dataReader["May"].ToString()),
                    June = CheckIfVoidOrEmpty(dataReader["June"].ToString()),
                    July = CheckIfVoidOrEmpty(dataReader["July"].ToString()),
                    August = CheckIfVoidOrEmpty(dataReader["August"].ToString()),
                    September = CheckIfVoidOrEmpty(dataReader["September"].ToString()),
                    October = CheckIfVoidOrEmpty(dataReader["October"].ToString()),
                    November = CheckIfVoidOrEmpty(dataReader["November"].ToString()),
                    December = CheckIfVoidOrEmpty(dataReader["December"].ToString()),
                };
                stackList.Add(study);
            }

            command.Dispose();
            connection.Close();

            return stackList;
        }

        private static string CheckIfVoidOrEmpty(string s)
        {
            if (s == null || s == "")
            {
                return "0";
            }
            else
            {
                return s;
            }
        }
    }
}
