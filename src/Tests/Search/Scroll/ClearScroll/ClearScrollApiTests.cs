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

namespace Tests.Search.Scroll.ClearScroll
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClearScrollApiTests : ApiIntegrationTestBase<IEmptyResponse, IClearScrollRequest, ClearScrollDescriptor, ClearScrollRequest>
	{
		public ClearScrollApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.ClearScroll("scroll-id", f),
			fluentAsync: (c, f) => c.ClearScrollAsync("scroll-id", f),
			request: (c, r) => c.ClearScroll(r),
			requestAsync: (c, r) => c.ClearScrollAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_search/scroll";

		protected override ClearScrollDescriptor NewDescriptor() => new ClearScrollDescriptor();

		protected override Func<ClearScrollDescriptor, IClearScrollRequest> Fluent => null;

		protected override ClearScrollRequest Initializer => new ClearScrollRequest("scroll-id");
	}
}
