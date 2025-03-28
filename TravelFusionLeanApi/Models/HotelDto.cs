        using System.Text.Json.Serialization;

        /// <summary>
        /// Oplysninger om hotel, tilpasset mock-API'ets JSON struktur.
        /// </summary>
        public class HotelData
        {
            [JsonPropertyName("hotelId")]
            public string HotelId { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("cityCode")]
            public string CityCode { get; set; }

            [JsonPropertyName("countryCode")]
            public string CountryCode { get; set; }

            [JsonPropertyName("stars")]
            public int StarRating { get; set; }

            [JsonPropertyName("rating")]
            public double Rating { get; set; }

            [JsonPropertyName("price")]
            public PriceInfo Price { get; set; }

            [JsonPropertyName("available")]
            public bool Available { get; set; }

            [JsonPropertyName("images")]
            public List<string> Images { get; set; }

            [JsonPropertyName("geoCode")]
            public GeoCodeInfo GeoCode { get; set; }
        }

/// <summary>
/// Indeholder prisoplysninger for et hotel tilpasset mock-API'ets JSON struktur.
/// </summary>
public class PriceInfo
        {
            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonPropertyName("total")]
            public decimal Total { get; set; }
        }

/// <summary>
/// Indeholder geografiske koordinater for et hotel tilpasset MockHotelsAPI'ets JSON struktur.
/// </summary>
public class GeoCodeInfo
        {
            [JsonPropertyName("latitude")]
            public double Latitude { get; set; }

            [JsonPropertyName("longitude")]
            public double Longitude { get; set; }
        }

/// <summary>
/// Data transfer object (Dto) for hoteldata, tilpasset mock-API'ets JSON struktur.
/// </summary>
public class HotelResponse 
{
    public List<HotelData> Data { get; set; }
}