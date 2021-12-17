using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary.DTO
{
    public class SaveDto
    {
        public string Name { get; set; }

        public int AuthorId { get; set; }

        public double[] Coords { get; set; }

    }
}