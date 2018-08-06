using System;
using Elastic.Managed.Ephemeral;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Cat.CatIndices
{
	public class CatIndicesApiTests : ApiIntegrationTestBase<ReadOnlyCluster,ICatResponse<CatIndicesRecord>, ICatIndicesRequest, CatIndicesDescriptor, CatIndicesRequest>
	{
		public CatIndicesApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatIndices(),
			fluentAsync: (client, f) => client.CatIndicesAsync(),
			request: (client, r) => client.CatIndices(r),
			requestAsync: (client, r) => client.CatIndicesAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/indices";

		protected override void ExpectResponse(ICatResponse<CatIndicesRecord> response)
		{
			response.Records.Should().NotBeEmpty().And.Contain(a => !string.IsNullOrEmpty(a.Status));
		}
	}

	public class CatIndicesApiNotFoundWithSecurityTests : ApiIntegrationTestBase<XPackCluster,ICatResponse<CatIndicesRecord>, ICatIndicesRequest, CatIndicesDescriptor, CatIndicesRequest>
	{
		public CatIndicesApiNotFoundWithSecurityTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CatIndices(f),
			fluentAsync: (client, f) => client.CatIndicesAsync(f),
			request: (client, r) => client.CatIndices(r),
			requestAsync: (client, r) => client.CatIndicesAsync(r)
		);

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 403;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cat/indices/doesnot-exist-%2A";

		protected override Func<CatIndicesDescriptor, ICatIndicesRequest> Fluent => f => f
			.Index("doesnot-exist-*")
			.RequestConfiguration(r=>r.BasicAuthentication(ClusterAuthentication.User.Username, ClusterAuthentication.User.Password));

		protected override CatIndicesRequest Initializer => new CatIndicesRequest("doesnot-exist-*")
		{
			RequestConfiguration = new RequestConfiguration
			{
				BasicAuthenticationCredentials = new BasicAuthenticationCredentials
				{
					Username = ClusterAuthentication.User.Username,
					Password = ClusterAuthentication.User.Password,
				}
			}
		};

		protected override void ExpectResponse(ICatResponse<CatIndicesRecord> response)
		{
			response.Records.Should().BeEmpty();
			response.ApiCall.Should().NotBeNull();
		}
	}
}
