using System;
using FluentAssertions;

namespace Nest.Tests.Literate.SearchAPIs.RequestBodySearch
{
	public class PostFilter
	{
		public class Usage : SearchUsageBase
		{
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
		}
	}
}
