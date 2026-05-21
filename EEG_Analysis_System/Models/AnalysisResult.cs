namespace EEG_Analysis_System.Models
{
    public class AnalysisResult
    {
        public int Id { get; set; }
        public string ResultType { get; set; } = string.Empty;
        public double Confidence { get; set; } // Переконайся, що тут double або float
        public DateTime CreatedAt { get; set; }
        public string? Notes { get; set; }
    }
}