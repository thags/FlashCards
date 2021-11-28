namespace FlashCards.Models
{
    public class StudySessionToView
    {
        public string StackName { get; set; }
        public System.DateTime Date { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalGueses { get; set; }
        public double ScorePercent { get; set; }
    }
}
