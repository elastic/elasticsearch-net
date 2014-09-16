using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.TermVectors
{
	[TestFixture]
	public class MultiTermVectorsTests : IntegrationTests
	{
		[Test]
		public void MultiTermVectorsTest()
		{
			var result = Client.MultiTermVectors<ElasticsearchProject>(s => s
				.Fields(ep => ep.Content)
				.Ids("1", "2")
			);


			result.IsValid.Should().BeTrue();

			result.Documents.Should().NotBeNull();
			result.Documents.Count().Should().Be(2);

			foreach (var document in result.Documents)
			{
				document.TermVectors.Count().Should().Be(1);
				document.TermVectors.First().Key.Should().Be("content");
			}
		}
		
		[Test]
		public void MultiTermVectorsTest_DocumentsInBody()
		{
			var result = Client.MultiTermVectors<ElasticsearchProject>(s => s
				.Fields(ep => ep.Content)
				.Documents(
					m=>m.Id(1).TermStatistics(),
					m=>m.Id(2).FieldStatistics().Offsets(false)
				)
			);

			result.IsValid.Should().BeTrue();

			result.Documents.Should().NotBeNull();
			result.Documents.Count().Should().Be(2);

			foreach (var document in result.Documents)
			{
				document.TermVectors.Count().Should().Be(1);
				document.TermVectors.First().Key.Should().Be("content");
			}
		}

		[Test]
		[SkipVersion("1.2.0 - 9.9.9", "Failing since ES 1.2: https://github.com/elasticsearch/elasticsearch/issues/6451")]
		public void MultiTermVectorsNonExistentIdTest()
		{
			var result = Client.MultiTermVectors<ElasticsearchProject>(s => s
				.Ids("thisiddoesnotexist")
			);

			result.IsValid.Should().BeTrue();
			result.Documents.Count().Should().Be(1);
			result.Documents.First().Found.Should().Be(false);
		}
	}
}
