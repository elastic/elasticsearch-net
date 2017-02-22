using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest_5_2_0
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Names : IUrlParameter
	{
		private readonly IEnumerable<Name> _names;

		public Names(IEnumerable<string> names)
		{
			if (!names.HasAny()) throw new ArgumentException("can not create Names on an empty enumerable of string", nameof(names));
			this._names = names.Select(n => (Name)n);
		}

		public Names(IEnumerable<Name> names) { this._names = names; }

		public static Names Parse(string names)
		{
			if (names.IsNullOrEmpty()) throw new ArgumentException("can not create Names on an empty enumerable of string", nameof(names));
			var nameList = names.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s=>s.Trim());
			return new Names(nameList);
		}

		//TODO to explicit private implemenation
		public string GetString(IConnectionConfigurationValues settings) =>
			string.Join(",", this._names.Select(n => n.GetString(settings)));

		private string DebugDisplay => GetString(null);

		public static implicit operator Names(Name name) => new Names(new[] { name });

		public static implicit operator Names(string names) => Parse(names);
	}
}
