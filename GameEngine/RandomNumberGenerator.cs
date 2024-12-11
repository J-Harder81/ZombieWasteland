using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    // Define the RandomNumberGenerator class to generate random numbers needed for gameplay
    public static class RandomNumberGenerator
    {
        private static Random _generator = new Random();

        // Method to generate a random number between a specified minimum and maximum value (inclusive)
        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            return _generator.Next(minimumValue, maximumValue + 1);
        }
    }
}
