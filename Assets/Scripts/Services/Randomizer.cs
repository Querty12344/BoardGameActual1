using System;

namespace Services
{
    public class Randomizer
    {
        public static Random rng = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
    }
}