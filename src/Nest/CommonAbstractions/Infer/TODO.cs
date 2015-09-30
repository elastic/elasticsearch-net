using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;

namespace Nest
{
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



	public class PropertyNames : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

	public class FieldNames : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}

		public static implicit operator FieldNames(string[] fields)
		{
			throw new NotImplementedException();
		}
	}

}
