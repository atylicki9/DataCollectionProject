using System;
using System.Collections.Concurrent;
using System.Threading;
using Windows.UI.Xaml;

namespace TylickiAaronDataCollector
{
    public class MeasureLengthDevice : Device, IMeasuringDevice
    {
        /// <summary>
        /// Conversion from in to cm -> 1 inch = 2.54 cm.
        /// </summary>
        private const decimal ConversionRate = 2.54m;
        /// <summary>
        /// Length of the <see cref="dataCaptured"/> array.
        /// </summary>
        private const int ArrayLength = 10;

        /// <summary>
        /// This field will determine whether the generated measurements are interpreted 
        /// in metric (e.g. centimeters) or imperial (e.g. inches) units. Its value will be 
        /// determined from user input.
        /// </summary>
        private Units unitsToUse;

        /// <summary>
        /// This field will store a history of a limited set of recently captured measurements. 
        /// Once the array is full, the class will start overwriting the oldest elements while 
        /// continuing to record the newest captures.
        /// </summary>
        private int[] dataCaptured;

        /// <summary>
        /// This field stores the most recent measurement captured for convenience of display.
        /// </summary>
        private int mostRecentMeasure;

        /// <summary>
        /// Event Timer.
        /// </summary>
        public Timer Timer;

        /// <summary>
        /// Constructor for MeasureLengthDevice.
        /// </summary>
        public MeasureLengthDevice()
        {
            unitsToUse = Units.Metric;
            dataCaptured = new int[ArrayLength];
            mostRecentMeasure = dataCaptured[0];
        }

        /// <summary>
        /// This method will return the most recent measurement as a metric measurement.
        /// </summary>
        public decimal MetricValue()
        {
            if (unitsToUse == Units.Imperial)
            {
                return mostRecentMeasure * ConversionRate;
            }
            else
            {
                return mostRecentMeasure;
            }
        }

        /// <summary>
        /// This method will return the most recent measurement as a imperial measurement.
        /// </summary>
        public decimal ImperialValue()
        {
            if (unitsToUse == Units.Metric)
                return mostRecentMeasure / ConversionRate;
            else
                return mostRecentMeasure;
        }

        /// <summary>
        /// Public Getter for current measurement units
        /// </summary>
        /// <returns>Current Measurement <see cref="Units"/></returns>
        public Units GetCurrentUnits() => unitsToUse;

        /// <summary>
        /// Converts Units to use on the <see cref="MeasureLengthDevice"/>
        /// </summary>
        public void UpdateUnitsToUse()
        {
            if (unitsToUse == Units.Imperial)
                unitsToUse = Units.Metric;
            else
                unitsToUse = Units.Imperial;
        }

        /// <summary>
        /// Start a timer (using System.Windows.Threading.DispatcherTimer) to perform a data capture every 15 seconds.
        /// The timer will call an EventHandler (hooked up to the Tick event) which should set 
        /// mostRecentMeasurewith a new value fromDevice.GetMeasurement() and add that value to the 
        /// dataCaptured history array.
        /// </summary>
        public void StartCollecting()
        {
            CaptureMeasurement(); // Capture an initial measurement before the first 15-second interval
        }

        /// <summary>
        /// Stop the timer that started in StartCollecting().
        /// </summary>
        public void StopCollecting()
        {
            Timer.Dispose();
        }

        /// <summary>
        /// Return the contents of the dataCapturedarray. 
        /// </summary>
        public int[] GetRawData() => dataCaptured;

        /// <summary>
        /// This method captures a new measurement and adds it to dataCaptured array.
        /// </summary>
        public void CaptureMeasurement()
        {
            mostRecentMeasure = GetMeasurement();
            RefreshDataCapturedArray();
        }

        /// <summary>
        /// This method adds the most recent measurement to the data captured array and 
        /// pushes other measurements back one spot.
        /// </summary>
        private void RefreshDataCapturedArray()
        {
            var updatedDataCaptured = new int[ArrayLength];

            var currentIndex = 0;
            updatedDataCaptured[0] = mostRecentMeasure;

            while (currentIndex < ArrayLength - 1)
            {
                updatedDataCaptured[currentIndex + 1] = dataCaptured[currentIndex];
                currentIndex++;
            }

            dataCaptured = updatedDataCaptured;
        }
    }
}
