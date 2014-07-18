using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class CustomScoreQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void CustomScore_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.CustomScore,
#pragma warning disable 0618
				//CustomScore is obsolete but we still need to test it.
				f=>f.CustomScore(cs=>cs
					.Query(qq=>Query1)
					.Script("My complex script")
					.Lang("mvel")
					.Params(p=>p.Add("param", "paramvalue"))
					)
				);
#pragma warning restore 0618
			q.Script.Should().Be("My complex script");
			q.Lang.Should().Be("mvel");
			q.Params.Should().NotBeEmpty().And.HaveCount(1);
			var param = q.Params.First();
			param.Key.Should().Be("param");
			param.Value.Should().Be("paramvalue");

			AssertIsTermQuery(q.Query, Query1);
		}
	}
}