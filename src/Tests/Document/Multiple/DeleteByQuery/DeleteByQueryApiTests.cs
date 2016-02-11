using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.DeleteByQuery
{
	[Collection(IntegrationContext.OwnIndex)]
	public class DeleteByQueryApiTests : ApiIntegrationTestBase<IDeleteByQueryResponse, IDeleteByQueryRequest, DeleteByQueryDescriptor<Project>, DeleteByQueryRequest>
	{
		public DeleteByQueryApiTests(OwnIndexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
		{
			foreach (var index in values.Values)
			{
				this.Client.IndexMany(Project.Projects, index);
				this.Client.Refresh(index);
			}
		}
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteByQuery(this.Indices, Types.All, f),
			fluentAsync: (client, f) => client.DeleteByQueryAsync(this.Indices, Types.All, f),
			request: (client, r) => client.DeleteByQuery(r),
			requestAsync: (client, r) => client.DeleteByQueryAsync(r)
		);
		protected override void OnAfterCall(IElasticClient client) => client.Refresh(CallIsolatedValue);

		private string SecondIndex => $"{CallIsolatedValue}-clone";
		private Nest.Indices Indices => Index(CallIsolatedValue).And(SecondIndex);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override string UrlPath => $"/{CallIsolatedValue}%2C{SecondIndex}/_query?ignore_unavailable=true";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new 
		{
			query = new
			{
				ids = new
				{
					types = new[] { "project" },
					values = new [] { Project.Projects.First().Name, "x" }
				}
			}
		};

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(this.Indices);

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.IgnoreUnavailable()
			.Query(q=>q
				.Ids(ids=>ids
					.Types(typeof(Project))
					.Values(Project.Projects.First().Name, "x")
				)
			);
			
		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(this.Indices)
		{
			IgnoreUnavailable = true,
			Query = new IdsQuery
			{
				Types = Types.Type<Project>(),
				Values = new Id[] { Project.Projects.First().Name, "x" }
			}
		};

		protected override void ExpectResponse(IDeleteByQueryResponse response)
		{
			response.Indices.Should().NotBeEmpty().And.HaveCount(2).And.ContainKey(CallIsolatedValue);
			response.Indices[CallIsolatedValue].Deleted.Should().Be(1);
			response.Indices[CallIsolatedValue].Found.Should().Be(1);
		}
	}
}
