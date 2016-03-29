using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.QueryDsl.Compound.FunctionScore
{
	public class FunctionScoreQueryUsageTests : QueryDslUsageTestsBase
	{
		public FunctionScoreQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			function_score = new
			{
				_name = "named_query",
				boost = 1.1,
				boost_mode = "multiply",
				functions = new object[] {
					new {
						exp = new {
							numberOfCommits = new {
								origin = 1.0,
								scale = 0.1,
								decay = 0.5
							}
						},
						weight = 2.1
					},
					new {
						gauss = new {
							lastActivity = new {
								origin = "now",
								scale = "1d",
								decay = 0.5
							}
						}
					},
					new {
						linear = new {
							location = new {
								origin = new {
								lat = 70.0,
								lon = -70.0
								},
								scale = "1.0mi"
							},
						multi_value_mode = "avg"
						}
					},
					new {
						field_value_factor = new {
							field = "x",
							factor = 1.1,
							missing = 0.1,
							modifier = "ln"
						}
					},
					new { random_score = new { seed = 1337 } },
					new { random_score = new { seed = "randomstring" } },
					new { weight = 1.0 },
					new {
						script_score = new {
							script = new {
								file = "x"
							}
						}
					}
				},
				max_boost = 20.0,
				min_score = 1.0,
				query = new
				{
					match_all = new { }
				},
				score_mode = "sum"
			}
		};

		protected override QueryContainer QueryInitializer => new FunctionScoreQuery()
		{
			Name = "named_query",
			Boost = 1.1,
			Query = new MatchAllQuery { },
			BoostMode = FunctionBoostMode.Multiply,
			ScoreMode = FunctionScoreMode.Sum,
			MaxBoost = 20.0,
			MinScore = 1.0,
			Functions = new List<IScoreFunction>
			{
				new ExponentialDecayFunction { Origin = 1.0, Decay =	0.5, Field = Field<Project>(p=>p.NumberOfCommits), Scale = 0.1, Weight = 2.1 },
				new GaussDateDecayFunction { Origin = DateMath.Now, Field = Field<Project>(p=>p.LastActivity), Decay = 0.5, Scale = TimeSpan.FromDays(1) },
				new LinearGeoDecayFunction { Origin = new GeoLocation(70, -70), Field = Field<Project>(p=>p.Location), Scale = Distance.Miles(1), MultiValueMode = MultiValueMode.Average },
				new FieldValueFactorFunction	
				{
					Field = "x", Factor = 1.1,	Missing = 0.1, Modifier = FieldValueFactorModifier.Ln
				},
				new RandomScoreFunction { Seed = 1337 },
				new RandomScoreFunction { Seed = "randomstring" },
				new WeightFunction { Weight = 1.0},
				new ScriptScoreFunction { Script = new ScriptQuery { File = "x" } }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.FunctionScore(c => c
				.Name("named_query")
				.Boost(1.1)
				.Query(qq => qq.MatchAll())
				.BoostMode(FunctionBoostMode.Multiply)
				.ScoreMode(FunctionScoreMode.Sum)
				.MaxBoost(20.0)
				.MinScore(1.0)
				.Functions(f => f
					.Exponential(b => b.Field(p => p.NumberOfCommits).Decay(0.5).Origin(1.0).Scale(0.1).Weight(2.1))
					.GaussDate(b => b.Field(p => p.LastActivity).Origin(DateMath.Now).Decay(0.5).Scale("1d"))
					.LinearGeoLocation(b => b.Field(p => p.Location).Origin(new GeoLocation(70, -70)).Scale(Distance.Miles(1)).MultiValueMode(MultiValueMode.Average))
					.FieldValueFactor(b => b.Field("x").Factor(1.1).Missing(0.1).Modifier(FieldValueFactorModifier.Ln))
					.RandomScore(1337)
					.RandomScore("randomstring")
					.Weight(1.0)
					.ScriptScore(ss => ss.Script(s => s.File("x")))
				)
			);

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IFunctionScoreQuery>(a => a.FunctionScore)
		{
			q=> q.Functions = null,
			q=> q.Functions = Enumerable.Empty<IScoreFunction>(),
		};
	}
}
