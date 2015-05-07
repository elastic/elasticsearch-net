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
					.MinChildren(2)
					.MaxChildren(10)
					.InnerHits()
				)
			);
			q.Type.Should().Be("person");
			q.ScoreType.Should().Be(ChildScoreType.Average);
			q.InnerHits.Should().NotBeNull();
			q.MinChildren.Should().Be(2);
			q.MaxChildren.Should().Be(10);
			AssertIsTermQuery(q.Query, Query2);
		}
	}
}