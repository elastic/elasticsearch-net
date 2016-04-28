using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.XPack.Security.User.PutUser
{
	[Collection(IntegrationContext.Shield)]
	[SkipVersion("<2.3.0", "")]
	public class PutUserApiTests : ApiIntegrationTestBase<IPutUserResponse, IPutUserRequest, PutUserDescriptor, PutUserRequest>
	{
		public PutUserApiTests(ShieldCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutUser(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.PutUserAsync(CallIsolatedValue, f),
			request: (client, r) => client.PutUser(r),
			requestAsync: (client, r) => client.PutUserAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath => $"/_shield/user/{CallIsolatedValue}";

		protected override bool SupportsDeserialization => false;

		private string Email => $"{CallIsolatedValue}@example.example";

		private string Password => CallIsolatedValue;

		protected override object ExpectJson => new
		{
			roles = new[] { "user" },
			password = this.Password,
			email = this.Email,
			full_name = this.CallIsolatedValue,
			metadata = new
			{
				x = CallIsolatedValue
			}
		};

		protected override PutUserRequest Initializer => new PutUserRequest(CallIsolatedValue)
		{
			Roles = new[] { "user" },
			Password = this.Password,
			Email = this.Email,
			FullName = CallIsolatedValue,
			Metadata = new Dictionary<string, object>
			{
				{ "x", CallIsolatedValue }
			}
		};

		protected override PutUserDescriptor NewDescriptor() => new PutUserDescriptor(CallIsolatedValue);

		protected override Func<PutUserDescriptor, IPutUserRequest> Fluent => d => d
			.Roles("user")
			.Password(this.Password)
			.Email(this.Email)
			.FullName(CallIsolatedValue)
			.Metadata(m => m
				.Add("x", CallIsolatedValue)
			);

		protected override void ExpectResponse(IPutUserResponse response)
		{
			response.User.Should().NotBeNull();
			response.User.Created.Should().BeTrue();
		}
	}

	[Collection(IntegrationContext.Shield)]
	public class PutUserRunAsApiTests : PutUserApiTests
	{
		public PutUserRunAsApiTests(ShieldCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 403;

		protected override PutUserRequest Initializer
		{
			get
			{
				var request = base.Initializer;
				request.RequestConfiguration = new RequestConfiguration
				{
					RunAs = ShieldInformation.User.Username
				};
				return request;
			}
		}

		protected override Func<PutUserDescriptor, IPutUserRequest> Fluent => f => base.Fluent(f
			.RequestConfiguration(c=>c
				.RunAs(ShieldInformation.User.Username)
			));

		protected override void ExpectResponse(IPutUserResponse response)
		{
		}
	}

}
