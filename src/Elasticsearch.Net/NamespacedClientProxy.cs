using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public class NamespacedClientProxy
	{
		private readonly ElasticLowLevelClient _client;

		internal NamespacedClientProxy(ElasticLowLevelClient client) => _client = client;

		protected TResponse DoRequest<TResponse>(HttpMethod post, string url, PostData body, IRequestParameters @params)
			where TResponse : class, IElasticsearchResponse, new() =>
			_client.DoRequest<TResponse>(post, url, body, @params);

		protected Task<TResponse> DoRequestAsync<TResponse>(HttpMethod post, string url, CancellationToken ctx, PostData body, IRequestParameters @params)
			where TResponse : class, IElasticsearchResponse, new() =>
			_client.DoRequestAsync<TResponse>(post, url, ctx, body, @params);

		protected string Url(FormattableString formattable) => _client.Url(formattable);

		protected TRequestParams RequestParams<TRequestParams>(TRequestParams requestParams)
			where TRequestParams : class, IRequestParameters, new()
			=> _client.RequestParams(requestParams, ContentType, ContentType);
		
		protected virtual string ContentType { get; }
	}
}
