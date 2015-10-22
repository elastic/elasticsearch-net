using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Document.Multiple.DeleteByQuery
{
	[CollectionDefinition(IntegrationContext.DeleteByQuery)]
	public class DeleteByQueryCluster : ClusterBase, ICollectionFixture<DeleteByQueryCluster>, IClassFixture<EndpointUsage> { } 

	[Collection(IntegrationContext.DeleteByQuery)]
	public class DeleteByQueryApiTests : ApiIntegrationTestBase<IDeleteByQueryResponse, IDeleteByQueryRequest, DeleteByQueryDescriptor<Project>, DeleteByQueryRequest>
	{
		public DeleteByQueryApiTests(DeleteByQueryCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientCall, string> values)
		{
			foreach (var index in values.Values)
			{
				this.Client.IndexMany(Project.Projects, index);
				this.Client.Refresh(index);
			}
		}
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteByQuery(this.Indices, f),
			fluentAsync: (client, f) => client.DeleteByQueryAsync(this.Indices, f),
			request: (client, r) => client.DeleteByQuery(r),
			requestAsync: (client, r) => client.DeleteByQueryAsync(r)
		);
		protected override void OnAfterCall(IElasticClient client) => client.Refresh(CallIsolatedValue);

		private Nest.Indices Indices => Index(CallIsolatedValue).And("x");

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/{CallIsolatedValue},x/_query?ignore_unavailable";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new 
		{
			query = new
			{
				ids = new
				{
					 values = new [] { Project.Projects.First().Name, "x" }
				}
			}
		};

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(this.Indices);

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.IgnoreUnavailable()
			.Query(q=>q
				.Ids(ids=>ids.Values(Project.Projects.First().Name, "x"))
			);
			
		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(this.Indices)
		{
			IgnoreUnavailable = true,
			Query = new IdsQuery
			{
				Values = new [] { Project.Projects.First().Name, "x" }
			}
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.Indices.Should().NotBeEmpty().And.HaveCount(2).And.ContainKey(CallIsolatedValue);
			r.Indices[CallIsolatedValue].Deleted.Should().Be(1);
			r.Indices[CallIsolatedValue].Found.Should().Be(1);
		});
	}
}
