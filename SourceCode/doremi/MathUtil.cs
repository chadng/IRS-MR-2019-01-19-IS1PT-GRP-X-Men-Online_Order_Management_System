using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doremi
{
    public static class MathUtil
    {
        public static double TruncateDouble(double d)
        {
            return Math.Truncate(d * 100) / 100;
        }
    }
}