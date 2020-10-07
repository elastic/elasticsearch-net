// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Transport.Products
{
	public class ProductRegistration : IProductRegistration
	{
		public static IProductRegistration Default { get; } = new ProductRegistration();

		public string Name { get; } = "elastic-transport-net";

		public bool SupportsPing { get; } = false;
		public bool SupportsSniff { get; } = false;

		public RequestData CreateSniffRequestData(Node node, IRequestConfiguration requestConfiguration, ITransportConfigurationValues settings, IMemoryStreamFactory memoryStreamFactory) =>
			throw new NotImplementedException();

		public Task<Tuple<IApiCallDetails, IReadOnlyCollection<Node>>> SniffAsync(IConnection connection, bool forceSsl, RequestData requestData, CancellationToken cancellationToken) =>
			throw new NotImplementedException();

		public Tuple<IApiCallDetails, IReadOnlyCollection<Node>> Sniff(IConnection connection, bool forceSsl, RequestData requestData) =>
			throw new NotImplementedException();

		public int SniffOrder(Node node) => throw new NotImplementedException();

		public bool NodePredicate(Node node) => true;

		public RequestData CreatePingRequestData(Node node, RequestConfiguration requestConfiguration, ITransportConfigurationValues global, IMemoryStreamFactory memoryStreamFactory) =>
			throw new NotImplementedException();

		public Task<IApiCallDetails> PingAsync(IConnection connection, RequestData pingData, CancellationToken cancellationToken) =>
			throw new NotImplementedException();

		public IApiCallDetails Ping(IConnection connection, RequestData pingData) =>
			throw new NotImplementedException();
	}
}
