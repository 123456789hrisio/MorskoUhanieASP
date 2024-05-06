namespace HotelMorskoUhanie.Data
{
    public class RoomNumber
    {
        public int Id { get; set; }
        public string RoomNumberName { get; set; }
        public DateTime DateModified { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}