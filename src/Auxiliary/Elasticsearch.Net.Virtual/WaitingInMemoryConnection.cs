using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Virtual
{
	public class WaitingInMemoryConnection : InMemoryConnection
	{
		private readonly TimeSpan _waitTime;

		public WaitingInMemoryConnection(TimeSpan waitTime, byte[] responseBody, int statusCode = 200, Exception exception = null)
			: base(responseBody, statusCode, exception) => _waitTime = waitTime;

		public override TResponse Request<TResponse>(RequestData requestData)
		{
			Thread.Sleep(_waitTime);
			return base.Request<TResponse>(requestData);
		}

		public override async Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
		{
			await Task.Delay(_waitTime, cancellationToken).ConfigureAwait(false);
			return await base.RequestAsync<TResponse>(requestData, cancellationToken).ConfigureAwait(false);
		}
	}
}
