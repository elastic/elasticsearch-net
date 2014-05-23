using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class MatchAllQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void MatchAll_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.MatchAllQuery,
				f=>f.MatchAll(1.0, "normField")
				);
			q.Boost.Should().Be(1.0);
			q.NormField.Should().Be("normField");
		}
	}
}