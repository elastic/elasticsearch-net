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
	public class TermFacetResponseTests : BaseFacetTestFixture
	{
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
		[Test]
		public void SimpleTermFacetWithGlobal()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@" { ""query"" : {
						    ""match_all"" : { }
					},
					""facets"" : {
					  ""followers.lastName"" : { 
						""terms"" : {""field"" : ""followers.lastName""} },
						""global"" : true
					}
				}"
			);

			var facets = queryResults.Facets<TermFacet>("followers.lastName");
			this.TestDefaultAssertions(queryResults);
			this.TestDefaultFacetCollectionAssertation(facets);
			Assert.IsTrue(facets.Any(f => f.Term == this._LookFor));
			Assert.IsTrue(facets.Any(f => f.Term == this._LookFor && f.Count > 0));
		}
	}
}
