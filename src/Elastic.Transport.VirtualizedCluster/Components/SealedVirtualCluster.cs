// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport.VirtualizedCluster.Products;
using Elastic.Transport.VirtualizedCluster.Providers;

namespace Elastic.Transport.VirtualizedCluster.Components
{
	public class SealedVirtualCluster
	{
		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly TestableDateTimeProvider _dateTimeProvider;
		private readonly IMockProductRegistration _productRegistration;

		internal SealedVirtualCluster(VirtualCluster cluster, IConnectionPool pool, TestableDateTimeProvider dateTimeProvider, IMockProductRegistration productRegistration)
		{
			_connectionPool = pool;
			_connection = new VirtualClusterConnection(cluster, dateTimeProvider);
			_dateTimeProvider = dateTimeProvider;
			_productRegistration = productRegistration;
		}

		private TransportConfiguration CreateSettings() =>
			new TransportConfiguration(_connectionPool, _connection, serializer: null, _productRegistration.ProductRegistration);

		public VirtualizedCluster AllDefaults() =>
			new VirtualizedCluster(_dateTimeProvider, CreateSettings());

		public VirtualizedCluster Settings(Func<TransportConfiguration, TransportConfiguration> selector) =>
			new VirtualizedCluster(_dateTimeProvider, selector(CreateSettings()));

		public VirtualClusterConnection VirtualClusterConnection(Func<TransportConfiguration, TransportConfiguration> selector = null) =>
			new VirtualizedCluster(_dateTimeProvider, selector == null ? CreateSettings() : selector(CreateSettings()))
				.Connection;
	}
}
