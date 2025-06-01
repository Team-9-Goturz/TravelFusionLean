using Shared.Dtos;
using Shared.Models;

namespace ServiceImplementations
{
    public class TravelPackageSearchBuilder
    {
        private readonly TravelPackageSearchDTO _searchDto = new();

        public TravelPackageSearchBuilder WithDepartureLocation(string departureLocation)
        {
            _searchDto.DepartureLocation = departureLocation;
            return this;
        }

        public TravelPackageSearchBuilder WithDestination(string destination)
        {
            _searchDto.Destination = destination;
            return this;
        }

        public TravelPackageSearchBuilder WithMinPrice(decimal minPrice)
        {
            _searchDto.MinPrice = minPrice;
            return this;
        }

        public TravelPackageSearchBuilder WithMaxPrice(decimal maxPrice)
        {
            _searchDto.MaxPrice = maxPrice;
            return this;
        }

        public TravelPackageSearchBuilder WithNumberOfTravelers(int numberOfTravelers)
        {
            _searchDto.NumberOfTravelers = numberOfTravelers;
            return this;
        }

        public TravelPackageSearchBuilder WithDepartureDateEarliest(DateOnly date)
        {
            _searchDto.DepartureDateEarliest = date;
            return this;
        }

        public TravelPackageSearchBuilder WithDepartureDateLatest(DateOnly date)
        {
            _searchDto.DepartureDateLatest = date;
            return this;
        }

        public TravelPackageSearchBuilder WithRecommendedOnly(bool recommended = true)
        {
            _searchDto.HasToBeRecommended = recommended;
            return this;
        }
        public TravelPackageSearchBuilder WithStatusOnly(TravelPackageStatus travelPackageStatus)
        {
            _searchDto.Status = travelPackageStatus;
            return this;
        }
        public TravelPackageSearchDTO Build()
        {
            return _searchDto;
        }
    }

}
