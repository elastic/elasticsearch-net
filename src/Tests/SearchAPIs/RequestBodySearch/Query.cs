using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests._Internals.Integration;
using Tests._Internals.MockData;
using FluentAssertions;

namespace Tests.SearchAPIs.RequestBodySearch
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

			public override string ExpectPathAndQuery => "/project/project/_search";

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
