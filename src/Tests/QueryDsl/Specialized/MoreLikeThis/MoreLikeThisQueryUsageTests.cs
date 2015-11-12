using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.Specialized.MoreLikeThis
{
	public class MoreLikeThisUsageTests : QueryDslUsageTestsBase
	{
		public MoreLikeThisUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			span_within = new
			{
				_name = "named_query",
				boost = 1.1,
				little = new
				{
					span_term = new { field1 = new { value = "hoya" } }
				},
				big = new
				{
					span_term = new { field1 = new { value = "hoya2" } }
				}
			}
		};

		protected override QueryContainer QueryInitializer => new MoreLikeThisQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Fields = Fields<Project>(p=>p.Name)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MoreLikeThis(sn => sn
				.Name("named_query")
				.Boost(1.1)
			);
	}
}
