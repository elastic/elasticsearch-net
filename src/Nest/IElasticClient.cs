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

		ElasticsearchResponse<T> DoRequest<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where T : class;

		Task<ElasticsearchResponse<T>> DoRequestAsync<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where T : class;
	}
}
