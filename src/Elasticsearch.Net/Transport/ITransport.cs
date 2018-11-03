using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface ITransport<out TConnectionSettings>
		where TConnectionSettings : IConnectionConfigurationValues

	{
		TConnectionSettings Settings { get; }

		TResponse Request<TResponse>(HttpMethod method, string path, PostData data = null, IRequestParameters requestParameters = null)
			where TResponse : class, IElasticsearchResponse, new();

		Task<TResponse> RequestAsync<TResponse>(
			HttpMethod method, string path, CancellationToken ctx, PostData data = null, IRequestParameters requestParameters = null
		)
			where TResponse : class, IElasticsearchResponse, new();
	}
}
