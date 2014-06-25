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
					.Functions(
						ff => ff.Gauss(x => x.StartedOn, d => d.Scale("42w")),
						ff => ff.Linear(x => x.FloatValue, d => d.Scale("0.3")),
						ff => ff.Exp(x => x.DoubleValue, d => d.Scale("0.5")),
						ff => ff.BoostFactor(2).Filter(bff=>Filter1)
					)
					.Query(qq=>Query1)
					.RandomScore(1337)
					.ScoreMode(FunctionScoreMode.first)
					.ScriptScore(s=>s
						.Script("My complex script")
						.Params(p=>p.Add("param", "paramvalue"))
						.Lang("mvel")
					)
					)
				);

			q.BoostMode.Should().Be(FunctionBoostMode.Average);
			q.RandomScore.Should().NotBeNull();
			q.RandomScore.Seed.Should().Be(1337);
			q.ScoreMode.Should().Be(FunctionScoreMode.first);
			q.ScriptScore.Should().NotBeNull();
			q.ScriptScore.Lang.Should().Be("mvel");
			q.ScriptScore.Script.Should().Be("My complex script");
			var param = q.ScriptScore.Params.FirstOrDefault();
			param.Should().NotBeNull();
			param.Key.Should().Be("param");
			param.Value.Should().Be("paramvalue");
			q.Functions.Should().NotBeEmpty().And.HaveCount(4);

			//TODO rip out state from all these function descriptors
			var functions = q.Functions.ToList();
			functions.Should().NotBeEmpty().And.HaveCount(4);


		}
	}
}