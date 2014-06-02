using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class SpanFirstQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void SpanFirst_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.SpanFirst,
				f=>f.SpanFirst(sf=>sf
					.MatchTerm(p => p.Name, "elasticsearch.pm", 1.1)
					.End(3)
					)
				);
			q.End.Should().Be(3);
			q.Match.Should().NotBeNull();
		}
	}
}