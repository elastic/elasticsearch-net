using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.FullText.Match
{
	public class MatchPhrasePrefixUsageTests : QueryDslUsageTestsBase
	{
		public MatchPhrasePrefixUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{

		};

		protected override QueryContainer QueryInitializer => new MatchPhrasePrefixQuery
		{
			Field = Field<Project>(p=>p.Description),
			Analyzer = "standard",
			Boost = 1.1,
			CutoffFrequency = 0.001,
			Fuzziness = Fuzziness.EditDistance(2),
			FuzzyTranspositions = true,
			//TODO more
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> d) => d
			.MatchPhrasePrefix(c => c
				.OnField(p => p.Description)
				.Analyzer("standard")
				.Boost(1.1)
				.CutoffFrequency(0.001)
			);

		[I] public void HandlingResponses() { }

	}
}
