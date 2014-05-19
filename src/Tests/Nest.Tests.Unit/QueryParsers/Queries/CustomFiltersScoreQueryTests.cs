using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class CustomFiltersScoreQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void CustomFiltersScore_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.CustomFiltersScore,
				f=>f.CustomFiltersScore(cfs=>cfs
					.Language("as")
					.MaxBoost("maxboost")
					.Query(qq=>Query1)
					.ScoreMode(ScoreMode.avg)
					.Filters(ff=>ff
						.Filter(sf=>Filter1)
						.Boost(2.3)
						.Script("My complex script")
						.Params(p=>p.Add("param", "paramvalue"))
						.Lang("mvel")
					)	
					)
				);
			q.Lang.Should().Be("as");
			q.MaxBoost.Should().Be("maxboost");
			q.ScoreMode.Should().Be(ScoreMode.avg);
			
			AssertIsTermQuery(q.Query, Query1);

			q.Filters.Should().NotBeEmpty().And.HaveCount(1);
			var fsf = q.Filters.First();
			fsf.Boost.Should().Be(2.3);
			fsf.Script.Should().Be("My complex script");
			fsf.Lang.Should().Be("mvel");
			fsf.Params.Should().NotBeEmpty().And.HaveCount(1);
			var param = fsf.Params.First();
			param.Key.Should().Be("param");
			param.Value.Should().Be("paramvalue");

			AssertIsTermFilter(fsf.Filter, Filter1);
		}
	}
}