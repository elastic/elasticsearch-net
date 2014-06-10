using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class PrefixQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Prefix_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Prefix,
				f=>f.Prefix(pq=>pq
					.Boost(2.1)
					.OnField(p=>p.Name)
					.Rewrite(RewriteMultiTerm.constant_score_boolean)
					.Value("prefix*")
					)
				);
			q.Boost.Should().Be(2.1);
			q.Field.Should().Be("name");
			q.Rewrite.Should().Be(RewriteMultiTerm.constant_score_boolean);
			q.Value.Should().Be("prefix*");
		}
	}
}