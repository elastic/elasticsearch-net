using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Tests.SearchAPIs;

namespace Tests.Search.Request
{
	public class Query
	{
		/**
		 * The query element within the search request body allows to define a query using the Query DSL.
		 */

		public class Usage : SearchUsageBase
		{
			public Usage(ReadOnlyIntegration i) : base(i) { }

			public override bool ExpectIsValid => true;

			public override int ExpectStatusCode => 200;

			protected override object ExpectJson => 
				new { query = new { term = new { name = new { value = "elasticsearch" } } } };

			protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
				.Query(q => q
					.Term(p => p.Name, "elasticsearch")
				);


			protected override SearchRequest<Project> Initializer =>
				new SearchRequest<Project>
				{
					Query = new TermQuery
							{
								Field = "name",
								Value = "elasticsearch"
							}
				};

		}
	}
}
