using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Document.Multiple.Bulk
{
	public class BulkUpdateManyTests : ApiTestBase<ReadOnlyCluster, IBulkResponse, IBulkRequest, BulkDescriptor, BulkRequest>
	{
		private readonly List<Project> Updates = Project.Projects.Take(10).ToList();

		public BulkUpdateManyTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => Updates.SelectMany<Project, object>(ProjectToBulkJson);

		protected override Func<BulkDescriptor, IBulkRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.UpdateMany(Updates, (b, u) => b.Script(s => s.Source("_source.counter++")));

		protected override HttpMethod HttpMethod => HttpMethod.POST;


		protected override BulkRequest Initializer => new BulkRequest(CallIsolatedValue)
		{
			Operations = Updates
				.Select(u => new BulkUpdateOperation<Project, Project>(u) { Script = new InlineScript("_source.counter++") })
				.ToList<IBulkOperation>()
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{CallIsolatedValue}/_bulk";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Bulk(f),
			(client, f) => client.BulkAsync(f),
			(client, r) => client.Bulk(r),
			(client, r) => client.BulkAsync(r)
		);

		private IEnumerable<object> ProjectToBulkJson(Project p)
		{
			yield return new Dictionary<string, object> { { "update", new { _id = p.Name, routing = p.Name } } };
			yield return new { script = new { source = "_source.counter++" } };
		}
	}
}
