using System.Diagnostics;
using Elasticsearch.Net;

namespace Nest
{
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class Name : IUrlParameter
	{
		private readonly string _name;
		public Name(string name) { this._name = name; }

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => _name;

		private string DebugDisplay => _name;

		public static implicit operator Name(string name) => new Name(name);
	}
}
