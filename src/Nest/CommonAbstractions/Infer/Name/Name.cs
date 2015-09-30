using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class Name : IUrlParameter
	{
		private readonly string _name;
		public Name(string name) { this._name = name; }

		public string GetString(IConnectionConfigurationValues settings) => _name;

		public static implicit operator Name(string name) => new Name(name);
	}
}
