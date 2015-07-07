using System;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Tests.SearchAPIs;

namespace Tests.Search.Request
{
	public class Explain
	{
		/**
		 * Enables explanation for each hit on how its score was computed.
		 */

		public class Usage : SearchUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i) { }

			public override bool ExpectIsValid => true;

			public override int ExpectStatusCode => 200;

			protected override object ExpectJson => 
				new { explain = true };

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Explain();

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project> { Explain = true };

			[I] protected async void ExplanationIsSetOnHits() => await this.AssertOnAllResponses(r =>
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
}
