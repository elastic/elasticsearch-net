using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.AliasManagement.AliasExists
{
	public class AliasExistsApiTests
		: ApiIntegrationTestBase<WritableCluster, IExistsResponse, IAliasExistsRequest, AliasExistsDescriptor, AliasExistsRequest>
	{
		public AliasExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
				client.CreateIndex(index, c => c
					.Aliases(aa => aa.Alias(index + "-alias"))
				);
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.AliasExists(f),
			fluentAsync: (client, f) => client.AliasExistsAsync(f),
			request: (client, r) => client.AliasExists(r),
			requestAsync: (client, r) => client.AliasExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/_alias/{CallIsolatedValue}-alias";

		protected override bool SupportsDeserialization => false;

		protected override Func<AliasExistsDescriptor, IAliasExistsRequest> Fluent => d => d
			.Name(CallIsolatedValue + "-alias");

		protected override AliasExistsRequest Initializer => new AliasExistsRequest(Names(CallIsolatedValue + "-alias"));

		protected override void ExpectResponse(IExistsResponse response)
		{
			response.Exists.Should().BeTrue();
		}
	}

	public class AliasExistsNotFoundApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IExistsResponse, IAliasExistsRequest, AliasExistsDescriptor, AliasExistsRequest>

	{
		public AliasExistsNotFoundApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.AliasExists(f),
			fluentAsync: (client, f) => client.AliasExistsAsync(f),
			request: (client, r) => client.AliasExists(r),
			requestAsync: (client, r) => client.AliasExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/_alias/unknown-alias";

		protected override bool SupportsDeserialization => false;

		protected override Func<AliasExistsDescriptor, IAliasExistsRequest> Fluent => d => d
			.Name("unknown-alias");

		protected override AliasExistsRequest Initializer => new AliasExistsRequest(Names("unknown-alias"));

		protected override void ExpectResponse(IExistsResponse response)
		{
			response.ServerError.Should().BeNull();
			response.Exists.Should().BeFalse();
		}
	}
}
