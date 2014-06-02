using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class FuzzyLikeThisQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void FuzzyLikeThis_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.FuzzyLikeThis,
				f=>f.FuzzyLikeThis(flq=>flq
					.Analyzer("my-analyzer")	
					.Boost(30.1)
					.IgnoreTermFrequency()
					.LikeText("likeme")
					.MaxQueryTerms(2)
					.MinimumSimilarity(0.3)
					.OnFields(p=>p.Name, p=>p.Origin)
					.PrefixLength(2)
					)
				);

			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(30.1);
			q.IgnoreTermFrequency.Should().BeTrue();
			q.LikeText.Should().Be("likeme");
			q.MaxQueryTerms.Should().Be(2);
			q.MinSimilarity.Should().Be(0.3);
			q.Fields.Should().BeEquivalentTo(new [] { "name", "origin"});
			q.PrefixLength.Should().Be(2);
		}
	}
}