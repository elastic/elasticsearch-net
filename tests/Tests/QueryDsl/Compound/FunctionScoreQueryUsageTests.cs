// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.Compound;

public class FunctionScoreQueryUsageTests : QueryDslUsageTestsBase
{
	public FunctionScoreQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage)
	{
	}

	protected override bool VerifyJson => true;

	private static FunctionScore GetFieldValueFactorScoreFunction()
	{
		var function = FunctionScore.FieldValueFactor(new FieldValueFactorScoreFunction
		{
			Field = Infer.Field<Project>(p => p.NumberOfCommits),
			Factor = 1.1,
			Missing = 0.1,
			Modifier = FieldValueFactorModifier.Square
		});

		function.Weight = 3;
		function.Filter = new TermQuery(Infer.Field<Project>(p => p.Branches))
		{
			Value = "dev"
		};

		return function;
	}

	private static FunctionScore GetScriptScoreFunction()
	{
		var function = FunctionScore.ScriptScore(new ScriptScoreFunction
		{
			Script = new Script(new InlineScript("Math.log(2 + doc['numberOfCommits'].value)"))
		});

		function.Weight = 1.0;

		return function;
	}

	protected override Query QueryInitializer => new FunctionScoreQuery
	{
		QueryName = "named_query",
		Boost = 1.1f,
		Query = new MatchAllQuery(),
		BoostMode = FunctionBoostMode.Multiply,
		ScoreMode = FunctionScoreMode.Sum,
		MaxBoost = 20.0,
		MinScore = 1.0,
		Functions = new FunctionScore[]
		{
			GetFieldValueFactorScoreFunction(),
			new RandomScoreFunction { Seed = 1337, Field = "_seq_no" }, // For ease, when weight is not required, we can allow the implicit conversion to apply.
			new RandomScoreFunction { Seed = "random_string", Field = "_seq_no" }, // For ease, when weight is not required, we can allow the implicit conversion to apply.
			GetScriptScoreFunction(),
			FunctionScore.WeightScore(1.0)
		}
	};

	protected override QueryDescriptor<Project> QueryFluent(QueryDescriptor<Project> queryDescriptor) =>
		queryDescriptor
			.FunctionScore(c => c
				.QueryName("named_query")
				.Boost(1.1f)
				.Query(qq => qq.MatchAll())
				.BoostMode(FunctionBoostMode.Multiply)
				.ScoreMode(FunctionScoreMode.Sum)
				.MaxBoost(20.0)
				.MinScore(1.0)
				.Functions(
					f => f.FieldValueFactor(fv => fv
						.Field(f => f.NumberOfCommits)
						.Factor(1.1)
						.Missing(0.1)
						.Modifier(FieldValueFactorModifier.Square)).Weight(3).Filter(f => f.Term(t => t.Field(fld => fld.Branches).Value("dev"))),
					f => f.RandomScore(r => r.Seed(1337).Field("_seq_no")),
					f => f.RandomScore(r => r.Seed("random_string").Field("_seq_no")),
					f => f.ScriptScore(s => s.Script(new Script(new InlineScript("Math.log(2 + doc['numberOfCommits'].value)")))).Weight(1.0),
					f => f.WeightScore(1.0)
				));
}
