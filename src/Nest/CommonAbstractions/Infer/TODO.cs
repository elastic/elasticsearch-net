using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	public class ScrollIds : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class ScrollId : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
		public static implicit operator ScrollId(string id) => new ScrollId();
	}

	public class NodeIds : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}

		public static implicit operator NodeIds(string nodes)
		{
			throw new NotImplementedException();
		}
	}

	public class Metrics : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class Features : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class Feature : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class Names : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}

		public static implicit operator Names(Name name)
		{
			throw new NotImplementedException();
		}

		public static implicit operator Names(string names)
		{
			throw new NotImplementedException();
		}
	}

	public class Name : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
		public static implicit operator Name(string name) => new Name();
	}

	public class PropertyNames : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}
	

}
