using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using Tests.Framework.MockData;

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

		protected override Func<FieldStatsDescriptor, IFieldStatsRequest> Fluent => null;

		protected override FieldStatsRequest Initializer => new FieldStatsRequest(typeof(Project));
	}
}
