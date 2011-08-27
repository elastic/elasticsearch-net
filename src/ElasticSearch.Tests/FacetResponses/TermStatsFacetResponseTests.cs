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
	public class TermStatsFacetResponseTests : BaseFacetTestFixture
	{
		[Test]
		public void SimpleTermStatsFacet()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""loc"" : 
						{ 
							""terms_stats"" : {
								""key_field"" : ""name"",
								""value_field"" : ""loc""
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
