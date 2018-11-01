using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface IConnection : IDisposable
	{
		TResponse Request<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new();

		Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse, new();
	}
}
