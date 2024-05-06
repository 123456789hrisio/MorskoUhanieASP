using System.Globalization;

namespace HotelMorskoUhanie.Data
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public DateTime DateModified { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}