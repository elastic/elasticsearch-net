using System;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public class PolicyId : IUrlParameter, IEquatable<PolicyId>
	{
		internal readonly string Value;

		public PolicyId(string value) => Value = value;

		public bool Equals(PolicyId other) => Value == other.Value;

		// ReSharper disable once ImpureMethodCallOnReadonlyValueField
		public string GetString(IConnectionConfigurationValues settings) => Value.ToString(CultureInfo.InvariantCulture);

		public static implicit operator PolicyId(string policy) => new PolicyId(policy);

		public static implicit operator string(PolicyId policy) => policy.Value;

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case string l: return Value == l;
				case PolicyId i: return Value == i.Value;
				default: return false;
			}
		}

		public override int GetHashCode() => Value.GetHashCode();

		public static bool operator ==(PolicyId left, PolicyId right) => Equals(left, right);

		public static bool operator !=(PolicyId left, PolicyId right) => !Equals(left, right);
	}
}
