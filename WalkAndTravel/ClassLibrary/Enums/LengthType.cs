using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    [Flags]
    public enum LengthType
    {
        None = 0,
        Short = 1 << 0,
        Medium = Short << 1,
        Long = Medium << 1,
        VeryLong = Long << 1
    }
}
