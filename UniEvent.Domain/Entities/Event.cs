namespace UniEvent.Domain.Entities
{
    public class Event
    {
        // primary key
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        
        public DateTime EventTime { get; set; }
        public int Capacity { get; set; }

        public string Location { get; set; } = string.Empty;

        


    }
}
