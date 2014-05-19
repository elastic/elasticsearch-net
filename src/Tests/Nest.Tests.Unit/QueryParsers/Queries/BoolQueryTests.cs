using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class BoolQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Bool_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Bool,
				f=>f.Bool(b=>b
					.Must(Query1)
					.Should(Query2)
					.MustNot(Query3)
					.Boost(1.1)
					.DisableCoord()
					.MinimumShouldMatch(2)
					)
				);
			q.Boost.Should().Be(1.1);
			q.DisableCoord.Should().BeTrue();
			q.MinimumShouldMatch.Should().Be("2");
			q.Must.Should().NotBeEmpty().And.HaveCount(1);
			q.Should.Should().NotBeEmpty().And.HaveCount(1);
			q.MustNot.Should().NotBeEmpty().And.HaveCount(1);

			AssertIsTermQuery(q.Must.First(), Query1);
			AssertIsTermQuery(q.Should.First(), Query2);
			AssertIsTermQuery(q.MustNot.First(), Query3);
		}

	}
}