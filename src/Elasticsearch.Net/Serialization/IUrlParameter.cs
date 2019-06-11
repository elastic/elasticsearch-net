using System;
using System.Globalization;

namespace Elasticsearch.Net
{
	public interface IUrlParameter
	{
		string GetString(IConnectionConfigurationValues settings);
	}

	internal class StringUrlParameter : IUrlParameter
	{
		private string Value { get; }

		public StringUrlParameter(string value) => Value = value;

		public string GetString(IConnectionConfigurationValues settings) => Value;

		public static explicit operator string(StringUrlParameter ps) => ps.ToString();

		public override string ToString() => Value;
	}
	
	internal class LongUrlParameter : IUrlParameter
	{
		private long? Value { get; }

		public LongUrlParameter(long? value) => Value = value;

		public string GetString(IConnectionConfigurationValues settings) => ToString();

		public static explicit operator long?(LongUrlParameter ps) => ps.Value;
		public static explicit operator long(LongUrlParameter ps) => ps.Value.GetValueOrDefault();
		public static explicit operator string(LongUrlParameter ps) => ps.ToString();

		public override string ToString() => !Value.HasValue 
			? string.Empty
			: Value.Value.ToString(CultureInfo.InvariantCulture);
	}
	
}
