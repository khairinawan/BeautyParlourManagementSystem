using Microsoft.EntityFrameworkCore;
using BeautyParlourManagementAPI.Models;

namespace BeautyParlourManagementAPI.Data
{
    public class BeautyParlourManagementContext : DbContext
    {
        public BeautyParlourManagementContext(DbContextOptions<BeautyParlourManagementContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}