namespace TravelFusionLeanApi.Configuration
{
    /// <summary>
    /// Indeholder URL'er til eksterne mock-API'er som sættes via appsettings.json
    /// </summary>
    public class ApiSettings
    {
        public string MockFlightsApiUrl { get; set; } = string.Empty;
        public string MockHotelsApiUrl { get; set; } = string.Empty;
    }
}