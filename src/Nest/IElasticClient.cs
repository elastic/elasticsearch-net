using System;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient : IDisposable
	{
		IConnectionSettingsValues ConnectionSettings { get; }
		IElasticsearchSerializer Serializer { get; }
		IElasticsearchClient Raw { get; }
		ElasticInferrer Infer { get; }
	}
}
