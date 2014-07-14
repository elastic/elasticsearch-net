using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.TermVectors
{
	[TestFixture]
	public class TermVectorTests : IntegrationTests
	{
		[Test]
		public void TermVectorDefaultsTest()
		{
			var result = _client.TermVector<ElasticsearchProject>(s => s
				.Id("1")
				.Fields(ep => ep.Content));

			result.IsValid.Should().BeTrue();
			result.Found.Should().BeTrue();
			result.TermVectors.Count().Should().Be(1);

			var contentTermVector = result.TermVectors["content"];
			contentTermVector.FieldStatistics.Should().NotBeNull();
			contentTermVector.FieldStatistics.DocumentCount.Should().BeGreaterOrEqualTo(1);
			contentTermVector.FieldStatistics.SumOfDocumentFrequencies.Should().BeGreaterOrEqualTo(1);
			contentTermVector.FieldStatistics.SumOfTotalTermFrequencies.Should().BeGreaterOrEqualTo(1);

			contentTermVector.Terms.Count.Should().BeGreaterOrEqualTo(1);

			var firstTerm = contentTermVector.Terms.First().Value;
			firstTerm.Tokens.Should().NotBeNull();
			firstTerm.TotalTermFrequency.Should().Be(0);
			firstTerm.DocumentFrequency.Should().Be(0);
		}

		[Test]
		public void TermVectorDefaultsWithTermStatisticsTest()
		{
			var result = _client.TermVector<ElasticsearchProject>(s => s
				.Id("1")
				.Fields(ep => ep.Content)
				.TermStatistics(true));

			result.IsValid.Should().BeTrue();
			result.Found.Should().BeTrue();
			result.TermVectors.Count().Should().Be(1);

			var contentTermVector = result.TermVectors["content"];
			contentTermVector.FieldStatistics.Should().NotBeNull();
			contentTermVector.FieldStatistics.DocumentCount.Should().BeGreaterOrEqualTo(1);
			contentTermVector.FieldStatistics.SumOfDocumentFrequencies.Should().BeGreaterOrEqualTo(1);
			contentTermVector.FieldStatistics.SumOfTotalTermFrequencies.Should().BeGreaterOrEqualTo(1);

			contentTermVector.Terms.Count.Should().BeGreaterOrEqualTo(1);

			var firstTerm = contentTermVector.Terms.First().Value;
			firstTerm.Tokens.Should().NotBeNull();
			firstTerm.TotalTermFrequency.Should().BeGreaterOrEqualTo(1);
			firstTerm.DocumentFrequency.Should().BeGreaterOrEqualTo(1);
		}

		[Test]
		public void TermVectorNoFieldStatisticsTest()
		{
			var result = _client.TermVector<ElasticsearchProject>(s => s
				.Id("1")
				.Fields(ep => ep.Content)
				.FieldStatistics(false));

			result.IsValid.Should().BeTrue();
			result.Found.Should().BeTrue();
			result.TermVectors.Count().Should().Be(1);

			var contentTermVector = result.TermVectors["content"];
			contentTermVector.FieldStatistics.Should().BeNull();
		}

		[Test]
		public void TermVectorNonMappedFieldTest()
		{
			var result = _client.TermVector<ElasticsearchProject>(s => s
				.Id("1")
				.Fields(ep => ep.Name));

			result.IsValid.Should().BeTrue();
			result.TermVectors.Count().ShouldBeEquivalentTo(0);
		}

		[Test]
		[SkipVersion("1.2.0 - 1.2.2", "Fails against ES 1.2: https://github.com/elasticsearch/elasticsearch/issues/6451")]
		public void TermVectorNonExistentIdTest()
		{
			var result = _client.TermVector<ElasticsearchProject>(s => s
				.Id("thisiddoesnotexist")
				.Fields(ep => ep.Name));

			result.IsValid.Should().Be(true);
			result.Found.Should().Be(false);
			result.TermVectors.Count.Should().Be(0);
		}
	}
}
