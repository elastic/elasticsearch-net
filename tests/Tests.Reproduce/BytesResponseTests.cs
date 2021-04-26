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
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Reproduce
{
	public class BytesResponseTests : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public BytesResponseTests(ReadOnlyCluster cluster) => _cluster = cluster;

		[I] public void NonNullBytesResponse()
		{
			var client = _cluster.Client;

			var bytesResponse = client.LowLevel.Search<BytesResponse>("project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(bytesResponse.ResponseBodyInBytes);
		}

		[I] public void NonNullBytesLowLevelResponse()
		{
			var settings = new ConnectionConfiguration(new Uri($"http://localhost:{_cluster.Nodes.First().Port ?? 9200}"));
			var lowLevelClient = new ElasticLowLevelClient(settings);

			var bytesResponse = lowLevelClient.Search<BytesResponse>("project", PostData.Serializable(new { }));

			bytesResponse.Body.Should().NotBeNull();
			bytesResponse.Body.Should().BeEquivalentTo(bytesResponse.ResponseBodyInBytes);
		}
	}
}
