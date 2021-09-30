using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public class Levels
    {
        private int experience;
        private int level;

        public Levels()
        {
            experience = 0;
            level = 1;
        }

        public int GetExperience()
        {
            return this.experience;
        }

        public void SetExperience(int experience)
        {
            this.experience = experience;
        }

        public int GetLevel()
        {
            return this.level;
        }

        public void GetLevel(int level)
        {
            this.level = level;
        }

        public void AddExperience(int exp)
        {
            this.experience += exp;
        }

        public void LevelUp()
        {
            this.level += 1;
            this.experience = 0;
        }

        public void CompleteTrail()
        {
            this.experience += 50;
        }

        public void CreateTrail()
        {
            this.experience += 30;
        }
    }
}
