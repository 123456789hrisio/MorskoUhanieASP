namespace MorskoUhanie.Data
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Register {  get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
