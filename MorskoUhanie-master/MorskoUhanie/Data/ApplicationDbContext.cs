using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MorskoUhanie.Data;

namespace MorskoUhanie.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MorskoUhanie.Data.Reservation>? Reservation { get; set; }
        public DbSet<MorskoUhanie.Data.Room>? Room { get; set; }
        public DbSet<MorskoUhanie.Data.RoomType>? RoomType { get; set; }
    }
}