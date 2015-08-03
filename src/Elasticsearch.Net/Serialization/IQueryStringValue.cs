using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net.Serialization
{
	public interface IQueryStringValue
	{
		string ToQueryStringValue(IConnectionConfigurationValues settings);
	}
}