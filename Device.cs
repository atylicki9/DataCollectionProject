using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TylickiAaronDataCollector
{
    /// <summary>
    /// Device Class. Contains a single method for getting measurements: <see cref="GetMeasurement"/>
    /// </summary>
    public class Device
    {
        private Random randomInt = new Random();

        /// <summary>
        /// This method returns a random integer between 1 and 10 as a measurement of some imaginary object.
        /// </summary>
        /// <returns>Random integer between 1 and 10</returns>
        public int GetMeasurement()
        {
            return randomInt.Next(1, 11);
        }
    }
}
