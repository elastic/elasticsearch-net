using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class NestedQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Nested_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Nested,
				f=>f.Nested(nq=>nq
					.Path(p=>p.NestedFollowers)
					.Query(qq=>Query1)
					.Scope("scopey")
					.Score(NestedScore.max)
					)
				);
			q.Score.Should().Be(NestedScore.max);
			q.Scope.Should().Be("scopey");
			q.Path.Should().Be("nestedFollowers");
			AssertIsTermQuery(q.Query, Query1);
		}

	}
}