// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
 using Elastic.Elasticsearch.Ephemeral;
 using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Security.User.PutUser
{
	[SkipVersion("<2.3.0", "")]
	public class PutUserApiTests : ApiIntegrationTestBase<Security, PutUserResponse, IPutUserRequest, PutUserDescriptor, PutUserRequest>
	{
		public PutUserApiTests(Security cluster, EndpointUsage usage) : base(cluster, usage) { }

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
			(client, f) => client.Security.PutUser(CallIsolatedValue, f),
			(client, f) => client.Security.PutUserAsync(CallIsolatedValue, f),
			(client, r) => client.Security.PutUser(r),
			(client, r) => client.Security.PutUserAsync(r)
		);

		protected override PutUserDescriptor NewDescriptor() => new PutUserDescriptor(CallIsolatedValue);

		protected override void ExpectResponse(PutUserResponse response) =>
			response.Created.Should().BeTrue("{0}", response.DebugInformation);
	}

	public class PutUserRunAsApiTests : PutUserApiTests
	{
		public PutUserRunAsApiTests(Security cluster, EndpointUsage usage) : base(cluster, usage)
		{
			// ReSharper disable VirtualMemberCallInConstructor
			var x = Client.Security.GetUser(new GetUserRequest(ClusterAuthentication.User.Username));
			var y = Client.Security.GetRole(new GetRoleRequest(ClusterAuthentication.User.Role));
			// ReSharper restore VirtualMemberCallInConstructor
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

		protected override void ExpectResponse(PutUserResponse response) { }
	}
}
