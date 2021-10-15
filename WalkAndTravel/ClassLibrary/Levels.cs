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

        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public void AddExperience(int exp)
        {
            experience += exp;
        }

        public void LevelUp()
        {
            level += 1;
            experience = 0;
        }

        public void CompleteTrail(float routeLength)    // routeLength in km with two numbers after point
        {
            experience += (int)routeLength*100;
        }

        public void CreateTrail(int exp)
        {
            experience += exp;
        }
    }
}
