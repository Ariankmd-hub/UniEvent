namespace UniEvent.Domain.Entities
{
    internal class Booking
    {
        public int Id { get; private set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }= string.Empty;

        public int EventId {  get; set; }

        public Event Event { get; set; } = null!;

    }
}
