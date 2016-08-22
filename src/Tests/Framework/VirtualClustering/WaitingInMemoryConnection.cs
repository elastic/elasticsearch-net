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

        public override ElasticsearchResponse<TReturn> Request<TReturn>(RequestData requestData)
        {
            Thread.Sleep(_waitTime);
            return base.Request<TReturn>(requestData);
        }

        public override async Task<ElasticsearchResponse<TReturn>> RequestAsync<TReturn>(RequestData requestData, CancellationToken cancellationToken)
        {
            await Task.Delay(_waitTime);
            return await base.RequestAsync<TReturn>(requestData, cancellationToken);
        }
    }
}