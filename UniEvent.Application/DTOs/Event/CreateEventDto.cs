namespace UniEvent.Application.DTOs.Event
{
    public class CreateEventDto
    {
        // what ever needs to create event !

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime EventTime { get; set; }
        public int Capacity { get; set; }

        public string Location { get; set; } = string.Empty;

    }
}
