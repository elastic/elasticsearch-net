using System;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public class LongId : IUrlParameter, IEquatable<LongId>
	{
		internal readonly long Value;

		public LongId(long value) => Value = value;

		public bool Equals(LongId other) => Value == other.Value;

		// ReSharper disable once ImpureMethodCallOnReadonlyValueField
		public string GetString(IConnectionConfigurationValues settings) => Value.ToString(CultureInfo.InvariantCulture);

		public static implicit operator LongId(long value) => new LongId(value);

		public static implicit operator long(LongId value) => value.Value;

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case int l: return Value == l;
				case long l: return Value == l;
				case LongId i: return Value == i.Value;
				default: return false;
			}
		}

		public override int GetHashCode() => Value.GetHashCode();

		public static bool operator ==(LongId left, LongId right) => Equals(left, right);

		public static bool operator !=(LongId left, LongId right) => !Equals(left, right);
	}
}
