using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class QueryStringQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void QueryString_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.QueryString,
				f=>f.QueryString(qs=>qs
					.AllowLeadingWildcard()
					.AnalyzeWildcard()
					.Analyzer("my-analyzer")
					.AutoGeneratePhraseQueries()
					.Boost(1.1)
					.DefaultOperator(Operator.And)
					.EnablePositionIncrements()
					.FuzzyMinimumSimilarity(2.1)
					.FuzzyPrefixLength(2)
					.Lenient()
					.LowercaseExpendedTerms()
					.MinimumShouldMatchPercentage(2)
					.DefaultField(p=>p.Name)
					.OnFields(p=>p.Name, p=>p.Origin)
					.PhraseSlop(2.1)
					.Query("q")
					.Rewrite(RewriteMultiTerm.ConstantScoreDefault)
					.TieBreaker(4.1)
					.UseDisMax()
					)
				);
			q.AllowLeadingWildcard.Should().BeTrue();
			q.AnalyzeWildcard.Should().BeTrue();
			q.Analyzer.Should().Be("my-analyzer");
			q.AutoGeneratePhraseQueries.Should().BeTrue();
			q.Boost.Should().Be(1.1);
			q.DefaultOperator.Should().Be(Operator.And);
			q.EnablePositionIncrements.Should().BeTrue();
			q.FuzzyMinimumSimilarity.Should().Be(2.1);
			q.FuzzyPrefixLength.Should().Be(2);
			q.Lenient.Should().BeTrue();
			q.LowercaseExpendedTerms.Should().BeTrue();
			q.MinimumShouldMatchPercentage.Should().Be("2%");
			q.DefaultField.Should().Be("name");
			q.Fields.Should().BeEquivalentTo(new []{"name", "origin"});
			q.PhraseSlop.Should().Be(2.1);
			q.Query.Should().Be("q");
			q.Rewrite.Should().Be(RewriteMultiTerm.ConstantScoreDefault);
			q.TieBreaker.Should().Be(4.1);
			q.UseDismax.Should().BeTrue();
		}

	}
}