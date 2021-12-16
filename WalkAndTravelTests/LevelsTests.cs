using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkAndTravel.ClassLibrary;
using Xunit;

namespace WalkAndTravelTests
{
    public class LevelsTests
    {
        [Theory]
        [InlineData(100, 1)]
        public void Levels_CompleteTrail_ReturnsCorrectExperiance(int experiance, int levels)
        {
            Levels level = new Levels();
            level.AddExperience(experiance);
            Assert.Equal(100, level.Exp);
            Assert.Equal(levels, level.Level);
        }

        [Theory]
        [InlineData(4.3, 3)]
        public async void Levels_CompleteTrail_ReturnsCorrectLevel(double length, int levels)
        {
            Levels level = new Levels();
            await level.CompleteTrail(length);
            Assert.Equal(levels, level.Level);
        }

        [Fact]
        public void Levels_LevelUp_CorrectlyLevelUps()
        {
            Levels level = new Levels();
            level.Exp = 300;
            level.LevelUp();
            Assert.Equal(175, level.Exp);
            Assert.Equal(2, level.Level);
        }
    }
}
