using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Elastic.Managed.Ephemeral.ClusterAuthentication;

namespace Tests.XPack.Security.User.InvalidateUserAccessToken
{
	[SkipVersion("<5.5.0", "")]
	public class InvalidateUserAccessTokenApiTests : ApiIntegrationTestBase<XPackCluster, IInvalidateUserAccessTokenResponse, IInvalidateUserAccessTokenRequest,
		InvalidateUserAccessTokenDescriptor, InvalidateUserAccessTokenRequest>
	{
		protected const string AccessTokenValueKey = "accesstoken";
		protected override void OnBeforeCall(IElasticClient client)
		{
			var r = client.GetUserAccessToken(Admin.Username, Admin.Password);
			r.ShouldBeValid();
			this.ExtendedValue(AccessTokenValueKey, r.AccessToken);
		}
		protected virtual string CurrentAccessToken => this.RanIntegrationSetup ? this.ExtendedValue<string>(AccessTokenValueKey) : "foo";

		public InvalidateUserAccessTokenApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.InvalidateUserAccessToken(CurrentAccessToken, f),
			fluentAsync: (client, f) => client.InvalidateUserAccessTokenAsync(CurrentAccessToken, f),
			request: (client, r) => client.InvalidateUserAccessToken(r),
			requestAsync: (client, r) => client.InvalidateUserAccessTokenAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override string UrlPath => "/_xpack/security/oauth2/token";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			token = CurrentAccessToken
		};

		protected override InvalidateUserAccessTokenDescriptor NewDescriptor() => new InvalidateUserAccessTokenDescriptor(CurrentAccessToken);
		protected override InvalidateUserAccessTokenRequest Initializer => new InvalidateUserAccessTokenRequest(CurrentAccessToken)
		{
		};

		protected override Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> Fluent => d => d;

		protected override void ExpectResponse(IInvalidateUserAccessTokenResponse response)
		{
			response.Created.Should().BeTrue();
		}
	}

	public class InvalidateUserAccessTokenBadPasswordApiTests : InvalidateUserAccessTokenApiTests
	{
		public InvalidateUserAccessTokenBadPasswordApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 401;
		protected override string CurrentAccessToken => "bad_password";

		protected override void ExpectResponse(IInvalidateUserAccessTokenResponse response)
		{
			response.ServerError.Should().NotBeNull();
		}
	}

}
