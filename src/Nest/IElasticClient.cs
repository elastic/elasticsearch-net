using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IConnectionSettingsValues ConnectionSettings { get; }
		Inferrer Infer { get; }
		IElasticLowLevelClient LowLevel { get; }
		IElasticsearchSerializer Serializer { get; }
	}
}
