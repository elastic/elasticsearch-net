using System;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.SearchAPIs.RequestBodySearch
{
	public class PostFilter
	{
		public class Usage : SearchUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i) {} 

			protected override object ExpectedJson =>
				new { post_filter = new { match_all = new { } } };

			public override int ExpectStatusCode => 200;

			public override bool ExpectIsValid => true;

			public override void AssertUrl(Uri u) => u.PathAndQuery.Should().EndWith("/_search");

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>()
				{
					PostFilter = new QueryContainer(new MatchAllQuery())
				};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.PostFilter(f => f.MatchAll());

			[I]
			public async void ShouldHaveHits() => await AssertOnAllResponses((r) =>
			{
				r.Hits.Should().NotBeNull();
			});

		}
	}
}
