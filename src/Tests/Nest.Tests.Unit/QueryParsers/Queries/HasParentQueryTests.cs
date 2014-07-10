using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class HasParentQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void HasParent_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.HasParent,
				f=>f.HasParent<Person>(hp=>hp
					.Query(qq=>Query3)
					.Score(ParentScoreType.Score)
					)
				);

			q.Type.Should().Be("person");
			q.ScoreType.Should().Be(ParentScoreType.Score);
			AssertIsTermQuery(q.Query, Query3);
		}
	}
}