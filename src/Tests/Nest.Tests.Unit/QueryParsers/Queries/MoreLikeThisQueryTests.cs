using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class MoreLikeThisQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void MoreLikeThis_Deserializes()
		{
			var stopWords = new [] {"no", "stopwords"};
			var q = this.SerializeThenDeserialize(
				f=>f.MoreLikeThis,
				f=>f.MoreLikeThis(mlt=>mlt
					.Analyzer("my-analyzer")
					.Boost(2.1)
					.BoostTerms(1.2)
					.LikeText("likeme")
					.MaxDocumentFrequency(2)
					.MaxQueryTerms(3)
					.MaxWordLength(10)
					.MinDocumentFrequency(2)
					.MinTermFrequency(2)
					.MinWordLength(2)
					.OnFields(p=>p.Name, p=>p.MyGeoShape)
					.StopWords(stopWords)
					.TermMatchPercentage(0.9)
					)
				);
			q.Analyzer.Should().Be("my-analyzer");
			q.Boost.Should().Be(2.1);
			q.BoostTerms.Should().Be(1.2);
			q.LikeText.Should().Be("likeme");
			q.MaxDocumentFrequency.Should().Be(2);
			q.MaxQueryTerms.Should().Be(3);
			q.MaxWordLength.Should().Be(10);
			q.MinDocumentFrequency.Should().Be(2);
			q.MinTermFrequency.Should().Be(2);
			q.MinWordLength.Should().Be(2);
			q.Fields.Should().BeEquivalentTo(new [] { "name", "myGeoShape"});
			q.StopWords.Should().BeEquivalentTo(stopWords);
			q.TermMatchPercentage.Should().Be(0.9);
		}
	}
}