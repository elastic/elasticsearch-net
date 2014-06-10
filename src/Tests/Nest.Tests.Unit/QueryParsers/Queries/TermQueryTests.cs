using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class TermQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Term_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f => f.Term,
				f => f.Term(p => p.Name, "hello world")
				);
			q.Field.Should().Be("name");
			q.Value.Should().Be("hello world");
		}
	}
}