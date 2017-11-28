using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface ITransport<out TConnectionSettings>
		where TConnectionSettings : IConnectionConfigurationValues
	{
		TConnectionSettings Settings { get; }

		ElasticsearchResponse<T> Request<T>(HttpMethod method, string path, PostData data = null, IRequestParameters requestParameters = null)
			where T : class;
		Task<ElasticsearchResponse<T>> RequestAsync<T>(HttpMethod method, string path, CancellationToken cancellationToken, PostData data = null, IRequestParameters requestParameters = null)
			where T : class;
	}

}
