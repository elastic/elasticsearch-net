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
					.Scope("my_scope")
					.Score(ParentScoreType.Score)
					)
				);

			q.Type.Should().Be("person");
			q.Scope.Should().Be("my_scope");
			q.ScoreType.Should().Be(ParentScoreType.Score);
			AssertIsTermQuery(q.Query, Query3);
		}
	}
}