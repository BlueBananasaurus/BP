using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_GL
{
    public static class CustomMath
    {
        public static float RandomAroundZero(Random rnd)
        {
            return (float)((rnd.NextDouble() - 0.5f) * 2);
        }
    }
}
