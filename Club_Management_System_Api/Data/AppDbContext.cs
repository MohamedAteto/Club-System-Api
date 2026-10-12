using Club_Management_System_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Booking> Bookings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<MemberShip>()
                .HasMany(m => m.payments)
                .WithOne(p => p.MemberShip)
                .HasForeignKey(p => p.MemberShipId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Member>()
                .HasMany(m => m.Bookings)
                .WithOne(b => b.Member)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Facility>()
                .HasMany(f => f.Bookings)
                .WithOne(b => b.Facility)
                .HasForeignKey(b => b.FacilityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.MemberShip)
                .WithOne(ms => ms.Member)
                .HasForeignKey<MemberShip>(ms => ms.MemberId)
                .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    MemberId = 1,
                    MemberFullName = "Ahmed Mohamed",
                    MemberEmail = "ahmed@example.com",
                    MemberPhone = "01011112222",
                    MemberBirthDate = new DateTime(2000, 5, 15, 0, 0, 0)
                },
                new Member
                {
                    MemberId = 2,
                    MemberFullName = "Mona Ali",
                    MemberEmail = "mona@example.com",
                    MemberPhone = "01022223333",
                    MemberBirthDate = new DateTime(1998, 9, 20, 0, 0, 0)
                },
                new Member
                {
                    MemberId = 3,
                    MemberFullName = "Omar Hassan",
                    MemberEmail = "omar@example.com",
                    MemberPhone = "01033334444",
                    MemberBirthDate = new DateTime(2002, 1, 10, 0, 0, 0)
                }
            );

            modelBuilder.Entity<MemberShip>().HasData(
                new MemberShip
                {
                    MemberShipId = 1,
                    MemberShipType = "Gold",
                    MemberShipStartDate = new DateTime(2026, 1, 1, 0, 0, 0),
                    MemberShipEndDate = new DateTime(2027, 1, 1, 0, 0, 0),
                    MemberShipPrice = 5000m,
                    MemberShipStatus = "Active",
                    MemberId = 1
                },
                new MemberShip
                {
                    MemberShipId = 2,
                    MemberShipType = "Silver",
                    MemberShipStartDate = new DateTime(2026, 3, 1, 0, 0, 0),
                    MemberShipEndDate = new DateTime(2027, 3, 1, 0, 0, 0),
                    MemberShipPrice = 3000m,
                    MemberShipStatus = "Active",
                    MemberId = 2
                },
                new MemberShip
                {
                    MemberShipId = 3,
                    MemberShipType = "Basic",
                    MemberShipStartDate = new DateTime(2025, 1, 1, 0, 0, 0),
                    MemberShipEndDate = new DateTime(2026, 1, 1, 0, 0, 0),
                    MemberShipPrice = 1500m,
                    MemberShipStatus = "Expired",
                    MemberId = 3
                }
            );

            modelBuilder.Entity<Facility>().HasData(
                new Facility
                {
                    FacilityId = 1,
                    FacilityName = "Swimming Pool",
                    FacilityCapacity = 30,
                    FacilityHourlyPrice = 250m
                },
                new Facility
                {
                    FacilityId = 2,
                    FacilityName = "Gym Hall",
                    FacilityCapacity = 40,
                    FacilityHourlyPrice = 150m
                },
                new Facility
                {
                    FacilityId = 3,
                    FacilityName = "Tennis Court",
                    FacilityCapacity = 10,
                    FacilityHourlyPrice = 200m
                }
            );

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    BoookingId = 1,
                    BookingDate = new DateTime(2026, 10, 10, 0, 0, 0),
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(12, 0, 0),
                    GroupSize = 10,
                    Status = "Confirmed",
                    MemberId = 1,
                    FacilityId = 1
                },
                new Booking
                {
                    BoookingId = 2,
                    BookingDate = new DateTime(2026, 10, 11, 0, 0, 0),
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(15, 0, 0),
                    GroupSize = 8,
                    Status = "Confirmed",
                    MemberId = 2,
                    FacilityId = 2
                },
                new Booking
                {
                    BoookingId = 3,
                    BookingDate = new DateTime(2026, 10, 12, 0, 0, 0),
                    StartTime = new TimeSpan(16, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0),
                    GroupSize = 4,
                    Status = "Cancelled",
                    MemberId = 3,
                    FacilityId = 3
                }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    PaymentId = 1,
                    PaymentAmount = 5000m,
                    PaymentDate = new DateTime(2026, 1, 1, 0, 0, 0),
                    PaymentMethod = "Card",
                    MemberShipId = 1
                },
                new Payment
                {
                    PaymentId = 2,
                    PaymentAmount = 3000m,
                    PaymentDate = new DateTime(2026, 3, 1, 0, 0, 0),
                    PaymentMethod = "Cash",
                    MemberShipId = 2
                },
                new Payment
                {
                    PaymentId = 3,
                    PaymentAmount = 1500m,
                    PaymentDate = new DateTime(2025, 1, 1, 0, 0, 0),
                    PaymentMethod = "Wallet",
                    MemberShipId = 3
                }
            );



        }



       
    }
}
