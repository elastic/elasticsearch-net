using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class MatchQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Match_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f => f.Match,
				f => f.Match(m => m
					.OnField(p => p.Name)
					.Analyzer("my-analyzer")
					.Boost(2.1)
					.CutoffFrequency(1.31)
					.Fuzziness(2.3)
					.Lenient()
					.MaxExpansions(2)
					.Operator(Operator.And)
					.PrefixLength(2)
					.Query("querytext")
					.Rewrite(RewriteMultiTerm.ConstantScoreBoolean)
					.Slop(2)
					)
				);

			q.Type.Should().Be(null);
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.1);
			q.CutoffFrequency.Should().Be(1.31);
			q.Fuzziness.Should().Be(2.3);
			q.Lenient.Should().BeTrue();
			q.MaxExpansions.Should().Be(2);
			q.Field.Should().Be("name");
			q.Operator.Should().Be(Operator.And);
			q.PrefixLength.Should().Be(2);
			q.Query.Should().Be("querytext");
			q.Rewrite.Should().Be(RewriteMultiTerm.ConstantScoreBoolean);
			q.Slop.Should().Be(2);
		}

		[Test]
		public void MatchPhrasePhrefix_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f => f.Match,
				f => f.MatchPhrasePrefix(m => m
					.OnField(p => p.Name)
					.Analyzer("my-analyzer")
					.Boost(2.1)
					.CutoffFrequency(1.31)
					.Fuzziness(2.3)
					.Lenient()
					.MaxExpansions(2)
					.Operator(Operator.And)
					.PrefixLength(2)
					.Query("querytext")
					.Rewrite(RewriteMultiTerm.ConstantScoreBoolean)
					.Slop(2)
					)
				);
			q.Type.Should().Be("phrase_prefix");
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.1);
			q.CutoffFrequency.Should().Be(1.31);
			q.Fuzziness.Should().Be(2.3);
			q.Lenient.Should().BeTrue();
			q.MaxExpansions.Should().Be(2);
			q.Field.Should().Be("name");
			q.Operator.Should().Be(Operator.And);
			q.PrefixLength.Should().Be(2);
			q.Query.Should().Be("querytext");
			q.Rewrite.Should().Be(RewriteMultiTerm.ConstantScoreBoolean);
			q.Slop.Should().Be(2);
		}

		[Test]
		public void MatchPhrase_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f => f.Match,
				f => f.MatchPhrase(m => m
					.OnField(p => p.Name)
					.Analyzer("my-analyzer")
					.Boost(2.1)
					.CutoffFrequency(1.31)
					.Fuzziness(2.3)
					.Lenient()
					.MaxExpansions(2)
					.Operator(Operator.And)
					.PrefixLength(2)
					.Query("querytext")
					.Rewrite(RewriteMultiTerm.ConstantScoreBoolean)
					.Slop(2)
					)
				);
			q.Type.Should().Be("phrase");
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.1);
			q.CutoffFrequency.Should().Be(1.31);
			q.Fuzziness.Should().Be(2.3);
			q.Lenient.Should().BeTrue();
			q.MaxExpansions.Should().Be(2);
			q.Field.Should().Be("name");
			q.Operator.Should().Be(Operator.And);
			q.PrefixLength.Should().Be(2);
			q.Query.Should().Be("querytext");
			q.Rewrite.Should().Be(RewriteMultiTerm.ConstantScoreBoolean);
			q.Slop.Should().Be(2);
		}

	}
}