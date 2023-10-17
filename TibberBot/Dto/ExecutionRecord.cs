namespace TibberBot.Dto
{
    public record ExecutionRecord
    {
        public long Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Commands { get; set; }
        public int Result { get; set; }
        public TimeSpan Duration { get; set; }

        public override string ToString()
        {
            return $"{TimeStamp},{Commands},{Result},{Duration}";
        }
    }
}
