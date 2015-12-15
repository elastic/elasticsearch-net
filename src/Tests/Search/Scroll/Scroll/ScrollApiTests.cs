using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.Scroll.Scroll
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ScrollApiTests : ApiIntegrationTestBase<ISearchResponse<Project>, IScrollRequest, ScrollDescriptor<Project>, ScrollRequest>
	{
		public ScrollApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.Scroll("1m", "scroll-id", f),
			fluentAsync: (c, f) => c.ScrollAsync("1m", "scroll-id", f),
			request: (c, r) => c.Scroll<Project>(r),
			requestAsync: (c, r) => c.ScrollAsync<Project>(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_search/scroll?scroll=1m";

		protected override ScrollDescriptor<Project> NewDescriptor() => new ScrollDescriptor<Project>();

		protected override Func<ScrollDescriptor<Project>, IScrollRequest> Fluent => null;

		protected override ScrollRequest Initializer => new ScrollRequest("scroll-id", "1m");
	}
}
