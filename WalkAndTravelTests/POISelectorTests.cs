using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using Xunit;

namespace WalkAndTravelTests
{
    public class POISelectorTests
    {
        [Fact]
        public void POISelector_SelectPOI_ReturnsCorrectAnyPOIs()
        {
            POISelector selector = new POISelector();
            var list = selector.SelectPOI();
            int res = 0;
            for (int i = 0; i < 20; i++)
            {
                if (list[i] != null)
                {
                    res++;
                }
            }
            Assert.Equal(6, res);
        }

        [Fact]
        public void POISelector_SelectPOI_ReturnsCorrectSpecializedPOIs()
        {
            POISelector selector = new POISelector();
            var list = selector.SelectPOI(Amenity.Restaurant);
            int res = 0;
            for (int i = 0; i < 20; i++)
            {
                if (list[i] != null)
                {
                    res++;
                }
            }
            Assert.Equal(3, res);
        }
    }
}
