using System;
using FluentAssertions;
using Nest.Tests.Literate._Internals.Integration;
using Xunit;

namespace Nest.Tests.Literate.SearchAPIs.RequestBodySearch
{
	public class PostFilter
	{
		public class Usage : SearchUsageBase
		{
			public Usage(ReadonlyIntegration i) : base(i) {} 

			protected override object ExpectedJson =>
				new { post_filter = new { match_all = new { } } };

			public override int ExpectStatusCode => 200;

			public override bool ExpectIsValid => true;

			public override void AssertUrl(Uri u) => u.PathAndQuery.Should().EndWith("/_search");

			protected override SearchRequest Initializer =>
				new SearchRequest()
				{
					PostFilter = new FilterContainer(new MatchAllFilter())
				};

			protected override Func<SearchDescriptor<object>, ISearchRequest> Fluent => s => s
				.PostFilter(f => f.MatchAll());

			[IntegrationFact]
			public async void ShouldHaveHits() => await AssertOnAllResponses((r) =>
			{
				r.Hits.Should().NotBeNull();
			});

		}
	}
}
