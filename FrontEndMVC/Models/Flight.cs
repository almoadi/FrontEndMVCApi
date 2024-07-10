namespace FrontEndMVC.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Departuer { get; set; }
        public string Arrival { get; set; }

        public List<Passenger> passengers { get; set; } = new List<Passenger>();

        //public List<Booking>? Bookings { get; set; }
    }
}
