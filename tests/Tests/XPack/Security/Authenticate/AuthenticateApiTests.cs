// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
 using Elastic.Elasticsearch.Ephemeral;
 using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Elastic.Elasticsearch.Ephemeral.ClusterAuthentication;

namespace Tests.XPack.Security.Authenticate
{
	[SkipVersion(">=8.0.0-SNAPSHOT", "TODO investigate")]
	public class AuthenticateApiTests
		: ApiIntegrationTestBase<XPackCluster, AuthenticateResponse, IAuthenticateRequest, AuthenticateDescriptor, AuthenticateRequest>
	{
		public AuthenticateApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => $"/_security/_authenticate";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Security.Authenticate(f),
			(client, f) => client.Security.AuthenticateAsync(f),
			(client, r) => client.Security.Authenticate(r),
			(client, r) => client.Security.AuthenticateAsync(r)
		);

		protected override void ExpectResponse(AuthenticateResponse response)
		{
			response.Username.Should().Be(Admin.Username);
			response.Roles.Should().Contain(Admin.Role);
		}
	}

	[SkipVersion("<2.3.0", "")]
	public class AuthenticateRequestConfigurationApiTests : AuthenticateApiTests
	{
		public AuthenticateRequestConfigurationApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<AuthenticateDescriptor, IAuthenticateRequest> Fluent => f => f
			.RequestConfiguration(c => c
				.Authentication(new BasicAuthentication(ClusterAuthentication.User.Username, ClusterAuthentication.User.Password))
			);

		protected override AuthenticateRequest Initializer => new AuthenticateRequest
		{
			RequestConfiguration = new RequestConfiguration
			{
				AuthenticationHeader = new BasicAuthentication(
					ClusterAuthentication.User.Username,
					ClusterAuthentication.User.Password)
			}
		};

		protected override void ExpectResponse(AuthenticateResponse response)
		{
			response.Username.Should().Be(ClusterAuthentication.User.Username);
			response.Roles.Should().Contain(ClusterAuthentication.User.Role);
		}
	}
}
