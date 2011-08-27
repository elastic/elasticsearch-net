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
	public class RangeFacetResponseTests : BaseFacetTestFixture
	{
		[Test]
		public void SimpleRangeFacet()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""loc"" : 
						{ 
							""range"" : 
							{
								""field"" : ""loc"",
								""ranges"" : [
									{ ""to"" : 10000 },
									{ ""from"" : 10000, ""to"" : 15000 },
									{ ""from"" : 15000, ""to"" : 20000 },
									{ ""from"" : 20000 }
								]
							}
						}
					}
				}"
			);
			
			var facets = queryResults.Facets<RangeFacet>("loc");
			this.TestDefaultAssertions(queryResults);
			this.TestDefaultFacetCollectionAssertation(facets);

			Assert.AreEqual(facets.Count(), 4);
			Assert.AreEqual(facets.Sum(f=>f.Count), queryResults.Total);
		}
		[Test]
		public void SplitKeyValueRangeFacet()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""loc"" : 
						{ 
							""range"" : 
							{
								""key_field"" : ""loc"",
								""value_field"" : ""loc"",
								""ranges"" : [
									{ ""to"" : 10000 },
									{ ""from"" : 10000, ""to"" : 15000 },
									{ ""from"" : 15000, ""to"" : 20000 },
									{ ""from"" : 20000 }
								]
							}
						}
					}
				}"
			);

			var facets = queryResults.Facets<RangeFacet>("loc");
			this.TestDefaultAssertions(queryResults);
			this.TestDefaultFacetCollectionAssertation(facets);

			Assert.AreEqual(facets.Count(), 4);
			Assert.AreEqual(facets.Sum(f => f.Count), queryResults.Total);
		}

		[Test]
		public void DateRangeFacets()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""followers.dateOfBirth"" : 
						{ 
							""range"" : 
							{
								""field"" : ""followers.dateOfBirth"",
								""ranges"" : [
									{ ""to"" : ""1900-01-01"" },
									{ ""from"" : ""1900-01-01"", ""to"" : ""1950-01-01"" },
									{ ""from"" : ""1950-01-01"", ""to"" : ""1980-01-01"" },
									{ ""from"" : ""1980-01-01"", ""to"" : ""1990-01-01"" },
									{ ""from"" : ""1990-01-01""}
								]
							}
						}
					}
				}"
			);

			var facets = queryResults.Facets<DateRangeFacet>("followers.dateOfBirth");
			this.TestDefaultAssertions(queryResults);
			this.TestDefaultFacetCollectionAssertation(facets);

			Assert.AreEqual(facets.Count(), 5);
		}
	}
}
