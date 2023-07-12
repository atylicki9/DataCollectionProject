using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TylickiAaronDataCollector
{
    public interface IMeasuringDevice
    {
        /// <summary>
        /// This method returns a decimal that represents the metric value of the most recent measurement that was captured.
        /// </summary>
        decimal MetricValue();

        /// <summary>
        /// This method returns a decimal that represents the imperial value of the most recent measurement that was captured.
        /// </summary>
        decimal ImperialValue();

        /// <summary>
        /// This method starts the device running. It will begin collecting measurements and record them.
        /// </summary>
        void StartCollecting();

        /// <summary>
        /// This method stops the device. It will cease collecting measurements.
        /// </summary>
        void StopCollecting();

        /// <summary>
        /// This method retrieves a copy of all of the recent data that the measuring device has captured. The data will be returned as an Int[]
        /// </summary>
        int[] GetRawData();
    }
}
