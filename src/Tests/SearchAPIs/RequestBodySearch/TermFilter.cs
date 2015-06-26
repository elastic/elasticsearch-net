using System;
using Nest;
using Tests._Internals;

namespace Tests.SearchAPIs.RequestBodySearch
{
	public class TermQueryTests
	{
		/**
		 * Pagination of results can be done by using the from and size parameters. 
		 * The from parameter defines the offset from the first result you want to fetch. 
		 * The size parameter allows you to configure the maximum amount of hits to be returned.
		 */
		public class Usage : GeneralUsageTests<ITermQuery, TermQueryDescriptor<object>, TermQuery>
		{
			protected override object ExpectedJson => 
				new  { field = "value" };

			protected override TermQuery Initializer =>
				new TermQuery
				{
					Field = "field",
					Value = "value"
				};

			protected override Func<TermQueryDescriptor<object>, ITermQuery> Fluent =>
				term => term.OnField("field").Value("value");
		}
		
		public class UsageInsideQueryDescriptor : GeneralUsageTests<IQueryContainer, QueryDescriptor<object>, QueryContainer>
		{
			protected override object ExpectedJson =>
				new { term = new { field = "value" } };

			protected override QueryContainer Initializer =>
				new TermQuery
				{
					Field = "field",
					Value = "value"
				};

			protected override Func<QueryDescriptor<object>, IQueryContainer> Fluent =>
				filter => filter.Term("field" , "value");
		}

	}
}
