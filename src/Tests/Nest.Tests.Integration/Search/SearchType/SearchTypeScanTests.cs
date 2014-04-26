using System.Linq;
using Elasticsearch.Net;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.SearchType
{
	[TestFixture]
	public class SearchTypeScanTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SearchTypeScanWithoutScrollIsInvalid()
		{
			var queryResults = this._client.Search<ElasticsearchProject>(s=>s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f=>f.Name)
				.SearchType(SearchTypeOptions.Scan)
			);
			Assert.False(queryResults.IsValid);
			StringAssert.Contains("Scroll must be provided when scanning", queryResults.ConnectionStatus.ResponseRaw.Utf8String());
		}
		[Test]
		public void SearchTypeScan()
		{
			var queryResults = this._client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(SearchTypeOptions.Scan)
				.Scroll("2s")
			);
			Assert.True(queryResults.IsValid);
			Assert.False(queryResults.Documents.Any());
			Assert.IsNotNullOrEmpty(queryResults.ScrollId);

		}
		[Test]
		public void SearchScrollOnly()
		{
			var queryResults = this._client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(10)
				.MatchAll()
				.Fields(f => f.Name)
				.Scroll("2s")
			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Hits.Any());
			Assert.IsNotNullOrEmpty(queryResults.ScrollId);
		}
	}
}