// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Transport.Products
{
	public interface IProductRegistration
	{
		bool SupportsPing { get; }
		bool SupportsSniff { get; }


		RequestData CreatePingRequestData(Node node, RequestConfiguration requestConfiguration, IConnectionConfigurationValues global, IMemoryStreamFactory memoryStreamFactory);

		Task<IApiCallDetails> PingAsync(IConnection connection, RequestData pingData, CancellationToken cancellationToken);

		IApiCallDetails Ping(IConnection connection, RequestData pingData);

		RequestData CreateSniffRequestData(Node node, IRequestConfiguration requestConfiguration, IConnectionConfigurationValues settings,
			IMemoryStreamFactory memoryStreamFactory
		);

		Task<Tuple<IApiCallDetails, IReadOnlyCollection<Node>>> SniffAsync(IConnection connection, bool forceSsl, RequestData requestData, CancellationToken cancellationToken);

		Tuple<IApiCallDetails, IReadOnlyCollection<Node>> Sniff(IConnection connection, bool forceSsl, RequestData requestData);
	}
}
