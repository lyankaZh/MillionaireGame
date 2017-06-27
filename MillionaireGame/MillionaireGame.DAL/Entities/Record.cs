namespace MillionaireGame.DAL.Entities
{
    public class Record
    {
        public int RecordId { get; set; }

        public string Username { get; set; }

        public virtual Question Question { get; set; }

        public int Answer { get; set; }
    }
}
