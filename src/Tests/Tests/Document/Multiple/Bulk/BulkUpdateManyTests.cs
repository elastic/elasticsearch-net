using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Multiple.Bulk
{
	public class BulkUpdateManyTests : ApiTestBase<ReadOnlyCluster, IBulkResponse, IBulkRequest, BulkDescriptor, BulkRequest>
	{
		private List<Project> Updates = Project.Projects.Take(10).ToList();

		public BulkUpdateManyTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Bulk(f),
			fluentAsync: (client, f) => client.BulkAsync(f),
			request: (client, r) => client.Bulk(r),
			requestAsync: (client, r) => client.BulkAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_bulk";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => Updates.SelectMany<Project, object>(ProjectToBulkJson);

		private IEnumerable<object> ProjectToBulkJson(Project p)
		{
			yield return new Dictionary<string, object> { { "update", new { _type = "doc", _id = p.Name, routing = p.Name } } };
			yield return new { script = new { source = "_source.counter++" } };
		}

		protected override Func<BulkDescriptor, IBulkRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.UpdateMany(Updates, (b, u) => b.Script(s => s.Source("_source.counter++")));


		protected override BulkRequest Initializer => new BulkRequest(CallIsolatedValue)
		{
			Operations = Updates
				.Select(u=> new BulkUpdateOperation<Project, Project>(u) { Script = new InlineScript("_source.counter++") })
				.ToList<IBulkOperation>()
		};
	}
}
