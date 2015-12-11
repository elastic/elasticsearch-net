using System;

namespace Nest.CommonOptions.Attributes
{
	public class AlternativeEnumMemberAttribute : Attribute
	{
		public AlternativeEnumMemberAttribute(string value)
		{
			Value = value;
		}

		public string Value { get; private set; }
	}
}