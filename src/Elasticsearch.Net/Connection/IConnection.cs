using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface IConnection : IDisposable
	{
		Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData, CancellationToken cancellationToken)
			where TReturn : class;

		ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData)
			where TReturn : class;
	}
}
