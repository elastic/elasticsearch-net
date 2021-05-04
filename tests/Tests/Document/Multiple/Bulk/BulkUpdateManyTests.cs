// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.Bulk
{
	public class BulkUpdateManyTests : ApiTestBase<ReadOnlyCluster, BulkResponse, IBulkRequest, BulkDescriptor, BulkRequest>
	{
		private readonly List<Project> _updates = Project.Projects.Take(10).ToList();

		public BulkUpdateManyTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => _updates.SelectMany(ProjectToBulkJson);

		protected override Func<BulkDescriptor, IBulkRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.UpdateMany(_updates, (b, u) => b.Script(s => s.Source("_source.counter++")));

		protected override HttpMethod HttpMethod => HttpMethod.POST;


		protected override BulkRequest Initializer => new BulkRequest(CallIsolatedValue)
		{
			Operations = _updates
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
