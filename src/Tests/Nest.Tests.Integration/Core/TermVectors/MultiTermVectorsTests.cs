using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			var result = _client.MultiTermVectors<ElasticsearchProject>(s => s
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
		public void MultiTermVectorsNonExistentIdTest()
		{
			var result = _client.MultiTermVectors<ElasticsearchProject>(s => s
				.Ids("thisiddoesnotexist")
			);

			result.IsValid.Should().BeTrue();
			result.Documents.Count().Should().Be(1);
			result.Documents.First().Found.Should().Be(false);
		}
	}
}
