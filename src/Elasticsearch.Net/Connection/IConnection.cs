// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface IConnection : IDisposable
	{
		Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
			where TResponse : class, IElasticsearchResponse, new();

		TResponse Request<TResponse>(RequestData requestData)
			where TResponse : class, IElasticsearchResponse, new();
	}
}
