using Data;
using Microsoft.EntityFrameworkCore;
using ServiceImplementations;
using Shared.Models;

namespace ServiceTests
{
    [TestClass]
    public class HotelModelServiceSqlTests
    {
        private const string ConnectionString = "Server=85.10.209.22;Database=TravelFusionTest;User ID=team09;Password=GdrnJu64D3Lo!;TrustServerCertificate=True";
   
        private AppDbContext GetSqlDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new AppDbContext(options);
        }
        [TestInitialize]
        public async Task TestInitialize()
        {
            // Sørg for at databasen er tom inden hver test kører
            using var context = GetSqlDbContext();
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Hotel");
        }

        [TestMethod]
        public async Task FindOrCreateAsync_ReturnsExistingHotel_WhenHotelExists()
        {
            // Arrange
            using var context = GetSqlDbContext();

            var hotel = new Hotel { Name = "Test Hotel", City = "Test City" };
            context.Hotels.Add(hotel);
            await context.SaveChangesAsync();

            var service = new HotelModelService(context);

            // Act
            var result = await service.FindOrCreateAsync(new Hotel { Name = "Test Hotel", City = "Test City" });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotel.Name, result.Name);
            Assert.AreEqual(hotel.City, result.City);
            Assert.AreEqual(hotel.Id, result.Id);
        }

        [TestMethod]
        public async Task FindOrCreateAsync_CreatesNewHotel_WhenHotelDoesNotExist()
        {
            // Arrange
            using var context = GetSqlDbContext();
            var service = new HotelModelService(context);

            var newHotel = new Hotel { Name = "New Hotel", City = "New City" };

            // Act
            var result = await service.FindOrCreateAsync(newHotel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newHotel.Name, result.Name);
            Assert.AreEqual(newHotel.City, result.City);
            Assert.IsTrue(result.Id > 0);
        }
    }
}
