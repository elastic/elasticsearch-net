using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public partial interface IElasticLowLevelClient
	{
		/// <summary>
		/// Perform any request you want over the configured IConnection synchronously while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="TResponse">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>An ElasticsearchResponse of T where T represents the JSON response body</returns>
		TResponse DoRequest<TResponse>(HttpMethod method, string path, PostData data = null, IRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new();

		/// <summary>
		/// Perform any request you want over the configured IConnection asynchronously while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="TResponse">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>A task of ElasticsearchResponse of T where T represents the JSON response body</returns>
		Task<TResponse> DoRequestAsync<TResponse>(HttpMethod method, string path, CancellationToken cancellationToken, PostData data = null,
			IRequestParameters requestParameters = null
		)
			where TResponse : class, IElasticsearchResponse, new();
	}
}
