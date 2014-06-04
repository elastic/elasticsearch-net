using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class MultiMatchQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void MultiMatch_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.MultiMatch,
				f=>f.MultiMatch(mq=>mq
					.Analyzer("my-analyzer")
					.Boost(2.5)
					.CutoffFrequency(0.8)
					.Fuzziness(0.8)
					.MaxExpansions(2)
					.OnFields(p=>p.Name,p=>p.MyGeoShape)
					.Operator(Operator.or)
					.PrefixLength(2)
					.Query("querytext")
					.Rewrite(RewriteMultiTerm.top_terms_N)
					.Slop(2)
					.TieBreaker(2.0)
					.Type(TextQueryType.BEST_FIELDS)
					)
				);
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.5);
			q.CutoffFrequency.Should().Be(0.8);
			q.Fuzziness.Should().Be(0.8);
			q.MaxExpansions.Should().Be(2);
			q.Fields.Should().BeEquivalentTo(new [] { "name", "myGeoShape"});
			q.Operator.Should().Be(Operator.or);
			q.PrefixLength.Should().Be(2);
			q.Query.Should().Be("querytext");
			q.Rewrite.Should().Be(RewriteMultiTerm.top_terms_N);
			q.Slop.Should().Be(2);
			q.TieBreaker.Should().Be(2.0);
			q.Type.Should().Be(TextQueryType.BEST_FIELDS);
		}
	}
}