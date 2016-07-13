using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
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
			var scanResults = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(1)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(SearchType.Scan)
				.Scroll("2s")
			);
			Assert.True(scanResults.IsValid);
			Assert.False(scanResults.FieldSelections.Any());
			Assert.IsNotNullOrEmpty(scanResults.ScrollId);

			var results = this.Client.Scroll<ElasticsearchProject>(s=>s
				.Scroll("4s") 
				.ScrollId(scanResults.ScrollId)
			);
			var hitCount = results.Hits.Count();
			while (results.FieldSelections.Any())
			{
				Assert.True(results.IsValid);
				Assert.True(results.FieldSelections.Any());
				Assert.IsNotNullOrEmpty(results.ScrollId);
				var localResults = results;
				results = this.Client.Scroll<ElasticsearchProject>(s=>s
					.Scroll("4s")
					.ScrollId(localResults.ScrollId));
				hitCount += results.Hits.Count();
			}
			Assert.AreEqual(scanResults.Total, hitCount);
		}

		[Test]
		public void SearchTypeScan_ObjectInitializer()
		{
			var scanResults = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(1)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(SearchType.Scan)
				.Scroll("2s")
			);
			Assert.True(scanResults.IsValid);
			Assert.False(scanResults.FieldSelections.Any());
			Assert.IsNotNullOrEmpty(scanResults.ScrollId);

			var results = this.Client.Scroll<ElasticsearchProject>(s=>s
				.Scroll("4s") 
				.ScrollId(scanResults.ScrollId)
			);
			var hitCount = results.Hits.Count();
			while (results.FieldSelections.Any())
			{
				Assert.True(results.IsValid);
				Assert.True(results.FieldSelections.Any());
				Assert.IsNotNullOrEmpty(results.ScrollId);
				var localResults = results;
				results = this.Client.Scroll<ElasticsearchProject>(new ScrollRequest(localResults.ScrollId, "4s"));
				hitCount += results.Hits.Count();
			}
			Assert.AreEqual(scanResults.Total, hitCount);
		}

		[Test]
		public void SearchTypeScanMoreThanOne()
		{
			var scanResults = this.Client.Search<ElasticsearchProject>(s => s
				.From(0)
				.Size(20)
				.MatchAll()
				.Fields(f => f.Name)
				.SearchType(SearchType.Scan)
				.Scroll("4s")
			);
			Assert.True(scanResults.IsValid);
			Assert.False(scanResults.FieldSelections.Any());
			Assert.IsNotNullOrEmpty(scanResults.ScrollId);

			var results = this.Client.Scroll<ElasticsearchProject>(s =>s
				.Scroll("4s")
				.ScrollId(scanResults.ScrollId));
			results.FieldSelections.Count().Should().Be((int)scanResults.Total);
			var hitCount = results.Hits.Count();
			while (results.FieldSelections.Any())
			{
				Assert.True(results.IsValid);
				Assert.True(results.FieldSelections.Any());
				Assert.IsNotNullOrEmpty(results.ScrollId);
				var results1 = results;
				results = this.Client.Scroll<ElasticsearchProject>(s=>s
					.Scroll("4s")
					.ScrollId(results1.ScrollId));
				hitCount += results.Hits.Count();
			}
			Assert.AreEqual(scanResults.Total, hitCount);
		}

		private readonly string _scrollId = "c2Nhbjs1Ozc3NjgyOjYwZFRpNmJwVGZPdUpRNkh2cmRBSmc7Nzc2ODU6NjBkVGk2YnBUZk91SlE2SHZyZEFKZzs3NzY4NDo2MGRUaTZicFRmT3VKUTZIdnJkQUpnOzc3NjgxOjYwZFRpNmJwVGZPdUpRNkh2cmRBSmc7Nzc2ODM6NjBkVGk2YnBUZk91SlE2SHZyZEFKZzsxO3RvdGFsX2hpdHM6Mjs=";
		[Test]
		public void DoesNotThrowOnBase64()
		{
			var results = this.Client.Scroll<ElasticsearchProject>(s =>s
				.Scroll("4s")
				.ScrollId(_scrollId));

			results.IsValid.Should().BeFalse();
			results.RequestInformation.HttpStatusCode.Should().Be(503);

		}
	}
}