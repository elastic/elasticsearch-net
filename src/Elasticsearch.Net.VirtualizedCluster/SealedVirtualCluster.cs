/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elasticsearch.Net.VirtualizedCluster.Providers;

namespace Elasticsearch.Net.VirtualizedCluster
{
	public class SealedVirtualCluster
	{
		private readonly IConnection _connection;
		private readonly IConnectionPool _connectionPool;
		private readonly TestableDateTimeProvider _dateTimeProvider;

		public SealedVirtualCluster(VirtualCluster cluster, IConnectionPool pool, TestableDateTimeProvider dateTimeProvider)
		{
			_connectionPool = pool;
			_connection = new VirtualClusterConnection(cluster, dateTimeProvider);
			_dateTimeProvider = dateTimeProvider;
		}

		private ConnectionConfiguration CreateSettings() =>
			new ConnectionConfiguration(_connectionPool, _connection);

		public VirtualizedCluster AllDefaults() =>
			new VirtualizedCluster(_dateTimeProvider, CreateSettings());

		public VirtualizedCluster Settings(Func<ConnectionConfiguration, ConnectionConfiguration> selector) =>
			new VirtualizedCluster(_dateTimeProvider, selector(CreateSettings()));
		
		public VirtualClusterConnection VirtualClusterConnection(Func<ConnectionConfiguration, ConnectionConfiguration> selector = null) =>
			new VirtualizedCluster(_dateTimeProvider, selector == null ? CreateSettings() : selector(CreateSettings()))
				.Connection;
	}
}
