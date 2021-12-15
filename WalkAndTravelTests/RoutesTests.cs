using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using Xunit;

namespace WalkAndTravelTests
{
    public class RoutesTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(4)]
        public void CityRoute_GenerateRoute_RetursCorrectNumberOfWaypoints_NoReturningToStart(int numberOfWaypoints)
        {
            var rng = new Random();
            CityRoute route = new CityRoute(new Marker(54.68585, 25.28647), 2, numberOfWaypoints, false);
            Func<double> bearingCalculator = delegate () { return rng.Next(30, 359) + rng.NextDouble(); };
            route.GenerateRoute(bearingCalculator);

            Assert.Equal(numberOfWaypoints, route.Markers.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(4)]
        public void CityRoute_GenerateRoute_RetursCorrectNumberOfWaypoints_ReturningToStart(int numberOfWaypoints)
        {
            var rng = new Random();
            CityRoute route = new CityRoute(new Marker(54.68585, 25.28647), 2, numberOfWaypoints, true);
            Func<double> bearingCalculator = delegate () { return rng.Next(30, 359) + rng.NextDouble(); };
            route.GenerateRoute(bearingCalculator);

            if (numberOfWaypoints > 0)
            {
                Assert.Equal(numberOfWaypoints + 1, route.Markers.Count);
            }
            else
            {
                Assert.Equal(numberOfWaypoints, route.Markers.Count);
            }
        }

        [Fact]
        public void Routes_PickLengthType_ReturnsLengthTypeLong()
        {
            Route route = new();
            route.Length = 1.5;

            route.PickLengthType();
            var result = route.Type;

            Assert.Equal(LengthType.Long, result);
        }

        [Theory]
        [InlineData(2, LengthType.Long)]
        public void Routes_PickLengthType_ReturnsCorrectLengthType(int length, LengthType type)
        {
            Route route = new();
            route.Length = length;

            route.PickLengthType();
            var result = route.Type;

            Assert.Equal(type, result);
        }
    }
}
