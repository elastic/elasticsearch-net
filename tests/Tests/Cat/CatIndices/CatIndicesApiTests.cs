// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
 using Elastic.Elasticsearch.Ephemeral;
 using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cat.CatIndices
{
	public class CatIndicesApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, CatResponse<CatIndicesRecord>, ICatIndicesRequest, CatIndicesDescriptor, CatIndicesRequest>
	{
		public CatIndicesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/indices";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Indices(f),
			(client, f) => client.Cat.IndicesAsync(f),
			(client, r) => client.Cat.Indices(r),
			(client, r) => client.Cat.IndicesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatIndicesRecord> response) =>
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));
	}

	public class CatIndicesApiNotFoundWithSecurityTests
		: ApiIntegrationTestBase<XPackCluster, CatResponse<CatIndicesRecord>, ICatIndicesRequest, CatIndicesDescriptor, CatIndicesRequest>
	{
		public CatIndicesApiNotFoundWithSecurityTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 403;

		protected override Func<CatIndicesDescriptor, ICatIndicesRequest> Fluent => f => f
			.Index("doesnot-exist-*")
			.RequestConfiguration(r => r.Authentication(new BasicAuthentication(ClusterAuthentication.User.Username, ClusterAuthentication.User.Password)));

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override CatIndicesRequest Initializer => new CatIndicesRequest("doesnot-exist-*")
		{
			RequestConfiguration = new RequestConfiguration
			{
				AuthenticationHeader = new BasicAuthentication(
					ClusterAuthentication.User.Username,
					ClusterAuthentication.User.Password)
			}
		};

		protected override string UrlPath => "/_cat/indices/doesnot-exist-%2A";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cat.Indices(f),
			(client, f) => client.Cat.IndicesAsync(f),
			(client, r) => client.Cat.Indices(r),
			(client, r) => client.Cat.IndicesAsync(r)
		);

		protected override void ExpectResponse(CatResponse<CatIndicesRecord> response)
		{
			response.Records.Should().BeEmpty();
			response.ApiCall.Should().NotBeNull();
		}
	}
}
