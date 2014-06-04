using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class SpanTermQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void SpanTerm_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.SpanTerm,
				f=>f.SpanTerm(sq=>sq
					.Boost(2.3)
					.OnField(p=>p.Name)
					.Value("query")
					)
				);
			q.Boost.Should().Be(2.3);
			q.Field.Should().Be("name");
			q.Value.Should().Be("query");

		}
	}
}