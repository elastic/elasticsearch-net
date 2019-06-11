using System;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public class StringId : IUrlParameter, IEquatable<StringId>
	{
		internal readonly string Value;

		public StringId(string value) => Value = value;

		public bool Equals(StringId other) => Value == other.Value;

		// ReSharper disable once ImpureMethodCallOnReadonlyValueField
		public string GetString(IConnectionConfigurationValues settings) => Value.ToString(CultureInfo.InvariantCulture);

		public static implicit operator StringId(string value) => new StringId(value);

		public static implicit operator string(StringId value) => value.Value;

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case string l: return Value == l;
				case StringId i: return Value == i.Value;
				default: return false;
			}
		}

		public override int GetHashCode() => Value.GetHashCode();

		public static bool operator ==(StringId left, StringId right) => Equals(left, right);

		public static bool operator !=(StringId left, StringId right) => !Equals(left, right);
	}
}
