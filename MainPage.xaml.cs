using System;
using System.Diagnostics;
using System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TylickiAaronDataCollector
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Timer interval (in milliseconds).
        /// </summary>
        public const int TimerInterval = 15000;

        /// <summary>
        /// Local instance of <see cref="MeasureLengthDevice"/>.
        /// </summary>
        private MeasureLengthDevice _measureLengthDevice;

        /// <summary>
        /// Instantiates page.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            _measureLengthDevice= new MeasureLengthDevice();

            StopCollectingButton.IsEnabled = false;
            MetricUnitsButton.IsEnabled = false; // default is metric units 

            MeasurementHistory.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// This method executes when <see cref="StartCollectingButton"/> is clicked
        /// </summary>
        private void StartCollecting(object sender, RoutedEventArgs e)
        {
            StartCollectingButton.IsEnabled = false;
            StopCollectingButton.IsEnabled = true;
            _measureLengthDevice.Timer = new Timer(Timer_Tick, null, TimerInterval, TimerInterval);
            _measureLengthDevice.StartCollecting();
            UpdateMeasurementInfoOnUI();
        }

        /// <summary>
        /// This method executes a <see cref="CaptureMeasurementOnTick"/> on each interval of <see cref="MeasureLengthDevice.Timer"/>. <para/>
        /// This code was a mix of Professor's example code and this article on
        /// <see href="https://stackoverflow.com/questions/34271100/timer-in-uwp-app-which-isnt-linked-to-the-ui">Stack Overflow.</see>
        /// </summary>
        /// <param name="state"></param>
        private async void Timer_Tick(object state) 
            => await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher
                .RunAsync(CoreDispatcherPriority.Normal, () => { CaptureMeasurementOnTick(); });

        /// <summary>
        /// This method captures a measurement and updates the Main Page.
        /// </summary>
        /// <returns></returns>
        private void CaptureMeasurementOnTick()
        {
            _measureLengthDevice.CaptureMeasurement();
            UpdateMeasurementInfoOnUI();
        }

        /// <summary>
        /// This method executes when <see cref="StopCollectingButton"/> is clicked
        /// </summary>
        private void StopCollecting(object sender, RoutedEventArgs e)
        {
            StartCollectingButton.IsEnabled = true;
            StopCollectingButton.IsEnabled = false;

            _measureLengthDevice.StopCollecting();
        }

        /// <summary>
        /// This method will execute when <see cref="MetricUnitsButton"/> is clicked and will set 
        /// <see cref="MeasureLengthDevice.unitsToUse"/> to <see cref="Units.Metric"/>
        /// </summary>
        private void SetUnitsMetric(object sender, RoutedEventArgs e)
        {
            MetricUnitsButton.IsEnabled = false;
            ImperialUnitsButton.IsEnabled = true;

            DisplayConversionValue();
        }

        /// <summary>
        /// This method will execute when <see cref="ImperialUnitsButton"/> is clicked and will set 
        /// <see cref="MeasureLengthDevice.unitsToUse"/> to <see cref="Units.Imperial"/>
        /// </summary>
        private void SetUnitsImperial(object sender, RoutedEventArgs e)
        {
            ImperialUnitsButton.IsEnabled = false;
            MetricUnitsButton.IsEnabled = true;

            DisplayConversionValue();
        }

        /// <summary>
        /// This method changes the measurement system of the Data Collector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeMeasurementSystem(object sender, RoutedEventArgs e)
        {
            _measureLengthDevice.UpdateUnitsToUse();

            CurrentUnitsText.Text = $"Current Units: {_measureLengthDevice.GetCurrentUnits()}";

            UpdateMeasurementInfoOnUI();
        }

        /// <summary>
        /// This method will show or hide the measurement history.
        /// </summary>
        private void ToggleMeasurementHistory(object sender, RoutedEventArgs e)
        {
            if (MeasurementHistory.Visibility == Visibility.Collapsed)
            {
                ToggleMeasurementHistoryButton.Content = "Hide Measurement History";
                MeasurementHistory.Text = DisplayMeasurementHistory(_measureLengthDevice.GetRawData());
                MeasurementHistory.Visibility = Visibility.Visible;
            }

            else
            {
                ToggleMeasurementHistoryButton.Content = "Show Measurement History";
                MeasurementHistory.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// This method updates the measurement values that appear on the UI
        /// </summary>
        private void UpdateMeasurementInfoOnUI()
        {
            var currentData = _measureLengthDevice.GetRawData();

            if(MeasurementHistory.Visibility == Visibility.Visible )
                MeasurementHistory.Text = DisplayMeasurementHistory(currentData);

            MostRecentMeasurement.Text = currentData[0].ToString();
            RecentMeasurementTimestamp.Text = DateTime.Now.ToString();

            DisplayConversionValue();
        }

        /// <summary>
        /// Displays measurement history.
        /// </summary>
        /// <param name="measurementHistory">Array of measurements</param>
        /// <returns>String containing measurement history.</returns>
        private string DisplayMeasurementHistory(int[] measurementHistory)
        {
            string rawDataString = null;
            if (measurementHistory[0] == 0)
                return "No Data has been collected yet.";

            foreach (var measurement in measurementHistory)
            {
                if (measurement != 0)
                {
                    rawDataString += $"{measurement} ";
                }
            }

            return rawDataString;
        }

        /// <summary>
        /// Displays Conversion Value text based on which Conversion button is selected
        /// </summary>
        private void DisplayConversionValue()
        {
            if (MetricUnitsButton.IsEnabled == true)
                ConversionValue.Text = $"Conversion: {_measureLengthDevice.ImperialValue().ToString("#.##")} in";
            else
                ConversionValue.Text = $"Conversion: {_measureLengthDevice.MetricValue().ToString("#.##")} cm";
        }
    }
}
