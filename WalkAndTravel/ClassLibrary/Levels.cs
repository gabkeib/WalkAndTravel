using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{

    public class Levels
    {
        private object locker = new object();

        private int _experience;
        private int _level;

        public Levels()
        {
            _experience = 0;
            _level = 1;
        }

        //[Column("Exp")]
        public int Exp
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
            lock (locker)
            {
                _experience += exp;
                if (_experience >= (_level * 125))
                {
                    LevelUp();
                }
            }
        }

        public void LevelUp()
        {
            while (_experience >= (_level * 125))
            {
                _experience -= _level * 125;
                ++_level;
            }
        }

        public Task CompleteTrail(double routeLength)    // routeLength in km with two numbers after point
        {
            return Task.Run(() => AddExperience((int)routeLength * 100));
        }

        public void CreateTrail(int exp)
        {
            AddExperience(exp);
        }
    }
}
