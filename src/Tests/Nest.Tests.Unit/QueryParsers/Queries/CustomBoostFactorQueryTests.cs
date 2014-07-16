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
#pragma warning disable 0618
				//custom_boost_factor has been obsoleted but we still need to test
				f=>f.CustomBoostFactor(cbf=>cbf
					.BoostFactor(2.1)
					.Query(cbfq=>Query2)
					)
				);
#pragma warning restore 0618
			q.BoostFactor.Should().Be(2.1);
			AssertIsTermQuery(q.Query, Query2);
		}
	}
}