using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class RegexpQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Regexp_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Regexp,
				f=>f.Regexp(r=>r
					.Boost(1.0)
					.Flags("FLAGS")
					.OnField(p=>p.Name)
					.Value("asdasd")
					)
				);

			q.Boost.Should().Be(1.0);
			q.Flags.Should().Be("FLAGS");
			q.Field.Should().Be("name");
			q.Value.Should().Be("asdasd");
		}
	}
}