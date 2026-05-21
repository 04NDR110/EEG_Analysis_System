namespace EEG_Analysis_System.Models
{
    public class EEGSignal
    {
        public int Id { get; set; }

        public int SessionId { get; set; }

        public double SignalValue { get; set; }

        public DateTime Timestamp { get; set; }

        public double Frequency { get; set; }
    }
}