using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WalkAndTravel.ClassLibrary
{
    public static class NameValidator
    {
        public static Boolean isValid(string name)
        {
            Regex regex = new Regex(@"^[\s\w.#-]+$");
            return regex.IsMatch(name);

        }
    }
}
