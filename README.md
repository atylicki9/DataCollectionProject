# DataCollectionProject
Course Assignment for C# Course that I took in Summer 2023. Below are the instructions provided by the professor.


This must be a Universal Windows Application.

Define an interface
Define an interface called IMeasuringDevice. Add it to your project in its own IMeasuringDevice.cs source file (Project > Add New Item > Interface). Add the following public method declarations to your new interface:

MetricValue. This method will return a decimal that represents the metric value of the most recent measurement that was captured.
ImperialValue. This method will return a decimal that represents the imperial value of the most recent measurement that was captured.
StartCollecting. This method will start the device running. It will begin collecting measurements and record them.
StopCollecting. This method will stop the device. It will cease collecting measurements.
GetRawData. This method will retrieve a copy of all of the recent data that the measuring device has captured. The data will be returned as an array of integer values.
Comment your new method declarations so developers using your interface know what they are meant to do.

 

Define an enumeration
Define an enumeration called Units. Add it to your project in its own UnitsEnumeration.cs source file. You may do so by adding a new class (Project > Add New Item > Class) called UnitsEnumeration and changing the empty class declaration generated into an enum declaration ("class UnitsEnumeration" -> "enum Units"). Make your enumeration publicly accessible, and add values Metric and Imperial to it. Comment your enumeration so its purpose is clear.

 

Define a Device class
Define a class called Device. Add it to your project in its own Device.cs source file. Make it publicly accessible and give it one method:

GetMeasurement. This method will return a random integer between 1 and 10 as a measurement of some imaginary object.
Comment your class so developers using it know what it does and how to use it.

 

Create the MeasureLengthDevice class
Define a class called MeasureLengthDevice. Add it to your project in its own MeasureLengthDevice.cs source file. This class will implement the IMeasuringDevice interface. You may generate stubs for your implementation methods using the Implement Interface Wizard.

Add the following private fields to your class (as well as any others you deem necessary):

unitsToUse: Units - This field will determine whether the generated measurements are interpreted in metric (e.g. centimeters) or imperial (e.g. inches) units. Its value will be determined from user input.
dataCaptured: integer array - This field will store a history of a limited set of recently captured measurements. Once the array is full, the class should start overwriting the oldest elements while continuing to record the newest captures. (You may need some helper fields/variables to go with this one).
mostRecentMeasure: integer - This field will store the most recent measurement captured for convenience of display.
Add a constructor for your class that initializes your fields.

Add a means for unitsToUse to be set according to user input.

Give your class access to the Device.GetMeasurement() method you created. Should the relationship between MeasureLengthDevice and Device be is-a or has-a?

Write function bodies for your interface methods:

MetricValue. Return the current value from mostRecentMeasure- convert it if unitsToUse is not Metric.
ImperialValue. Return the current value from mostRecentMeasure- convert it if unitsToUse is not Imperial.
StartCollecting. Start a timer (using System.Windows.Threading.DispatcherTimer) to perform a data capture every 15 seconds. The timer will call an EventHandler (hooked up to the Tick event) which should set mostRecentMeasurewith a new value fromDevice.GetMeasurement() and add that value to the dataCaptured history array. Each time this event occurs, you will also need to update your form by displaying the new measurement along with a current timestamp. Should your device also capture a value before the first 15-second interval has elapsed?
StopCollecting. Stop the timer that started in StartCollecting().
GetRawData. Return the contents of the dataCapturedarray. Which element is the oldest one in your history? Do you need to manipulate these values? How will they be presented to the user?
Build the user interface
In your main window create an instance of your MeasureLengthDevice class. This object will be accessed and manipulated by the controls on your form. Where/when should this object be created?

Add controls to your main window form:

Give the user a way to set the units of measurement (unitsToUse).

Provide a mechanism to start and stop the measurement cycle (StartCollecting / StopCollecting). Should your interface change when these controls are clicked? Are all of the control options valid in both states (collecting / not collecting)? What happens when the user tries to use the interface in ways you may not have expected?

Create places (labels) to display the current measurement (mostRecentMeasure) and the timestamp when it was taken.

Create a way to display the most recent measurement in the alternate to the selected measurement units (MetricValue / ImperialValue). That is, if the selected units are metric, show the measurement in imperial, and vice-versa. Which units are being collected? Where/how will you display the converted value?

Add a button to display the measurement history (GetRawData). Where/how will you display this list? What did you make GetRawData() return? Does the list start with the oldest value collected? What happens when your history array fills up and older values are overwritten? What happens when your history has not been filled up yet? Does "0" count as an actual history entry? Does your history display the units in which the data was collected?
