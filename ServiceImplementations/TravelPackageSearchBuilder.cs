using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementations
{
    public class TravelPackageSearchBuilder
    {
        private readonly TravelPackageSearchDTO _searchDto = new();

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

        public TravelPackageSearchBuilder WithDestination(string destination)
        {
            _searchDto.Destination = destination;
            return this;
        }

        public TravelPackageSearchDTO Build()
        {
            return _searchDto;
        }
    }

}
