using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Search.FieldStats
{
	[Collection(IntegrationContext.ReadOnly)]
	public class FieldStatsApiTests
		: ApiIntegrationTestBase<IFieldStatsResponse, IFieldStatsRequest, FieldStatsDescriptor, FieldStatsRequest>
	{
		public FieldStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.FieldStats(typeof(Project)),
			fluentAsync: (c, f) => c.FieldStatsAsync(typeof(Project)),
			request: (c, r) => c.FieldStats(r),
			requestAsync: (c, r) => c.FieldStatsAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/_field_stats";

		protected override Func<FieldStatsDescriptor, IFieldStatsRequest> Fluent => d => d.Fields<Project>(p=>p.Name);

		protected override FieldStatsRequest Initializer => new FieldStatsRequest(typeof(Project)) { Fields = Field<Project>(p => p.Name) };
	}
}
