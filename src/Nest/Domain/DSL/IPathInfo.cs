using Elasticsearch.Net;

namespace Nest
{
	interface IPathInfo<K> where K : FluentRequestParameters<K>, new()
	{
		ElasticsearchPathInfo<K> ToPathInfo(IConnectionSettingsValues settings);
	}
}