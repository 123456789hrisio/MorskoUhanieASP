namespace HotelMorskoUhanie.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ComeInDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public string UsersId { get; set; }
        public User Users { get; set; }
        public int RoomsId { get; set; }
        public Room Rooms { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}