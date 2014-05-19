using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class BoostingQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Boosting_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Boosting,
				f=>f.Boosting(bq => bq
					.Positive(pq=>Query1)
					.Negative(nq=>Query2)
					.NegativeBoost(1.1)
					)
				);
			q.NegativeBoost.Should().Be(1.1);

			AssertIsTermQuery(q.NegativeQuery, Query2);
			AssertIsTermQuery(q.PositiveQuery, Query1);
		}
	}
}