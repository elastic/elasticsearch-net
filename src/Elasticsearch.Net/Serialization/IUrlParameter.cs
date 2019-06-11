using System;
using System.Globalization;

namespace Elasticsearch.Net
{
	public interface IUrlParameter
	{
		string GetString(IConnectionConfigurationValues settings);
	}
}
