namespace EEG_Analysis_System.Models
{
    public class EEGSession
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string SessionName { get; set; }

        public DateTime CreatedAt { get; set; }

        public int DurationSeconds { get; set; }

        public string Status { get; set; }
    }
}