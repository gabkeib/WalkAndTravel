using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Models
{
    public class RoutesCounter
    {
        public String Type
        {
            get; set;
        }

        public int Count
        {
            get; set;
        }

        public RoutesCounter(String type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}
