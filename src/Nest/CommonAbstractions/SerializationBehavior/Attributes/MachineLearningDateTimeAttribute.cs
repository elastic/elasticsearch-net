using System;

namespace Nest
{
	/// <summary>
	/// Signals that this date time property is used in Machine learning API's some of which will always return the date as epoch.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	internal class MachineLearningDateTimeAttribute : Attribute { }
}
