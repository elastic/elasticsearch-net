using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using static Elastic.Managed.Ephemeral.ClusterAuthentication;

namespace Tests.XPack.Security.User.SecurityInvalidateApiKey
{
	[SkipVersion("<6.7.0", "API has been implemented against Elasticsearch 6.7.0 request and response format.")]
	public class SecurityInvalidateApiKeyApiTests
		: ApiIntegrationTestBase<XPackCluster, ISecurityInvalidateApiKeyResponse, ISecurityInvalidateApiKeyRequest, SecurityInvalidateApiKeyDescriptor
			, SecurityInvalidateApiKeyRequest>
	{
		private const string ApiKey = "apikey";

		protected virtual string CurrentApiKey => RanIntegrationSetup ? ExtendedValue<string>(ApiKey) : "-";

		public SecurityInvalidateApiKeyApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void OnBeforeCall(IElasticClient client)
		{
			var r = client.SecurityCreateApiKey(f => f.Name(CallIsolatedValue));
			r.ShouldBeValid();
			ExtendedValue(ApiKey, r.ApiKey);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			id = CurrentApiKey
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> Fluent => d => d
			.Id(CurrentApiKey)
			;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override SecurityInvalidateApiKeyRequest Initializer => new SecurityInvalidateApiKeyRequest
		{
			Id = CurrentApiKey
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_security/api_key";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.SecurityInvalidateApiKey(f),
			(client, f) => client.SecurityInvalidateApiKeyAsync(f),
			(client, r) => client.SecurityInvalidateApiKey(r),
			(client, r) => client.SecurityInvalidateApiKeyAsync(r)
		);

		protected override SecurityInvalidateApiKeyDescriptor NewDescriptor() => new SecurityInvalidateApiKeyDescriptor();

		protected override void ExpectResponse(ISecurityInvalidateApiKeyResponse response)
		{
			response.ShouldBeValid();
			response.ErrorCount.Should().Be(0);
			response.ErrorDetails.Should().BeEmpty();
		}
	}
}
