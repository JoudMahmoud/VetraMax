using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using VetraMax.Domain.Entities;

namespace VetraMax.Infrastructure.Persistence
{
	public class VetraMaxDbContext : IdentityDbContext<User>
	{
		public VetraMaxDbContext(DbContextOptions<VetraMaxDbContext> options) : base(options) { }

		public DbSet<Category> Categories { get; set; }
		public DbSet<Day> Days { get; set; }
		public DbSet<Line> Lines { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderProduct> OrderProducts { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductPriceTier> ProductPriceTier { get; set; }
		public DbSet<SubCategory> SubCategories { get; set; }
		public DbSet<TraderPricing> TradersPricing { get; set; }
		public DbSet<TraderType> TraderTypes { get; set; }
		public DbSet<TraderVerificationInfo> TraderVerificationInfos { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Wallet> Wallets { get; set; }
		public DbSet<WalletTransaction> WalletTransactions { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Product>()
				.Property(p => p.WeightUnit)
				.HasConversion<string>();

			modelBuilder.Entity<Order>(entity =>
			{
				entity.Property(p => p.DeliveryState).HasConversion<string>();
				entity.Property(p => p.PaymentMethod).HasConversion<string>();
			});

			modelBuilder.Entity<WalletTransaction>(entity =>
			{
				entity.Property(p => p.ChargingMethod).HasConversion<string>();
				entity.Property(p => p.Status).HasConversion<string>();
				entity.Property(p => p.TransactionType).HasConversion<string>();
			});


			modelBuilder.Entity<Day>().HasData(
			new Day { Id = 1, DayOfWeek = "Saturday" },
			new Day { Id = 2, DayOfWeek = "Sunday" },
			new Day { Id = 3, DayOfWeek = "Monday" },
			new Day { Id = 4, DayOfWeek = "Tuesday" },
			new Day { Id = 5, DayOfWeek = "Wednesday" },
			new Day { Id = 6, DayOfWeek = "Thursday" },
			new Day { Id = 7, DayOfWeek = "Friday" }
			);

			modelBuilder.Entity<User>()
				.HasMany(u => u.FavoriteProducts)
				.WithMany(p => p.FavoritedByUsers)
				.UsingEntity(j => j.ToTable("Favorites"));
		}
	}
}