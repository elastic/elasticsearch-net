using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class HasChildQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void HasChild_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.HasChild,
				f=>f.HasChild<Person>(hq=>hq
					.Query(qq=>Query2)
					.Score(ChildScoreType.Average)
					)
				);
			q.Type.Should().Be("person");
			q.ScoreType.Should().Be(ChildScoreType.Average);
			AssertIsTermQuery(q.Query, Query2);
		}
	}
}