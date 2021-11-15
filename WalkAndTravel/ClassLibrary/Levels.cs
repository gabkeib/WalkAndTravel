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
            if((_experience + exp) < (_level * 100 * 1.25))
            {
                _experience += exp;
            }
            else
            {
                int experience = _experience + exp;
                while (experience >= (_level * 100 * 1.25))
                {
                    experience -= (int)(_level * 100 * 1.25);
                    LevelUp();
                }
                _experience = experience;
            }
        }

        public void LevelUp()
        {
            _level += 1;
            //_experience = 0;
        }

        public void CompleteTrail(float routeLength)    // routeLength in km with two numbers after point
        {
            _experience += (int) routeLength * 100;
        }

        public void CreateTrail(int exp)
        {
            _experience += exp;
        }
    }
}
