using System.Diagnostics;
using Elasticsearch.Net_5_2_0;

namespace Nest_5_2_0
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Name : IUrlParameter
	{
		private readonly string _name;
		public Name(string name) { this._name = name; }

		//TODO to explicit private implemenation
		public string GetString(IConnectionConfigurationValues settings) => _name;

		private string DebugDisplay => GetString(null);

		public static implicit operator Name(string name) => new Name(name);
	}
}
