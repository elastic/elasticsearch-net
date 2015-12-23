using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.Scroll.ClearScroll
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClearScrollApiTests : ApiIntegrationTestBase<IEmptyResponse, IClearScrollRequest, ClearScrollDescriptor, ClearScrollRequest>
	{
		public ClearScrollApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private string _scrollId = "default-for-unit-tests";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.ClearScroll(_scrollId, f),
			fluentAsync: (c, f) => c.ClearScrollAsync(_scrollId, f),
			request: (c, r) => c.ClearScroll(r),
			requestAsync: (c, r) => c.ClearScrollAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override string UrlPath => $"/_search/scroll";

		protected override ClearScrollDescriptor NewDescriptor() => new ClearScrollDescriptor();

		protected override Func<ClearScrollDescriptor, IClearScrollRequest> Fluent => null;

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
