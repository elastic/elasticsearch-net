using System;
using System.Threading;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.Scroll.Scroll
{
	public class SlicedScrollSearchApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, IScrollRequest, ScrollDescriptor<Project>, ScrollRequest>
	{
		public SlicedScrollSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

		protected int _slice = 0;
		protected override void OnBeforeCall(IElasticClient client)
		{
			var maxSlices = 2; // number of shards we use by default for test indices
			var currentSlice = Interlocked.Increment(ref this._slice) % maxSlices;
			var scrollTimeout = TimeSpan.FromMinutes(1);
			var response = client.Search<Project>(s => s
				.Scroll(scrollTimeout)
				.Slice(ss=>ss.Max(maxSlices).Id(currentSlice))
				.Sort(ss=>ss.Field("_doc", SortOrder.Ascending))
			);
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
