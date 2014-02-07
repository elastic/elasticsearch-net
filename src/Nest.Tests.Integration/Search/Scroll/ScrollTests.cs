using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace Nest.Tests.Integration.Search.Scroll
{
	[TestFixture]
	public class ScrollTests : IntegrationTests
	{
		[Test]
		public void SearchTypeScan()
		{
			var scanResults = this._client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(1)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(SearchTypeOptions.Scan)
				.Scroll("2s")
			);
			Assert.True(scanResults.IsValid);
			Assert.False(scanResults.Documents.Any());
			Assert.IsNotNullOrEmpty(scanResults.ScrollId);

			var scrolls = 0;
			var results = this._client.Scroll<ElasticsearchProject>(s=>s
				.Scroll("4s") 
				.ScrollId(scanResults.ScrollId)
			);
			while (results.Documents.Any())
			{
				Assert.True(results.IsValid);
				Assert.True(results.Documents.Any());
				Assert.IsNotNullOrEmpty(results.ScrollId);
				var localResults = results;
				results = this._client.Scroll<ElasticsearchProject>(s=>s
					.Scroll("4s")
					.ScrollId(localResults.ScrollId));
				scrolls++;
			}
			Assert.AreEqual(18, scrolls);

		}
		[Test]
		public void SearchTypeScanMoreThanOne()
		{
			var scanResults = this._client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(20)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(SearchTypeOptions.Scan)
				.Scroll("4s")
			);
			Assert.True(scanResults.IsValid);
			Assert.False(scanResults.Documents.Any());
			Assert.IsNotNullOrEmpty(scanResults.ScrollId);

			var scrolls = 0;
			var results = this._client.Scroll<ElasticsearchProject>(s =>s
				.Scroll("4s")
				.ScrollId(scanResults.ScrollId));
			results.Documents.Count().Should().Be(18);
			while (results.Documents.Any())
			{
				Assert.True(results.IsValid);
				Assert.True(results.Documents.Any());
				Assert.IsNotNullOrEmpty(results.ScrollId);
				var results1 = results;
				results = this._client.Scroll<ElasticsearchProject>(s=>s
					.Scroll("4s")
					.ScrollId(results1.ScrollId));
				scrolls++;
			}
			Assert.AreEqual(1, scrolls);
		}
	}
}