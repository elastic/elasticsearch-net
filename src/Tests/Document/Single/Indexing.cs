using System;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Tests.Search
{
	[Collection(IntegrationContext.Indexing)]
	public class IndexingUsage : EndpointUsageBase<IIndexResponse, IIndexRequest<Project>, IndexDescriptor<Project>, IndexRequest<Project>>
	{
		public IndexingUsage(IndexingCluster cluster, ApiUsage usage) : base(cluster, usage) { }

		public override string UrlPath => "/project/project/SomeProject?consistency=all&op_type=create&refresh=true&routing=route";
		public override bool ExpectIsValid => true;
		public override int ExpectStatusCode => 200;
		public override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Index<Project>(this.Document, f),
			fluentAsync: (client, f) => client.IndexAsync<Project>(this.Document, f),
			request: (client, r) => client.Index<Project>(r),
			requestAsync: (client, r) => client.IndexAsync<Project>(r)
		);

		protected int ProjectId => 1337;

		public Project Document => new Project
		{
			Name = "SomeProject",
			CuratedTags = new List<Tag> { new Tag { Name = "x", Added = FixedDate } }
		};

		protected override object ExpectJson =>
			new { explain = true };

		protected override Func<IndexDescriptor<Project>, IIndexRequest<Project>> Fluent => s => s
			.Consistency(Consistency.All)
			.OpType(OpType.Create)
			.Refresh()
			.Routing("route");

		protected override IndexRequest<Project> Initializer =>
			new IndexRequest<Project>(this.Document)
			{
				Refresh = true,
				OpType = OpType.Create,
				Consistency = Consistency.All,
				Routing = "route"
			};

	}
}
