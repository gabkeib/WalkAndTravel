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
		[InlineData(0.4, LengthType.Short)]
		[InlineData(1, LengthType.Medium)]
		[InlineData(2, LengthType.Long)]
		[InlineData(5, LengthType.VeryLong)]
		public void Routes_PickLengthType_ReturnsCorrectLengthType(double length, LengthType type)
		{
			Route route = new();
			route.Length = length;

			route.PickLengthType();

			Assert.Equal(type, route.Type);
		}

		[Fact]
		public void Routes_MarkersListToArray_ReturnsEmptyListOfArrays()
		{
			List<Marker> markers = new();
			List<double[]> expected = new();

			var result = Route.MarkersListToArray(markers);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void Routes_MarkersListToArray_ReturnsCorrectCoordinates()
		{
			List<Marker> markers = new();
			Marker marker = new(latitude: 54.693899674979086, longitude: 25.276343239919967);
			markers.Add(marker);

			List<double[]> expected = new();
			double[] coord = new double[] { 54.693899674979086, 25.276343239919967 };
			expected.Add(coord);

			var result = Route.MarkersListToArray(markers);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void Routes_AddCoordinate_ReturnsCorrectCoordinate()
		{
			Route route = new();
			Marker marker = new(54.693899674979086, 25.276343239919967);
			double[] expected = new double[] { 54.693899674979086, 25.276343239919967 };
			route.Coordinates = new List<double[]>();

			route.AddCoordinate(marker);
			var result = route.Coordinates.FirstOrDefault();

			Assert.Equal(expected, result);
		}

		[Fact]
		public void Routes_AddMarker_SetsAndReturnsCorrectMarkerAndCoordinate()
		{
			Route route = new();
			Marker marker = new(54.693899674979086, 25.276343239919967);
			route.Coordinates = new List<double[]>();
			route.Markers = new List<Marker>();
			double[] expectedCoord = new double[] {54.693899674979086, 25.276343239919967};

			route.AddMarker(marker);
			var resultM = route.Markers.FirstOrDefault();
			var resultC = route.Coordinates.FirstOrDefault();

			Assert.Equal(marker, resultM);
			Assert.Equal(expectedCoord, resultC);
		}

		[Theory]
		[InlineData(LengthType.Short)]
		[InlineData(LengthType.Medium)]
		[InlineData(LengthType.Long)]
		[InlineData(LengthType.VeryLong)]
		public void Routes_Type_ReturnsCorrectType(LengthType type)
		{
			Route route = new();

			route.Type = type;

			Assert.Equal(type, route.Type);
		}

		[Fact]
		public void Routes_Coordinates_SetsAndReturnsCorrectCoordinates()
		{
			Route route = new();
			List<double[]> coords = new();
			double[] coord = new double[] { 54.693899674979086, 25.276343239919967 };
			coords.Add(coord);

			route.Coordinates = coords;

			Assert.Equal(coords, route.Coordinates);
		}

		[Fact]
		public void Routes_Markers_SetsAndReturnsCorrectMarkers()
		{
			Route route = new();
			List<Marker> markers = new();
			Marker marker = new(54.693899674979086, 25.276343239919967);
			markers.Add(marker);

			route.Markers = markers;

			Assert.Equal(markers, route.Markers);
		}

		[Theory]
		[InlineData(5.3)]
		[InlineData(56.55)]
		[InlineData(103.566)]
		public void Routes_Length_SetsAndReturnsCorrectLength(double length)
		{
			Route route = new();

			route.Length = length;

			Assert.Equal(length, route.Length);
		}

		[Theory]
		[InlineData("Pavadinimas")]
		[InlineData("WAT")]
		[InlineData("Data")]
		public void Routes_Length_SetsAndReturnsCorrectName(string name)
		{
			Route route = new();

			route.Name = name;

			Assert.Equal(name, route.Name);
		}

		[Theory]
		[InlineData("Kitas", 0.1, LengthType.Short, 1)]
		[InlineData("Kitas", 5, LengthType.VeryLong, -1)]
		[InlineData("Kitas", 2, LengthType.Long, -1)]
		[InlineData("Kitas", 1.6, LengthType.Long, 1)]
		public void Routes_CompareTo_ReturnsCorrectValues(string name, double length, LengthType type, int value)
		{
			Route route = new(name: "Pagrindinis", length: 1.8, type: LengthType.Long, markers: new List<Marker>());
			Route other = new(name: name, length: length, type: type, markers: new List<Marker>());
			int expected = value;

			var result = route.CompareTo(other);

			Assert.Equal(expected, result);
		}
	}
}
