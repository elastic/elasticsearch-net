using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Exists
{
	[TestFixture]
	public class DocumentExistsTests : IntegrationTests
	{
		[Test]
		public void TestSuggest()
		{
			var tempIndex = ElasticsearchConfiguration.NewUniqueIndexName();
			var elasticsearchProject = new ElasticsearchProject
			{
				Id = 1337,
				Name = "Coboles",
				Content = "COBOL elasticsearch client"
			};
			var indexResponse = this.Client.Index(elasticsearchProject, i=>i.Refresh().Index(tempIndex));

			indexResponse.IsValid.Should().BeTrue();

			var existsResponse = this.Client.DocumentExists<ElasticsearchProject>(d => d.IdFrom(elasticsearchProject).Index(tempIndex));
			existsResponse.IsValid.Should().BeTrue();
			existsResponse.Exists.Should().BeTrue();
			existsResponse.ConnectionStatus.RequestMethod.Should().Be("HEAD");
			
			var doesNotExistsResponse = this.Client.DocumentExists<ElasticsearchProject>(d => d.IdFrom(elasticsearchProject).Index(tempIndex + "-random"));
			doesNotExistsResponse.IsValid.Should().BeTrue();
			doesNotExistsResponse.Exists.Should().BeFalse();

		}

		
	}
}
