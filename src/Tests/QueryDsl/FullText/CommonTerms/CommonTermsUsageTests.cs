using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.FullText.CommonTerms
{
	public class CommonTermsUsageTests : QueryDslUsageTestsBase
	{
		public CommonTermsUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{

		};

		protected override QueryContainer QueryInitializer => new CommonTermsQuery()
		{
			Field = Field<Project>(p=>p.Description),
			Analyzer = "standard",
			Boost = 1.1,
			CutoffFrequency = 0.001,
			DisableCoord = true,
			HighFrequencyOperator = Operator.And,
			LowFrequencyOperator = Operator.Or,
			MinimumShouldMatch = 1,
			Name = "named_query",
			Query = "nelly the elephant not as a"
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> d) => d
			.CommonTerms(c => c
				.OnField(p => p.Description)
				.Analyzer("standard")
				.Boost(1.1)
				.CutoffFrequency(0.001)
				.DisableCoord()
				.HighFrequencyOperator(Operator.And)
				.LowFrequencyOperator(Operator.Or)
				.MinimumShouldMatch(1)
				.Name("named_query")
				.Query("nelly the elephant not as a")
			);

		[I] public void HandlingResponses() { }

	}
}
