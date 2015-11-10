using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.Joining.Nested
{
	public class NestedUsageTests : QueryDslUsageTestsBase
	{
		public NestedUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			nested = new
			{
				_name = "named_query",
				boost = 1.1,
				query = new
				{
					match_all = new { }
				},
				path = "curatedTags",
				inner_hits = new
				{
					explain = true
				}
			}
		};

		protected override QueryContainer QueryInitializer => new NestedQuery
		{
			Name = "named_query",
			Boost = 1.1,
			InnerHits = new InnerHits { Explain = true },
			Query = new MatchAllQuery(),
			Path = Field<Project>(p=>p.CuratedTags)
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Nested(c => c
				.Name("named_query")
				.Boost(1.1)
				.InnerHits(i=>i.Explain())
				.Query(qq=>qq.MatchAll())
				.Path(p=>p.CuratedTags)
			);
	}
}
