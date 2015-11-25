using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	public partial interface IElasticClient
	{
		IElasticsearchSerializer Serializer { get; }
		IElasticsearchClient Raw { get; }
		ElasticInferrer Infer { get; }

	}
}
