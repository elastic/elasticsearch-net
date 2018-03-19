using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Names : IEquatable<Names>, IUrlParameter
	{
		private readonly IList<Name> _names;
		internal IList<Name> Value => _names;

		public Names(IEnumerable<string> names) : this(names?.Select(n=>(Name)n).ToList()) { }

		public Names(IEnumerable<Name> names)
		{
			this._names = names?.ToList();
			if (!this._names.HasAny())
				throw new ArgumentException($"can not create {nameof(Names)} on an empty enumerable of ", nameof(names));
		}

		public static Names Parse(string names) => names.IsNullOrEmptyCommaSeparatedList(out var list) ? null : new Names(list);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) =>
			string.Join(",", this._names.Cast<IUrlParameter>().Select(n => n.GetString(settings)));

		private string DebugDisplay => ((IUrlParameter)this).GetString(null);

		public static implicit operator Names(Name name) => name == null ? null : new Names(new[] { name });

		public static implicit operator Names(string names) => Parse(names);

		public static implicit operator Names(string[] names) => names.IsEmpty() ? null : new Names(names);

		public static bool operator ==(Names left, Names right) => Equals(left, right);

		public static bool operator !=(Names left, Names right) => !Equals(left, right);

		public bool Equals(Names other) => EqualsAllIds(this.Value, other.Value);

		private static bool EqualsAllIds(ICollection<Name> thisIds, ICollection<Name> otherIds)
		{
			if (thisIds == null && otherIds == null) return true;
			if (thisIds == null || otherIds == null) return false;
			if (thisIds.Count != otherIds.Count) return false;
			return thisIds.Count == otherIds.Count && !thisIds.Except(otherIds).Any();
		}

		public override bool Equals(object obj) => obj is string s ? this.Equals(Parse(s)) : (obj is Names i) && this.Equals(i);

		public override int GetHashCode() => this._names.GetHashCode();
	}
}
