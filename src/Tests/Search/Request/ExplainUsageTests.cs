using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.Search.Request
{
	public class ExplainUsageTests : SearchUsageTestBase
	{
		/**
		 * Enables explanation for each hit on how its score was computed.
		 *
		 * See the Elasticsearch documentation on {ref_current}/search-explain.html[Explain] for more detail.
		 */
		public ExplainUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object ExpectJson =>
			new { explain = true };

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.Explain();

		protected override SearchRequest<Project> Initializer =>
			new SearchRequest<Project> { Explain = true };

		[I]
		protected async Task ExplanationIsSetOnHits() => await this.AssertOnAllResponses(r =>
		{
			r.Hits.Should().NotBeEmpty();
			r.Hits.Should().NotContain(hit => hit.Explanation == null);
			foreach (var explanation in r.Hits.Select(h => h.Explanation))
			{
				explanation.Description.Should().NotBeNullOrEmpty();
				explanation.Value.Should().BeGreaterThan(0);
			}
		});
	}
}
