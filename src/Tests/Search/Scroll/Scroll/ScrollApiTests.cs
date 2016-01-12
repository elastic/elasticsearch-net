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

		private string _scrollId = "default-for-unit-tests"; 

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.Scroll("1m", _scrollId, f),
			fluentAsync: (c, f) => c.ScrollAsync("1m", _scrollId, f),
			request: (c, r) => c.Scroll<Project>(r),
			requestAsync: (c, r) => c.ScrollAsync<Project>(r)
		);

		protected override object ExpectJson => new
		{
			scroll = "1m",
			scroll_id = _scrollId
		};
		
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_search/scroll";
		protected override bool SupportsDeserialization => false;

		protected override Func<ScrollDescriptor<Project>, IScrollRequest> Fluent => s => s.Scroll("1m").ScrollId(_scrollId);

		protected override ScrollRequest Initializer => new ScrollRequest(_scrollId, "1m");

		protected override void OnBeforeCall(IElasticClient client)
		{
			var response = client.Search<Project>(s => s.MatchAll().Scroll(TimeSpan.FromMinutes((1))));
			if (!response.IsValid)
				throw new Exception("Scroll setup failed");
			_scrollId = response.ScrollId ?? _scrollId;
		}

		protected override void OnAfterCall(IElasticClient client)
		{
			client.ClearScroll(cs => cs.ScrollId(_scrollId));
		}
	}
}
