using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class WildCardQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void WildCard_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Wildcard,
				f=>f.Wildcard(wq=>wq
					.Boost(1.1)
					.Rewrite(RewriteMultiTerm.constant_score_boolean)
					.OnField(p=>p.Name)
					.Value("wild*")
					)
				);
			q.Boost.Should().Be(1.1);
			q.Field.Should().Be("name");
			q.Value.Should().Be("wild*");
			q.Rewrite.Should().Be(RewriteMultiTerm.constant_score_boolean);
		}
	}
}