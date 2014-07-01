using System.Linq;
using Elasticsearch.Net;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.SearchTypeTests
{
	[TestFixture]
	public class SearchTypeCountTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SearchTypeCount()
		{
			var queryResults = this._client.Search<ElasticsearchProject>(s=>s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f=>f.Name)
				.SearchType(SearchType.Count)
			);
			Assert.True(queryResults.IsValid);
			Assert.False(queryResults.Documents.Any());
			Assert.Greater(queryResults.Total, 0);
		}
	}
}