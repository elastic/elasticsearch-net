using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class FunctionScoreQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void FunctionScore_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.FunctionScore,
				f=>f.FunctionScore(fq=>fq
					.BoostMode(FunctionBoostMode.Average)
					.MaxBoost(0.95f)
					.MinScore(1.1f)
					.Functions(
						ff => ff.Gauss(x => x.StartedOn, d => d.Scale("42w")).Weight(1),
						ff => ff.Linear(x => x.FloatValue, d => d.Scale("0.3")).Filter(lff=>Filter2).Weight(2),
						ff => ff.Exp(x => x.DoubleValue, d => d.Scale("0.5")).Weight(3),
						ff => ff.BoostFactor(2).Filter(bff=>Filter1),
						ff => ff.Weight(5.0).Filter(wf =>Filter1),
						ff => ff.RandomScore(1337),
						ff => ff.ScriptScore(s => s
							.Script("My complex script")
							.Params(p => p.Add("param", "paramvalue"))
							.Lang("mvel")
						)
					)
					.Query(qq=>Query1)
					.ScoreMode(FunctionScoreMode.First)
					)
				);

			q.BoostMode.Should().Be(FunctionBoostMode.Average);
			q.MaxBoost.Should().Be(0.95f);
			q.MinScore.Should().Be(1.1f);
			q.ScoreMode.Should().Be(FunctionScoreMode.First);

			//TODO rip out state from all these function descriptors
			var functions = q.Functions.ToList();
			functions.Should().NotBeEmpty().And.HaveCount(7);


		}
	}
}