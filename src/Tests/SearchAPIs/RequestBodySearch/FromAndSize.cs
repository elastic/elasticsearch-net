using System;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;

namespace Tests.SearchAPIs.RequestBodySearch
{
	public class FromAndSize
	{
		/**
		 * Pagination 2 of results can be done by using the from and size parameters. 
		 * The from parameter defines the offset from the first result you want to fetch. 
		 * The size parameter allows you to configure the maximum amount of hits to be returned.
		 */

		public class Usage : SearchUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i) {} 

			protected override object ExpectedJson =>
				new { from = 10, size = 12 };

			public override int ExpectStatusCode => 200;

			public override bool ExpectIsValid => true;

			public override void AssertUrl(Uri u) => u.AbsolutePath.Should().Be("/project/project/_search");

			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>()
				{
					From = 10,
					Size = 12
				};

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
					.From(10)
					.Size(12);
		}
	}
}
