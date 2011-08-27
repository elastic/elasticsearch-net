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
	public class HistogramFacetResponseTests : BaseFacetTestFixture
	{
		[Test]
		public void SimpleHistogramFacet()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""loc"" : 
						{ 
							""histogram"" : 
							{
								""field"" : ""loc"",
								""interval"" : 1000
							}
						}
					}
				}"
			);
			
			var facets = queryResults.Facets<HistogramFacet>("loc");
			this.TestDefaultAssertions(queryResults);
			this.TestDefaultFacetCollectionAssertation(facets);

			Assert.AreEqual(facets.Sum(f=>f.Count), queryResults.Total);
		}
		[Test]
		public void DateHistogramFacet()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""followers.dateOfBirth"" : 
						{ 
							""date_histogram"" : 
							{
								""field"" : ""followers.dateOfBirth"",
								""interval"" : ""year""
							}
						}
					}
				}"
			);

			var facets = queryResults.Facets<DateHistogramFacet>("followers.dateOfBirth");
			this.TestDefaultAssertions(queryResults);
			this.TestDefaultFacetCollectionAssertation(facets);

		}
	}
}
