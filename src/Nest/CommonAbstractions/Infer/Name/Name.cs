using System;
using System.Diagnostics;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Name : IEquatable<Name>, IUrlParameter
	{
		private readonly string _name;
		internal string Value => _name;
		private string DebugDisplay => _name;

		public Name(string name) => this._name = name?.Trim();

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => _name;

		public static implicit operator Name(string name) => name.IsNullOrEmpty() ? null : new Name(name);

		public static bool operator ==(Name left, Name right) => Equals(left, right);

		public static bool operator !=(Name left, Name right) => !Equals(left, right);

		public bool Equals(Name other) => EqualsString(other?.Value);

		public override bool Equals(object obj) =>
			obj is string s ? this.EqualsString(s) : (obj is Name i) && this.EqualsString(i.Value);

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other.Trim() == this.Value;

		private static int TypeHashCode { get; } = typeof(Name).GetHashCode();
		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (this.Value?.GetHashCode() ?? 0);
				return result;
			}
		}
	}
}
