using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceImplementations;
using Shared.Models;
using Data;

namespace ServiceTests;

[TestClass]
public class AirportServiceSqlTests
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
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Airport"); ;
    }

    [TestMethod]
    public async Task FindOrCreateAsync_CreatesNewAirport_WhenAirportDoesExist()
    {
        // Arrange
        using var context = GetSqlDbContext();

        Airport airport = new Airport
        {
            City = "Randers",
            Country = "Danmark",
            Name = "Randers Flyveplads",
            Address = "Flyvepladsvej 1",
            Latitude = (decimal?)56.406,
            Longitude = (decimal?)10.040
        };
        context.Airports.Add(airport);
        await context.SaveChangesAsync();

        AirportService service = new AirportService(context);

        // Act
        Airport result = await service.FindOrCreateAsync(new Airport
        {
            City = "Randers",
            Country = "Danmark",
            Name = "Randers Flyveplads",
            Address = "Flyvepladsvej 1",
            Latitude = (decimal?)56.406,
            Longitude = (decimal?)10.040
        });

        Assert.IsNotNull(result);
        Assert.AreEqual(airport.Name, result.Name);
        Assert.AreEqual(airport.City, result.City);
        Assert.AreEqual(airport.Id, result.Id);
    }

    [TestMethod]
    public async Task FindOrCreateAsync_CreatesNewAirport_WhenAirportDoesNotExist()
    {
        // Arrange
        using var context = GetSqlDbContext();
        AirportService service = new AirportService(context);

        Airport newAirport = new Airport { Name = "New Airport", City = "ukendt by", Country="Ukendt land",Address="Ukendt adresse",Latitude=0,Longitude=0 };

        // Act
        var result = await service.FindOrCreateAsync(newAirport);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(newAirport.Name, result.Name);
        Assert.AreEqual(newAirport.City, result.City);
        Assert.IsTrue(result.Id > 0);
    }
}