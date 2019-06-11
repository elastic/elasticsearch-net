using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class StringIds : IUrlParameter, IEquatable<StringIds>
	{
		private readonly List<string> _stringIds;

		public StringIds(IEnumerable<string> value) => _stringIds = value?.ToList();

		public StringIds(string value)
		{
			if (!value.IsNullOrEmptyCommaSeparatedList(out var arr))
				_stringIds = arr.ToList();
		}

		private string DebugDisplay => ((IUrlParameter)this).GetString(null);

		public bool Equals(StringIds other)
		{
			if (other == null) return false;
			if (_stringIds == null && other._stringIds == null) return true;
			if (_stringIds == null || other._stringIds == null) return false;

			return _stringIds.Count == other._stringIds.Count &&
				_stringIds.OrderBy(id => id).SequenceEqual(other._stringIds.OrderBy(id => id));
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) =>
			string.Join(",", _stringIds ?? Enumerable.Empty<string>());

		public static implicit operator StringIds(string value) =>
			value.IsNullOrEmptyCommaSeparatedList(out var arr) ? null : new StringIds(arr);

		public static implicit operator StringIds(string[] value) =>
			value.IsEmpty() ? null : new StringIds(value);

		public override bool Equals(object obj) => obj is StringIds other && Equals(other);

		public override int GetHashCode()
		{
			if (_stringIds == null) return 0;
			unchecked
			{
				var hc = 0;
				foreach (var id in _stringIds.OrderBy(id => id))
					hc = hc * 17 + id.GetHashCode();
				return hc;
			}
		}

		public static bool operator ==(StringIds left, StringIds right) => Equals(left, right);

		public static bool operator !=(StringIds left, StringIds right) => !Equals(left, right);
	}
}
