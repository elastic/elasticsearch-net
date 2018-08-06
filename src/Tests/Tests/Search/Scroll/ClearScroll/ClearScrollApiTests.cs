using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Search.Scroll.ClearScroll
{
	// ReadOnlyCluster because eventhough its technically a write action it does not hinder
	// on going reads
	public class ClearScrollApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IClearScrollResponse, IClearScrollRequest, ClearScrollDescriptor, ClearScrollRequest>
	{
		public ClearScrollApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private string _scrollId = "default-for-unit-tests";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.ClearScroll(f),
			fluentAsync: (c, f) => c.ClearScrollAsync(f),
			request: (c, r) => c.ClearScroll(r),
			requestAsync: (c, r) => c.ClearScrollAsync(r)
		);

		protected override object ExpectJson => new
		{
			scroll_id = new []
			{
				_scrollId
			}
		};

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_search/scroll";
		protected override bool SupportsDeserialization => false;

		protected override ClearScrollDescriptor NewDescriptor() => new ClearScrollDescriptor();

		protected override Func<ClearScrollDescriptor, IClearScrollRequest> Fluent => cs => cs.ScrollId(_scrollId);

		protected override ClearScrollRequest Initializer => new ClearScrollRequest(_scrollId);

		protected override void OnBeforeCall(IElasticClient client)
		{
			var scroll = this.Client.Search<Project>(s => s.MatchAll().Scroll(TimeSpan.FromMinutes((1))));
			if (!scroll.IsValid)
				throw new Exception("Setup: Initial scroll failed.");
			_scrollId = scroll.ScrollId ?? _scrollId;
		}
	}
}
