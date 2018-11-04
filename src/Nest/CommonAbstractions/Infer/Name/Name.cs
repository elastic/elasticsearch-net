using System.Diagnostics;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Name : IUrlParameter
	{
		private readonly string _name;

		public Name(string name) => _name = name;

		private string DebugDisplay => GetString(null);

		//TODO to explicit private implemenation
		public string GetString(IConnectionConfigurationValues settings) => _name;

		public static implicit operator Name(string name) => new Name(name);
	}
}
