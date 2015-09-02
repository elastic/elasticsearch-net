using System;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Search.Request
{
	public class TermQueryTests
	{
		/**
		 * Pagination of results can be done by using the from and size parameters. 
		 * The from parameter defines the offset from the first result you want to fetch. 
		 * The size parameter allows you to configure the maximum amount of hits to be returned.
		 */
		public class Usage : GeneralUsageBase<ITermQuery, TermQueryDescriptor<Project>, TermQuery>
		{
			protected override object ExpectJson =>
				new { name = new { value = "myvalue" } };

			protected override TermQuery Initializer =>
				new TermQuery
				{
					Field = Field<Project>(p=>p.Name),
					Value = "myvalue"
				};

			protected override Func<TermQueryDescriptor<Project>, ITermQuery> Fluent =>
				term => term.OnField("name").Value("myvalue");
		}

		public class UsageInsideQueryContainer : GeneralUsageBase<IQueryContainer, QueryContainerDescriptor<Project>, QueryContainer>
		{
			protected override object ExpectJson =>
				new { term = new { myfield = new { value = "myvalue" } } };

			//implemenations of IQuery such TermQuery implictly convert to `QueryContainer`
			protected override QueryContainer Initializer =>
				new TermQuery
				{
					Field = "myfield",
					Value = "myvalue"
				};

			protected override Func<QueryContainerDescriptor<Project>, IQueryContainer> Fluent =>
				filter => filter.Term("myfield", "myvalue");
		}

	}
}
