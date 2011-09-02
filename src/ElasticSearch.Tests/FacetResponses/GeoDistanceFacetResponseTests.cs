using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using HackerNews.Indexer.Domain;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace ElasticSearch.Tests.FacetResponses
{
	/// <summary>
	///  Tests that test whether the query response can be successfully mapped or not
	/// </summary>
	[TestFixture]
	public class GeoDistanceFacetResponseTests : BaseFacetTestFixture
	{
		[Test]
		public void SimpleGeoFacet()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""loc"" : 
						{ 
							""geo_distance"" : {
								""pin.location"" : {
									""lat"" : 52,
									""lon"" : 52
								},
								""ranges"" : [
									{ ""to"" : 10 },
									{ ""from"" : 10, ""to"" : 20 },
									{ ""from"" : 20, ""to"" : 100 },
									{ ""from"" : 100 }
								]
							}
						}
					}
				}"
			);

			var facets = queryResults.Facets<TermStatsFacet>("loc");		
			this.TestDefaultAssertions(queryResults);
			this.TestDefaultFacetCollectionAssertation(facets);
			var facet = facets.First();
			Assert.Greater(facet.Count,0);
			Assert.Greater(facet.Total, 0);
			Assert.Greater(facet.Min, 0);
			Assert.Greater(facet.Max, 0);
			Assert.Greater(facet.Mean, 0);
		
		}
	}
}
