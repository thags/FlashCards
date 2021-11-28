namespace FlashCards.Models
{
    public class StudySession
    {
        public int StackId { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalGueses { get; set; }
        public int ScorePercent { get; set; }
    }
}
