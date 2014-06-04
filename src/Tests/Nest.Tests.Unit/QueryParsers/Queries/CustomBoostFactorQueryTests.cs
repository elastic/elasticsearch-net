using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class CustomBoostFactorQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void CustomBoostFactor_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.CustomBoostFactor,
				f=>f.CustomBoostFactor(cbf=>cbf
					.BoostFactor(2.1)
					.Query(cbfq=>Query2)
					)
				);
			q.BoostFactor.Should().Be(2.1);
			AssertIsTermQuery(q.Query, Query2);
		}
	}
}