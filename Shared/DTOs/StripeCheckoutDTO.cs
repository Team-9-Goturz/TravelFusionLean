namespace Shared.Dtos
{
    public class StripeCheckoutDTO
    {
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? PackageHeadline { get; set; }
    }

}
