using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using HackerNews.Indexer.Domain;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Nest.Tests.FacetResponses
{
	[TestFixture]
	public class GeoDistanceFacetResponseTests : BaseFacetTestFixture
	{
		[Test]
		public void SimpleGeoFacet()
		{
			this.DeleteIndices();
			this.ConnectedClient.Map<ElasticSearchProject>();
			this.BulkIndexData();
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""origin"" : 
						{ 
							""geo_distance"" : {
								""origin"" : {
									""lat"" : 52,
									""lon"" : 52
								},
								""ranges"" : [
									{ ""to"" : 500 },
									{ ""from"" : 500, ""to"" : 5000 },
									{ ""from"" : 5000, ""to"" : 10000 },
									{ ""from"" : 10000 }
								]
							}
						}
					}
				}"
			);

			var facets = queryResults.FacetItems<GeoDistanceRange>(p => p.Origin);
			this.TestDefaultAssertions(queryResults);
			//this.TestDefaultFacetCollectionAssertation(facets);
			var facet = facets.First();
			Assert.Greater(facets.Count(),0);
			Assert.Greater(facet.Total, 0);
			Assert.Greater(facet.Min, 0);
			Assert.Greater(facet.Max, 0);
			Assert.Greater(facet.Mean, 0);
		
		}
	}
}
