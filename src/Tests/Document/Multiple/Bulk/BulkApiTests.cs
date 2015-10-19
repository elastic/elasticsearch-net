using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Multiple.Bulk
{
	[Collection(IntegrationContext.Indexing)]
	public class BulkApiTests : ApiTestBase<IBulkResponse, IBulkRequest, BulkDescriptor, BulkRequest>
	{
		public BulkApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Bulk(f),
			fluentAsync: (client, f) => client.BulkAsync(f),
			request: (client, r) => client.Bulk(r),
			requestAsync: (client, r) => client.BulkAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}/_bulk";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new object[]
		{
			new Dictionary<string, object>{ { "index", new { _index = "project", _type = "project", _id = "nesttests" } } },
			Project.InstanceAnonymous,
			new Dictionary<string, object>{ { "update", new { _index = "project", _type="project", _id = "nesttests1" } } },
			new { script = "1+1" },
			new Dictionary<string, object>{ { "create", new { _index = "project", _type="project", _id = "nesttests" } } },
			Project.InstanceAnonymous,
		};

		protected override Func<BulkDescriptor, IBulkRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.Index<Project>(b => b.Document(Project.Instance))
			.Update<Project>(b => b.Script("1+1").Id("nesttests1"))
			.Create<Project>(b => b.Document(Project.Instance));
			

		protected override BulkRequest Initializer => new BulkRequest(CallIsolatedValue)
		{
			Operations = new List<IBulkOperation>
			{
				new BulkIndexOperation<Project>(Project.Instance),
				new BulkUpdateOperation<Project, Project>("nesttests1") { Script = "1+1" },
				new BulkCreateOperation<Project>(Project.Instance),
			}
		};
	}
}
