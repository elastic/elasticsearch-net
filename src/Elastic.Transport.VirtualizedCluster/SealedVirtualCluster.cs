// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport.VirtualizedCluster.Products;
using Elastic.Transport.VirtualizedCluster.Providers;

namespace Elastic.Transport.VirtualizedCluster
{
	public class SealedVirtualCluster
	{
		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly TestableDateTimeProvider _dateTimeProvider;
		private readonly IMockProductRegistration _productRegistration;

		public SealedVirtualCluster(VirtualCluster cluster, IConnectionPool pool, TestableDateTimeProvider dateTimeProvider, IMockProductRegistration productRegistration)
		{
			_connectionPool = pool;
			_connection = new VirtualClusterConnection(cluster, dateTimeProvider);
			_dateTimeProvider = dateTimeProvider;
			_productRegistration = productRegistration;
		}

		private ConnectionConfiguration CreateSettings() =>
			new ConnectionConfiguration(_connectionPool, _connection);

		public VirtualizedCluster AllDefaults() =>
			new VirtualizedCluster(_dateTimeProvider, CreateSettings(), _productRegistration);

		public VirtualizedCluster Settings(Func<ConnectionConfiguration, ConnectionConfiguration> selector) =>
			new VirtualizedCluster(_dateTimeProvider, selector(CreateSettings()), _productRegistration);

		public VirtualClusterConnection VirtualClusterConnection(Func<ConnectionConfiguration, ConnectionConfiguration> selector = null) =>
			new VirtualizedCluster(_dateTimeProvider, selector == null ? CreateSettings() : selector(CreateSettings()), _productRegistration)
				.Connection;
	}
}
