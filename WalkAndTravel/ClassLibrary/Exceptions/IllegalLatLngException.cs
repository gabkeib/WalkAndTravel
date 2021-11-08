using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.Exceptions
{
    public class IllegalLatLngException : Exception
    {
        public IllegalLatLngException()
        {

        }

        public IllegalLatLngException(string message) : base(message)
        {

        }

        public IllegalLatLngException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
}
