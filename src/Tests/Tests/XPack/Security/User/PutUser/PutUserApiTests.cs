using System;
using System.Collections.Generic;
using Elastic.Managed.Ephemeral;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Security.User.PutUser
{
	[SkipVersion("<2.3.0", "")]
	public class PutUserApiTests : ApiIntegrationTestBase<XPackCluster, IPutUserResponse, IPutUserRequest, PutUserDescriptor, PutUserRequest>
	{
		public PutUserApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			roles = new[] { "user" },
			password = Password,
			email = Email,
			full_name = CallIsolatedValue,
			metadata = new
			{
				x = CallIsolatedValue
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutUserDescriptor, IPutUserRequest> Fluent => d => d
			.Roles("user")
			.Password(Password)
			.Email(Email)
			.FullName(CallIsolatedValue)
			.Metadata(m => m
				.Add("x", CallIsolatedValue)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutUserRequest Initializer => new PutUserRequest(CallIsolatedValue)
		{
			Roles = new[] { "user" },
			Password = Password,
			Email = Email,
			FullName = CallIsolatedValue,
			Metadata = new Dictionary<string, object>
			{
				{ "x", CallIsolatedValue }
			}
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/_security/user/{CallIsolatedValue}";

		private string Email => $"{CallIsolatedValue}@example.example";

		private string Password => CallIsolatedValue;

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.PutUser(CallIsolatedValue, f),
			(client, f) => client.PutUserAsync(CallIsolatedValue, f),
			(client, r) => client.PutUser(r),
			(client, r) => client.PutUserAsync(r)
		);

		protected override PutUserDescriptor NewDescriptor() => new PutUserDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(IPutUserResponse response)
		{
			response.User.Should().NotBeNull();
			response.User.Created.Should().BeTrue();
		}
	}

	public class PutUserRunAsApiTests : PutUserApiTests
	{
		public PutUserRunAsApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
			var x = Client.GetUser(new GetUserRequest(ClusterAuthentication.User.Username));
			var y = Client.GetRole(new GetRoleRequest(ClusterAuthentication.User.Role));
		}

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 403;

		protected override Func<PutUserDescriptor, IPutUserRequest> Fluent => f => base.Fluent(f
			.RequestConfiguration(c => c
				.RunAs(ClusterAuthentication.User.Username)
			));

		protected override PutUserRequest Initializer
		{
			get
			{
				var request = base.Initializer;
				request.RequestConfiguration = new RequestConfiguration
				{
					RunAs = ClusterAuthentication.User.Username
				};
				return request;
			}
		}

		protected override void ExpectResponse(IPutUserResponse response) { }
	}
}
