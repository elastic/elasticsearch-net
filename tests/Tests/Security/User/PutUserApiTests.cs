// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain.Helpers;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Security.User
{
	public class PutUserApiTests : ApiIntegrationTestBase<SecurityCluster, PutUserResponse, IPutUserRequest,
		PutUserDescriptor, PutUserRequest>
	{
		private static readonly string UserName = Gimme.Random.String(8, 'a', 'z');

		public PutUserApiTests(SecurityCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string ExpectedUrlPathAndQuery => $"/_security/user/{UserName}";
		
		protected override LazyResponses ClientUsage() => Calls(
			//(client, f) => client.Cluster.Health(),
			//(client, f) => client.Cluster.HealthAsync(),
			(client, r) => client.Security.PutUser(r),
			(client, r) => client.Security.PutUserAsync(r)
		);

		// TODO: Additional properties
		protected override PutUserRequest Initializer => new(UserName)
		{
			Password = "ThisIsAPassword123!"
		}; 

		protected override object ExpectJson => new
		{
			password = "ThisIsAPassword123"
		};

		protected override void ExpectResponse(PutUserResponse response)
		{
			response.IsValid.Should().BeTrue();
			response.Created.Should().BeTrue();
		}
	}
}
