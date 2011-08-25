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

namespace ElasticSearch.Tests
{
	/// <summary>
	///  Tests that test whether the query response can be successfully mapped or not
	/// </summary>
	[TestFixture]
	public class QueryFacetResponseMapperTests : BaseElasticSearchTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().LastName.ToLower();


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



		[Test]
		public void SimpleTermFacet()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@" { ""query"" : {
						    ""match_all"" : { }
					},
					""facets"" : {
					  ""followers.lastName"" : { ""terms"" : {""field"" : ""followers.lastName""} }
					}
				}"
			);
			
			var facets = queryResults.Facets<TermFacet>("followers.lastName");
			this.TestDefaultAssertions(queryResults);
			this.TestDefaultFacetCollectionAssertation(facets);
			Assert.IsTrue(facets.Any(f => f.Term == this._LookFor));	
		}
		[Test]
		public void SimpleTermFacetWithExclude()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@" { ""query"" : {
						    ""match_all"" : { }
					},
					""facets"" : {
					  ""followers.lastName"" : { ""terms"" : {
						""field"" : ""followers.lastName""
						, exclude : [""" + this._LookFor + @"""]
					  } }
					}
				}"
			);

			var facets = queryResults.Facets<TermFacet>("followers.lastName");
			this.TestDefaultAssertions(queryResults);
			this.TestDefaultFacetCollectionAssertation(facets);
			Assert.IsFalse(facets.Any(f=>f.Term == this._LookFor));
		}
	}
}
