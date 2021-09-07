//// Licensed to Elasticsearch B.V under one or more agreements.
//// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
//// See the LICENSE file in the project root for more information

//using System.Threading.Tasks;
//using Elastic.Elasticsearch.Xunit.XunitPlumbing;
//using FluentAssertions;
//using Elastic.Clients.Elasticsearch;
//using Tests.Core.ManagedElasticsearch.Clusters;
//using Tests.Framework.EndpointTests;
//using Tests.Framework.EndpointTests.TestState;

//namespace Tests.Security
//{
//	public class SecurityCoordinatedUserTests : CoordinatedIntegrationTestBase<XPackCluster>
//	{
//		private const string PutUserStep = nameof(PutUserStep);

//		public SecurityCoordinatedUserTests(SecurityCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
//		{
//			{
//				PutUserStep,  u =>
//					u.Calls<PutUserDescriptor, PutUserRequest, IPutUserRequest, PutUserResponse>(
//						v => new PutUserRequest($"user-{v}")
//						{
//							Password = "password",
//							Roles = new[] { "superuser" },
//							//FullName = "API key superuser"
//						},
//						//(v, d) => d, // TODO
//						//(v, c, f) => c.Security.PutUser($"user-{v}", f),
//						//(v, c, f) => c.Security.PutUserAsync($"user-{v}", f),
//						(v, c, r) => c.Security.PutUser(r),
//						(v, c, r) => c.Security.PutUserAsync(r)
//					)
//			}
//		})
//		{ }

//		[I] public async Task SecurityCreateApiKeyResponse() => await Assert<PutUserResponse>(PutUserStep, r =>
//		{
//			r.IsValid.Should().BeTrue();
//			r.Created.Should().BeTrue();
//		});
//	}
//}
