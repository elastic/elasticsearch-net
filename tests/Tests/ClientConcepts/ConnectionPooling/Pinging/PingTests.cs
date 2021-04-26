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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.ClientConcepts.Connection;
using Tests.Core.ManagedElasticsearch.Clusters;


namespace Tests.ClientConcepts.ConnectionPooling.Pinging
{
	public class PingTests : IClusterFixture<ReadOnlyCluster>
	{

		// ReSharper disable once UnusedParameter.Local
		public PingTests(ReadOnlyCluster cluster) { }

#if DOTNETCORE
		[I]
		public void UsesRelativePathForPing()
		{
			var pool = new StaticConnectionPool(new[] { new Uri("http://localhost:9200/elasticsearch/") });
			var settings = new ConnectionSettings(pool,
				new HttpConnectionTests.TestableHttpConnection(response =>
				{
					response.RequestMessage.RequestUri.AbsolutePath.Should().StartWith("/elasticsearch/");
				}));

			var client = new ElasticClient(settings);
			var healthResponse = client.Ping();
		}
#else
		[I]
		public void UsesRelativePathForPing()
		{
			var pool = new StaticConnectionPool(new[] { new Uri("http://localhost:9200/elasticsearch/") });
			var connection = new HttpWebRequestConnectionTests.TestableHttpWebRequestConnection();
			var settings = new ConnectionSettings(pool, connection);

			var client = new ElasticClient(settings);
			var healthResponse = client.Ping();

			connection.LastRequest.Address.AbsolutePath.Should().StartWith("/elasticsearch/");
		}
#endif
	}
}

