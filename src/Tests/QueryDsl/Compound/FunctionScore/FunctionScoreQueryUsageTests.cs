using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using static Nest.Static;

namespace Tests.QueryDsl.Compound.FunctionScore
{
	public class FunctionScoreQueryUsageTests : QueryDslUsageTestsBase
	{
		public FunctionScoreQueryUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object QueryJson => new
		{
			indices = new
			{
				_name = "named_query",
				boost = 1.1,
				indices = new[] { "project" },
				no_match_query = new
				{
					match_all = new
					{
						_name = "no_match"
					}
				},
				query = new
				{
					match_all = new { }
				}
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
			Functions = new List<IFunctionScoreFunction>
			{
				new BoostFactorFunction { BoostFactor = 1.1, Filter = new MatchAllQuery { }, Weight = 2.1 },
				new ExponentialDecayFunction { Origin = 1.0, Decay =  0.5, Field = Field<Project>(p=>p.NumberOfCommits), Scale = 0.1, Weight = 2.1 },
				new GaussDateDecayFunction { Origin = DateMath.Now, Field = Field<Project>(p=>p.LastActivity), Decay = 0.5, Scale = TimeSpan.FromDays(1) },
				new LinearGeoDecayFunction { Origin = new GeoLocation(70, -70), Field = Field<Project>(p=>p.Location), Scale = GeoDistance.Miles(1), MultiValueMode = MultiValueMode.Average }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.FunctionScore(c => c
				.Name("named_query")
				.Boost(1.1)
				.Query(qq=>qq.MatchAll())
				.BoostMode(FunctionBoostMode.Multiply)
				.ScoreMode(FunctionScoreMode.Sum)
				.MaxBoost(20.0)
				.MinScore(1.0)
				.Functions(f=>f
					.BoostFactor(b=>b.BoostFactor(1.1).Filter(bq=>bq.MatchAll()).Weight(2.1))
					.Exponential(b=>b.Field(p=>p.NumberOfCommits).Decay(0.5).Origin(1.0).Scale(0.1).Weight(2.1))
					.GaussDate(b=>b.Field(p=>p.LastActivity).Origin(DateMath.Now).Decay(0.5).Scale("1d"))
					.LinearGeoLocation(b=>b.Field(p=>p.Location).Origin(new GeoLocation(70,70)).Scale(GeoDistance.Miles(1)).MultiValueMode(MultiValueMode.Average))
				)
			);
	}
}
