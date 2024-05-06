using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMorskoUhanie.Data
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumbersId { get; set; }
        public RoomNumber RoomNumbers { get; set; }
        public int RoomTypesId { get; set; }
        public RoomType RoomTypes { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePerDay { get; set; }
        public DateTime DateModified { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
