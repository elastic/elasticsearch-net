using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		IElasticsearchSerializer Serializer { get; }
		IElasticsearchClient Raw { get; }
		ElasticInferrer Infer { get; }

	}
}
