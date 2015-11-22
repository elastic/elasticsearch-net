using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.Serialization
{
	public interface IUrlParameter
	{
		string GetString(IConnectionConfigurationValues settings);
	}
}