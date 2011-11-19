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

		    var facet = queryResults.Facets["loc"];
			this.TestDefaultAssertions(queryResults);
			
            Assert.IsInstanceOf<HistogramFacet>(facet);

		    var hf = (HistogramFacet) facet;

            Assert.Greater(hf.Items.Count(), 0);

		    foreach (var entry in hf.Items)
		    {
		        Assert.Greater(entry.Count, 0);
                Assert.Greater(entry.Key, 0);
		    }
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
						""dob"" : 
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

            var facet = queryResults.Facets["dob"];
            this.TestDefaultAssertions(queryResults);

            Assert.IsInstanceOf<DateHistogramFacet>(facet);

            var dhf = (DateHistogramFacet)facet;

            Assert.Greater(dhf.Items.Count(), 0);

            foreach (var entry in dhf.Items)
            {
                Assert.Greater(entry.Count, 0);
                Assert.Greater(entry.Time, DateTime.MinValue);
            }
		}
	}
}
