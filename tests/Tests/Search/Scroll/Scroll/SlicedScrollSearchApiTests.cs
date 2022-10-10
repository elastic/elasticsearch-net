// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Scroll.Scroll;

// ReadOnlyCluster because even though its technically a write action, it does not hinder on going reads.
public class SlicedScrollSearchApiTests
	: ApiIntegrationTestBase<ReadOnlyCluster, ScrollResponse<Project>, ScrollRequestDescriptor, ScrollRequest>
{
	private int _slice;

	private ScrollId _scrollId = "default-for-unit-tests";

	public SlicedScrollSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override bool SupportsDeserialization => false;
	protected override string ExpectedUrlPathAndQuery => $"/_search/scroll";
	protected override int ExpectStatusCode => 200;
	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override object ExpectJson => new
	{
		scroll = "1m",
		scroll_id = _scrollId
	};

	protected override ScrollRequest Initializer => new() { Scroll = "1m", ScrollId = _scrollId };

	protected override Action<ScrollRequestDescriptor> Fluent => f => f.Scroll("1m").ScrollId(_scrollId);

	protected override LazyResponses ClientUsage() => Calls(
		(c, f) => c.Scroll<Project>(f),
		(c, f) => c.ScrollAsync<Project>(f),
		(c, r) => c.Scroll<Project>(r),
		(c, r) => c.ScrollAsync<Project>(r)
	);

	protected override void OnBeforeCall(ElasticsearchClient client)
	{
		var maxSlices = 2; // number of shards we use by default for test indices
		var currentSlice = Interlocked.Increment(ref _slice) % maxSlices;
		var scrollTimeout = TimeSpan.FromMinutes(1);
		var response = client.Search<Project>(s => s
			.Scroll(scrollTimeout)
			.Slice(ss => ss.Max(maxSlices).Id(currentSlice))
			.Sort(new[]
			{
				new SortCombinations(SortOptions.Field("_doc", new FieldSort { Order = SortOrder.Asc }))
			})
		);
		if (!response.IsValid)
			throw new Exception("Scroll setup failed");

		_scrollId = response.ScrollId ?? _scrollId;
	}

	protected override void OnAfterCall(ElasticsearchClient client) => client.ClearScroll(cs => cs.ScrollId(_scrollId));
}
