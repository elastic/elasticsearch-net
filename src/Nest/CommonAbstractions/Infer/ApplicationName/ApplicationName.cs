using System;
using System.Diagnostics;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class ApplicationName : IEquatable<ApplicationName>, IUrlParameter
	{
		public ApplicationName(string name) => Value = name?.Trim();

		internal string Value { get; }

		private string DebugDisplay => Value;

		private static int TypeHashCode { get; } = typeof(ApplicationName).GetHashCode();

		public bool Equals(ApplicationName other) => EqualsString(other?.Value);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => Value;

		public static implicit operator ApplicationName(string name) => name.IsNullOrEmpty() ? null : new ApplicationName(name);

		public static bool operator ==(ApplicationName left, ApplicationName right) => Equals(left, right);

		public static bool operator !=(ApplicationName left, ApplicationName right) => !Equals(left, right);

		public override bool Equals(object obj) =>
			obj is string s ? EqualsString(s) : obj is ApplicationName i && EqualsString(i.Value);

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other.Trim() == Value;

		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (Value?.GetHashCode() ?? 0);
				return result;
			}
		}
	}
}
