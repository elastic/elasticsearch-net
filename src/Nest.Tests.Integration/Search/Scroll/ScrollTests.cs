using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.Scroll
{
	[TestFixture]
	public class ScrollTests : IntegrationTests
	{
		[Test]
		public void SearchTypeScan()
		{
			var scanResults = this._client.Search<ElasticSearchProject>(s => s
				.From(0)
				.Size(1)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(Nest.SearchType.Scan)
				.Scroll("2s")
			);
			Assert.True(scanResults.IsValid);
			Assert.False(scanResults.Documents.Any());
			Assert.IsNotNullOrEmpty(scanResults.ScrollId);

			var scrolls = 1;
			var results = this._client.Scroll<ElasticSearchProject>("4s", scanResults.ScrollId);
			while (results.Documents.Any())
			{ 
				Assert.True(results.IsValid);
				Assert.True(results.Documents.Any());
				Assert.IsNotNullOrEmpty(results.ScrollId);
				results = this._client.Scroll<ElasticSearchProject>("4s", results.ScrollId);
				scrolls++;
			}
			Assert.AreEqual(19, scrolls);

		}
	}
}