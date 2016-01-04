using System;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class AlternativeEnumMemberAttribute : Attribute
	{
		public AlternativeEnumMemberAttribute(string value)
		{
			Value = value;
		}

		public string Value { get; private set; }
	}
}