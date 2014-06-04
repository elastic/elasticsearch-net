using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class DisMaxQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void DisMax_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.DisMax,
				f=>f.Dismax(d=>d
					.Boost(1.5)
					.Queries(qq=>Query1, qq=>Query2)
					.TieBreaker(1.1)
					)
				);
			q.Boost.Should().Be(1.5);
			q.TieBreaker.Should().Be(1.1);
			q.Queries.Should().NotBeEmpty().And.HaveCount(2);
			AssertIsTermQuery(q.Queries.First(), Query1);
			AssertIsTermQuery(q.Queries.Last(), Query2);
		}
	}
}