using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.TestData;
using ElasticSearch.Client;
using Nest.TestData.Domain;
using NUnit.Framework;

namespace ElasticSearch.Tests.FacetResponses
{
	public class BaseFacetTestFixture: BaseElasticSearchTests
	{
		protected string _LookFor = NestTestData.Data.First().Followers.First().LastName.ToLower();

		protected void TestDefaultAssertions(QueryResponse<ElasticSearchProject> queryResponse)
		{
			Assert.True(queryResponse.IsValid);
			Assert.Null(queryResponse.ConnectionError);
			Assert.True(queryResponse.Total > 0, "No hits");
			Assert.True(queryResponse.Documents.Any());
			Assert.True(queryResponse.Documents.Count() > 0);
			Assert.True(queryResponse.Shards.Total > 0);
			Assert.True(queryResponse.Shards.Successful == queryResponse.Shards.Total);
			Assert.True(queryResponse.Shards.Failed == 0);
			Assert.That(queryResponse.ElapsedMilliseconds, Is.InRange(0, 200));
		}
		protected void TestDefaultFacetCollectionAssertation(IEnumerable<Facet> facets)
		{
			Assert.NotNull(facets);
			Assert.True(facets.Count() > 0);
		}
	}
}
