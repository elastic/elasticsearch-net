using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Nest.Tests.Literate;
using Ploeh.AutoFixture;
using Xunit;
using FluentAssertions;
using Nest.Resolvers;

namespace SearchApis.RequestBody
{
	public class TermFilterTests
	{
		/**
		 * Pagination of results can be done by using the from and size parameters. 
		 * The from parameter defines the offset from the first result you want to fetch. 
		 * The size parameter allows you to configure the maximum amount of hits to be returned.
		 */
		public class Usage : GeneralUsageTests<ITermFilter, TermFilterDescriptor, TermFilter>
		{
			protected override object ExpectedJson => 
				new  { field = "value" };

			protected override TermFilter Initializer =>
				new TermFilter
				{
					Field = "field",
					Value = "value"
				};

			protected override Func<TermFilterDescriptor, ITermFilter> Fluent =>
				term => term.Field("field").Value("value");
		}
		
		public class UsageInsideFilterDescriptor : GeneralUsageTests<IFilterContainer, FilterDescriptor<object>, FilterContainer>
		{
			protected override object ExpectedJson =>
				new { term = new { field = "value" } };

			protected override FilterContainer Initializer =>
				new TermFilter
				{
					Field = "field",
					Value = "value"
				};

			protected override Func<FilterDescriptor<object>, IFilterContainer> Fluent =>
				filter => filter.Term("field" , "value");
		}

	}
}
