using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Cat.CatThreadPool
{
	public class CatThreadPoolApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatThreadPoolRecord>, ICatThreadPoolRequest, CatThreadPoolDescriptor, CatThreadPoolRequest>
	{
		public CatThreadPoolApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatThreadPool(),
			fluentAsync: (client, f) => client.CatThreadPoolAsync(),
			request: (client, r) => client.CatThreadPool(r),
			requestAsync: (client, r) => client.CatThreadPoolAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/thread_pool";

	}

	public class CatThreadPoolFullApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ICatResponse<CatThreadPoolRecord>, ICatThreadPoolRequest, CatThreadPoolDescriptor, CatThreadPoolRequest>
	{
		public CatThreadPoolFullApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatThreadPool(f),
			fluentAsync: (client, f) => client.CatThreadPoolAsync(f),
			request: (client, r) => client.CatThreadPool(r),
			requestAsync: (client, r) => client.CatThreadPoolAsync(r)
		);

		protected override Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> Fluent => f => f.Headers("*");

		protected override CatThreadPoolRequest Initializer { get; } = new CatThreadPoolRequest
		{
			Headers = new[] { "*" }
		};

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/thread_pool?h=%2A";

		protected override void ExpectResponse(ICatResponse<CatThreadPoolRecord> response)
		{
			response.Records.Should().NotBeNull();


			foreach (var r in response.Records)
			{
				r.EphemeralNodeId.Should().NotBeNullOrWhiteSpace();
				r.Host.Should().NotBeNullOrWhiteSpace();
				r.Ip.Should().NotBeNullOrWhiteSpace();
				r.Maximum.Should().BeGreaterThan(0);
				r.Minimum.Should().BeGreaterThan(0);
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
