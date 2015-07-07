using System;
using Nest;
using Tests.Framework;

namespace Tests.Search.Request
{
	public class TermQueryTests
	{
		/**
		 * Pagination of results can be done by using the from and size parameters. 
		 * The from parameter defines the offset from the first result you want to fetch. 
		 * The size parameter allows you to configure the maximum amount of hits to be returned.
		 */
		public class Usage : GeneralUsageBase<ITermQuery, TermQueryDescriptor<object>, TermQuery>
		{
			protected override object ExpectJson =>
				new { myfield = new { value = "myvalue" } };

			protected override TermQuery Initializer =>
				new TermQuery
				{
					Field = "myfield",
					Value = "myvalue"
				};

			protected override Func<TermQueryDescriptor<object>, ITermQuery> Fluent =>
				term => term.OnField("myfield").Value("myvalue");
		}

		public class UsageInsideQueryContainer : GeneralUsageBase<IQueryContainer, QueryContainerDescriptor<object>, QueryContainer>
		{
			protected override object ExpectJson =>
				new { term = new { myfield = new { value = "myvalue" } } };

			protected override QueryContainer Initializer =>
				new TermQuery
				{
					Field = "myfield",
					Value = "myvalue"
				};

			protected override Func<QueryContainerDescriptor<object>, IQueryContainer> Fluent =>
				filter => filter.Term("myfield", "myvalue");
		}

	}
}
