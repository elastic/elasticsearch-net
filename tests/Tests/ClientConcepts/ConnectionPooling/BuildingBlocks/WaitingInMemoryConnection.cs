// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Tests.ClientConcepts.ConnectionPooling.BuildingBlocks
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
