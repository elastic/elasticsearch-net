using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class IDocumentTests : IntegrationTests
	{
		[Test]
		public void Search()
		{
			var searchResults = this.Client.Search<IDocument>(c=>c
				.Index(ElasticsearchConfiguration.DefaultIndexPrefix + "*")
				.AllTypes()
			);
			searchResults.Total.Should().BeGreaterThan(0);
			var project = searchResults.Documents.First().OfType<ElasticsearchProject>();
			project.Should().NotBeNull();
			project.Name.Should().NotBeNullOrEmpty();
		}

	}
}