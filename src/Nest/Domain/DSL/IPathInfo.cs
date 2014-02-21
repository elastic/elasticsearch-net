using Elasticsearch.Net;

namespace Nest
{
	interface IPathInfo<K> where K : FluentQueryString<K>, new()
	{
		ElasticsearchPathInfo<K> ToPathInfo(IConnectionSettingsValues settings);
	}
}