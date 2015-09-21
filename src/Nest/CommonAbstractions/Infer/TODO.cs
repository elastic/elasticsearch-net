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

	public class Names : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
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
	}

	public class PropertyNames : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings)
		{
			throw new NotImplementedException();
		}
	}

}
