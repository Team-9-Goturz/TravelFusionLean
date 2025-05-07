namespace TravelFusionLeanApi.Models
{
    public class StripeSessionResponseDTO
    {
        public string Url { get; set; } = string.Empty;
        public string StripeSessionId { get; set; } = string.Empty;
        public string StripePaymentIntentId { get; set; } = string.Empty;
    }

}
