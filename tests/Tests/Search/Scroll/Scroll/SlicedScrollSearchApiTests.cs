// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Scroll.Scroll
{
	public class SlicedScrollSearchApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ISearchResponse<Project>, IScrollRequest, ScrollDescriptor<Project>, ScrollRequest>
	{
		private int _slice;

		private string _scrollId = "default-for-unit-tests";

		public SlicedScrollSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_search/scroll";
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override object ExpectJson => new
		{
			scroll = "1m",
			scroll_id = _scrollId
		};

		protected override ScrollRequest Initializer => new ScrollRequest(_scrollId, "1m");
		protected override ScrollDescriptor<Project> NewDescriptor() => new ScrollDescriptor<Project>("1m", _scrollId);

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Scroll("1m", _scrollId, f),
			(c, f) => c.ScrollAsync("1m", _scrollId, f),
			(c, r) => c.Scroll<Project>(r),
			(c, r) => c.ScrollAsync<Project>(r)
		);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var maxSlices = 2; // number of shards we use by default for test indices
			var currentSlice = Interlocked.Increment(ref _slice) % maxSlices;
			var scrollTimeout = TimeSpan.FromMinutes(1);
			var response = client.Search<Project>(s => s
				.Scroll(scrollTimeout)
				.Slice(ss => ss.Max(maxSlices).Id(currentSlice))
				.Sort(ss => ss.Field("_doc", SortOrder.Ascending))
			);
			if (!response.IsValid)
				throw new Exception("Scroll setup failed");

			_scrollId = response.ScrollId ?? _scrollId;
		}

		protected override void OnAfterCall(IElasticClient client) => client.ClearScroll(cs => cs.ScrollId(_scrollId));
	}
}
