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
	public class StatisticalFacetResponseTests : BaseFacetTestFixture
	{
		[Test]
		public void StatisticalHistogramFacet()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""loc"" : 
						{ 
							""statistical"" : 
							{
								""field"" : ""loc""
							}
						}
					}
				}"
			);
			
			var facet = queryResults.Facet<StatisticalFacet>("loc");
			this.TestDefaultAssertions(queryResults);
			Assert.Greater(facet.Count,0);
			Assert.Greater(facet.Total, 0);
			Assert.Greater(facet.Min, 0);
			Assert.Greater(facet.Max, 0);
			Assert.Greater(facet.Mean, 0);
			Assert.Greater(facet.SumOfSquares, 0);
			Assert.Greater(facet.Variance, 0);
			Assert.Greater(facet.StandardDeviation, 0);
		
		}
		[Test]
		public void StatisticalHistogramFacetPropertyAccess()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""loc"" : 
						{ 
							""statistical"" : 
							{
								""field"" : ""loc""
							}
						}
					}
				}"
			);

			var facet = queryResults.Facet<StatisticalFacet>(p=>p.LOC);
			this.TestDefaultAssertions(queryResults);
			Assert.Greater(facet.Count, 0);
			Assert.Greater(facet.Total, 0);
			Assert.Greater(facet.Min, 0);
			Assert.Greater(facet.Max, 0);
			Assert.Greater(facet.Mean, 0);
			Assert.Greater(facet.SumOfSquares, 0);
			Assert.Greater(facet.Variance, 0);
			Assert.Greater(facet.StandardDeviation, 0);

		}
	}
}
