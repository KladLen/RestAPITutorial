using MagicAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Villa> Villas { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Villa>().HasData(
				new Villa()
				{
					Id = 1,
					Name = "Royal",
					City = "New York",
					Rate = 10,
					ImageUrl = "https://www.villaabiente.com/resources/abiente/headers/mobile/xVilla,P20Abiente,P20-,P20Magnificent,P20exterior,P20villa.jpg.pagespeed.ic.e6bNABZFo5.jpg",
					CreatedDate = DateTime.Now
				},
				new Villa()
				{
					Id = 2,
					Name = "Sun",
					City = "Berlin",
					Rate = 8.7,
					ImageUrl = "https://image.urlaubspiraten.de/1024/image/upload/v1669132243/Impressions%20and%20Other%20Assets/28ad22b7-e6e8-41dd-94f8-ad3490ac205c_ygxlws.webp",
					CreatedDate = DateTime.Now
				}
				);
		}
	}
}
