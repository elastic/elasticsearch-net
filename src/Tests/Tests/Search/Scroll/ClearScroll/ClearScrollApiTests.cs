using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Search.Scroll.ClearScroll
{
	// ReadOnlyCluster because eventhough its technically a write action it does not hinder
	// on going reads
	public class ClearScrollApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IClearScrollResponse, IClearScrollRequest, ClearScrollDescriptor, ClearScrollRequest>
	{
		private string _scrollId = "default-for-unit-tests";

		public ClearScrollApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			scroll_id = new[]
			{
				_scrollId
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<ClearScrollDescriptor, IClearScrollRequest> Fluent => cs => cs.ScrollId(_scrollId);
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override ClearScrollRequest Initializer => new ClearScrollRequest(_scrollId);
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_search/scroll";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.ClearScroll(f),
			(c, f) => c.ClearScrollAsync(f),
			(c, r) => c.ClearScroll(r),
			(c, r) => c.ClearScrollAsync(r)
		);

		protected override ClearScrollDescriptor NewDescriptor() => new ClearScrollDescriptor();

		protected override void OnBeforeCall(IElasticClient client)
		{
			var scroll = Client.Search<Project>(s => s.MatchAll().Scroll(TimeSpan.FromMinutes(1)));
			if (!scroll.IsValid)
				throw new Exception("Setup: Initial scroll failed.");

			_scrollId = scroll.ScrollId ?? _scrollId;
		}
	}
}
