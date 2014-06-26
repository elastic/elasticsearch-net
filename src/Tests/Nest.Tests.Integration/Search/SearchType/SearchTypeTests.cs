using System.Linq;
using Elasticsearch.Net;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.SearchType
{
	[TestFixture]
	public class SearchTypeTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SearchQueryAndFetch()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s=>s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f=>f.Name)
				.SearchType(SearchTypeOptions.QueryAndFetch)
				
			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Hits.Any());
		}
		[Test]
		public void SearchQueryThenFetch()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(SearchTypeOptions.QueryThenFetch)

			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Hits.Any());
		}

		[Test]
		public void SearchDfsQueryAndFetch()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(SearchTypeOptions.DfsQueryAndFetch)

			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Hits.Any());
		}
		[Test]
		public void SearchDfsQueryThenFetch()
		{
			var queryResults = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(SearchTypeOptions.DfsQueryThenFetch)

			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Hits.Any());
		}

	}
}