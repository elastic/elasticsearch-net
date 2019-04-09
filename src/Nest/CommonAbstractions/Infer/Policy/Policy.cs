using System;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public class Policy : IUrlParameter, IEquatable<Policy>
	{
		internal readonly string Value;

		public Policy(string value) => Value = value;

		public bool Equals(Policy other) => Value == other.Value;

		// ReSharper disable once ImpureMethodCallOnReadonlyValueField
		public string GetString(IConnectionConfigurationValues settings) => Value.ToString(CultureInfo.InvariantCulture);

		public static implicit operator Policy(string policy) => new Policy(policy);

		public static implicit operator string(Policy policy) => policy.Value;

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case string l: return Value == l;
				case Policy i: return Value == i.Value;
				default: return false;
			}
		}

		public override int GetHashCode() => Value.GetHashCode();

		public static bool operator ==(Policy left, Policy right) => Equals(left, right);

		public static bool operator !=(Policy left, Policy right) => !Equals(left, right);
	}
}
