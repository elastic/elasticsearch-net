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
		private const string AccessTokenValueKey = "accesstoken";

		protected virtual string CurrentAccessToken => RanIntegrationSetup ? ExtendedValue<string>(AccessTokenValueKey) : "foo";

		public SecurityInvalidateApiKeyApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void OnBeforeCall(IElasticClient client)
		{
			var r = client.GetUserAccessToken(Admin.Username, Admin.Password, f => f.Scope("FULL").GrantType(AccessTokenGrantType.Password));
			r.ShouldBeValid();
			ExtendedValue(AccessTokenValueKey, r.AccessToken);
		}

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			token = CurrentAccessToken
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> Fluent => d => d
			.Token(CurrentAccessToken)
			;

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override SecurityInvalidateApiKeyRequest Initializer => new SecurityInvalidateApiKeyRequest
		{
			Token = CurrentAccessToken
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_xpack/security/oauth2/token";

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
			response.Created.Should().BeTrue();
			response.ErrorCount.Should().Be(0);
			response.ErrorDetails.Should().BeEmpty();
			response.InvalidatedTokens.Should().Be(1);
			response.PreviousInvalidatedTokens.Should().Be(0);
		}
	}
}
