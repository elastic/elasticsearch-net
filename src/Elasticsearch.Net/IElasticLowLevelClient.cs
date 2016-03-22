using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public partial interface IElasticLowLevelClient
	{
		/// <summary>
		/// Perform any request you want over the configured IConnection synchronously while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="T">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>An ElasticsearchResponse of T where T represents the JSON response body</returns>
		ElasticsearchResponse<T> DoRequest<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where T : class;

		/// <summary>
		/// Perform any request you want over the configured IConnection asynchronously while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="T">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>A task of ElasticsearchResponse of T where T represents the JSON response body</returns>
		Task<ElasticsearchResponse<T>> DoRequestAsync<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where T : class;
	}
}
