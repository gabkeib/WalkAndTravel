using System;
using WalkAndTravel.ClassLibrary;
using Xunit;

namespace WalkAndTravelTests
{
    public class MarkersTests
    {
        [Fact]
        public void MarkersCalculator_CalculateDistanceBetweenMarkers_ReturnsCorrectDistanceBetweenSameMarkers()
        {
            var marker1 = new Marker(54.68585, 25.28647);
            var marker2 = new Marker(54.68585, 25.28647);

            double distance = marker1.CalculateDistanceBetweenMarkers(marker2);
            Assert.Equal(0, distance, 6);
        }

        [Fact]
        public void MarkersCalculator_CalculateDistanceBetweenMarkers_ReturnsCorrectDistanceBetweenMarkers()
        {
            var marker1 = new Marker(50.0359, 5.4253);
            var marker2 = new Marker(58.3838, 3.0412);

            double distance = marker1.CalculateDistanceBetweenMarkers(marker2);
            Assert.Equal(942, distance, 1);
        }

        [Fact]
        public void MarkersCalculator_CalculateDistanceBetweenMarkers_ReturnsCorrectMarker()
        {
            var marker1 = new Marker(50.0359, 5.4253);
            double bearing = 10;
            double distance = 100;
            var marker2 = marker1.CalculateNextMarker(distance, bearing);
            var distance2 = marker1.CalculateDistanceBetweenMarkers(marker2);
            Assert.Equal(100, distance2, 0);
        }
    }
}
