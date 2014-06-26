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
					.Rewrite(RewriteMultiTerm.ConstantScoreBoolean)
					.Value("prefix*")
					)
				);
			q.Boost.Should().Be(2.1);
			q.Field.Should().Be("name");
			q.Rewrite.Should().Be(RewriteMultiTerm.ConstantScoreBoolean);
			q.Value.Should().Be("prefix*");
		}
	}
}