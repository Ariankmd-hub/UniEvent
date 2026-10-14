namespace UniEvent.Domain.Entities
{
    public class Event
    {
        public int Id { get; private set; }

        public string Title { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string Description { get; set; }= string.Empty;
        
        public int Capacity { get; set; }

        public string Location { get; set; }=string.Empty;

        // the person who created this event .
        public string OrganizerId { get; set; } = string.Empty;

        // it's like list , with (add , remove , count ) ability . 
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();


    }
}
