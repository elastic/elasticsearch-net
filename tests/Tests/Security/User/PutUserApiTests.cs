//using System.Collections.Generic;
//using Elastic.Transport;
//using FluentAssertions;
//using Elastic.Clients.Elasticsearch;
//using Tests.Core.ManagedElasticsearch.Clusters;
//using Tests.Domain.Helpers;
//using Tests.Framework.EndpointTests;
//using Tests.Framework.EndpointTests.TestState;

//namespace Tests.Security.User
//{
//	public class PutUserApiTests : ApiIntegrationTestBase<SecurityCluster, PutUserResponse, IPutUserRequest,
//		PutUserDescriptor, PutUserRequest>
//	{
//		private static readonly string UserName = Gimme.Random.String(8, 'a', 'z');

//		public PutUserApiTests(SecurityCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

//		protected override bool SupportsDeserialization => false; // no parameterless ctor and STJ doesn't support an internal one.
//		protected override bool ExpectIsValid => true;
//		protected override int ExpectStatusCode => 200;
//		protected override HttpMethod HttpMethod => HttpMethod.PUT;
//		protected override string ExpectedUrlPathAndQuery => $"/_security/user/{UserName}";

//		protected override LazyResponses ClientUsage() => Calls(
//			//(client, f) => client.Cluster.Health(),
//			//(client, f) => client.Cluster.HealthAsync(),
//			(client, r) => client.Security.PutUser(r),
//			(client, r) => client.Security.PutUserAsync(r)
//		);

//		// TODO: Additional properties
//		protected override PutUserRequest Initializer => new(UserName)
//		{
//			Password = "ThisIsAPassword123!",
//			Roles = new[] { "Test" },
//			Enabled = true,
//			Metadata = new Dictionary<string, object>
//			{
//				{"key_one","value_one"}
//			}
//		};

//		protected override object ExpectJson => new
//		{
//			password = "ThisIsAPassword123!",
//			roles = new[]
//			{
//				"Test"
//			},
//			enabled = true,
//			metadata = new Dictionary<string, string>
//			{
//				{"key_one","value_one"}
//			}
//		};

//		protected override void ExpectResponse(PutUserResponse response)
//		{
//			response.IsValid.Should().BeTrue();
//			response.Created.Should().BeTrue();
//		}
//	}
//}


