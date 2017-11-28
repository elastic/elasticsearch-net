using System;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum)]
	public class StringEnumAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Property)]
	public class StringTimeSpanAttribute : Attribute { }

	/// <summary>
	/// Signals that this date time property is used in Machine learning API's some of which will always return the date as epoch.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MachineLearningDateTimeAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Property)]
	public class IgnoreAttribute : Attribute { }

	[AttributeUsage(AttributeTargets.Property)]
	public class RenameAttribute : Attribute
	{
		public string Name { get; set; }
		public RenameAttribute(string name) => Name = name;
	}
}
