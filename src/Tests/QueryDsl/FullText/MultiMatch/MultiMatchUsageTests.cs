using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.FullText.MultiMatch
{
	public class MultiMatchUsageTests : QueryDslUsageTestsBase
	{
		public MultiMatchUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			match = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					query = "hello world",
					analyzer = "standard",
					fuzzy_rewrite = "constant_score_boolean",
					fuzziness = "AUTO",
					fuzzy_transpositions = true,
					cutoff_frequency = 0.001,
					prefix_length = 2,
					max_expansions = 2,
					slop = 2,
					lenient = true,
					minimum_should_match = 2,
			        @operator = "or"
				}
			}
		};

		protected override QueryContainer QueryInitializer => new MultiMatchQuery
		{
			Fields = Field<Project>(p=>p.Description).And("myOtherField").ToArray(),
			Analyzer = "standard",
			Boost = 1.1,
			Name = "named_query",
			CutoffFrequency = 0.001,
			Query = "hello world",
			Fuzziness = Fuzziness.Auto,
			FuzzyRewrite = RewriteMultiTerm.ConstantScoreBoolean,
			MaxExpansions = 2,
			Slop = 2,
			Lenient = true,
			Operator = Operator.Or,
			PrefixLength = 2
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.MultiMatch(c => c
				.OnFields(p => p.Description)
				.Query("hello world")
				.Analyzer("standard")
				.Boost(1.1)
				.Slop(2)
				.Fuzziness(Fuzziness.Auto)
				.PrefixLength(2)
				.MaxExpansions(2)
				.Operator(Operator.Or)
				.MinimumShouldMatch(2)
				.FuzzyRewrite(RewriteMultiTerm.ConstantScoreBoolean)
				.TieBreaker(1.1)
				.CutoffFrequency(0.001)
				.Lenient()
				.ZeroTermsQuery(ZeroTermsQuery.All)
				.Name("named_query")
			);
	}
}
