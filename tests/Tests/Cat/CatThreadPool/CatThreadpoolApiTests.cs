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
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatThreadPool
{
	public class CatThreadPoolApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatThreadPoolRecord>, ICatThreadPoolRequest, CatThreadPoolDescriptor,
			CatThreadPoolRequest>
	{
		public CatThreadPoolApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/thread_pool";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.ThreadPool(),
			(client, f) => client.Cat.ThreadPoolAsync(),
			(client, r) => client.Cat.ThreadPool(r),
			(client, r) => client.Cat.ThreadPoolAsync(r)
		);
	}

	public class CatThreadPoolFullApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatThreadPoolRecord>, ICatThreadPoolRequest, CatThreadPoolDescriptor,
			CatThreadPoolRequest>
	{
		public CatThreadPoolFullApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> Fluent => f => f.Headers("*");
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override CatThreadPoolRequest Initializer { get; } = new CatThreadPoolRequest
		{
			Headers = new[] { "*" }
		};

		protected override string UrlPath => "/_cat/thread_pool?h=%2A";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.ThreadPool(f),
			(client, f) => client.Cat.ThreadPoolAsync(f),
			(client, r) => client.Cat.ThreadPool(r),
			(client, r) => client.Cat.ThreadPoolAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatThreadPoolRecord> response)
		{
			response.Records.Should().NotBeNull();

			foreach (var r in response.Records)
			{
				r.EphemeralNodeId.Should().NotBeNullOrWhiteSpace();
				r.Host.Should().NotBeNullOrWhiteSpace();
				r.Ip.Should().NotBeNullOrWhiteSpace();
				r.Name.Should().NotBeNullOrWhiteSpace();
				r.NodeId.Should().NotBeNullOrWhiteSpace();
				r.NodeName.Should().NotBeNullOrWhiteSpace();
				r.Port.Should().BeGreaterThan(0);
				r.ProcessId.Should().BeGreaterThan(0);
				r.Type.Should().NotBeNullOrWhiteSpace();
			}

			response.Records.Should().Contain(r => r.KeepAlive != null);
		}
	}
}
