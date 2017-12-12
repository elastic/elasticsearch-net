using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Tests.Framework
{
    public class WaitingInMemoryConnection : InMemoryConnection
    {
        private readonly TimeSpan _waitTime;

        public WaitingInMemoryConnection(TimeSpan waitTime, byte[] responseBody, int statusCode = 200, Exception exception = null)
            : base (responseBody, statusCode, exception)
        {
            this._waitTime = waitTime;
        }

        public override TResponse Request<TResponse>(RequestData requestData)
        {
            Thread.Sleep(_waitTime);
            return base.Request<TResponse>(requestData);
        }

        public override async Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
        {
            await Task.Delay(_waitTime, cancellationToken);
            return await base.RequestAsync<TResponse>(requestData, cancellationToken);
        }
    }
}
