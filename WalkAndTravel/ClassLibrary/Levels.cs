using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{

    public class Levels
    {
        private int _experience;
        private int _level;

        public Levels()
        {
            _experience = 0;
            _level = 1;
        }

        public int Experience
        {
            get { return _experience; }
            set { _experience = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public void AddExperience(int exp)
        {
            _experience += exp;
        }

        public void LevelUp()
        {
            _level += 1;
            _experience = 0;
        }

        public void CompleteTrail(int exp)
        {
            _experience += exp;
        }

        public void CreateTrail(int exp)
        {
            _experience += exp;
        }
    }
}
