// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
