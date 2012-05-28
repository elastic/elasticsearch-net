using System.Collections.Generic;
using System.Linq;
using Nest;
using NUnit.Framework;
using Nest.TestData.Domain;

namespace Nest.Tests.FacetResponses
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
			QueryResponse<ElasticSearchProject> queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@" { ""query"" : {
							""match_all"" : { }
					},
					""facets"" : {
					  ""followers.lastName"" : { ""terms"" : {""field"" : ""followers.lastName""} }
					}
				}"
				);

			var facet = queryResults.Facets["followers.lastName"];
			this.TestDefaultAssertions(queryResults);
			Assert.IsInstanceOf<TermFacet>(facet);
			var tf = (TermFacet) facet;
			Assert.AreEqual(0, tf.Missing);
			Assert.Greater(tf.Other, 0);
			Assert.Greater(tf.Total, 0);
			Assert.Greater(tf.Items.Count(), 0);

			foreach (TermItem term in tf.Items)
			{
				Assert.Greater(term.Count, 0);
				Assert.IsNotNullOrEmpty(term.Term);
			}

			tf = queryResults.Facet<TermFacet>(p=>p.Followers.Select(f=>f.LastName));
			Assert.AreEqual(0, tf.Missing);
			Assert.Greater(tf.Other, 0);
			Assert.Greater(tf.Total, 0);
			Assert.Greater(tf.Items.Count(), 0);

			var items = queryResults.FacetItems<TermItem>(p=>p.Followers.Select(f=>f.LastName));
			foreach (var i in items)
			{
				Assert.Greater(i.Count, 0);
				Assert.IsNotNullOrEmpty(i.Term);
			}
		}

		[Test]
		public void SimpleTermFacetWithExclude()
		{
			QueryResponse<ElasticSearchProject> queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@" { ""query"" : {
							""match_all"" : { }
					},
					""facets"" : {
					  ""followerLastName"" : { ""terms"" : {
						""field"" : ""followers.lastName""
						, exclude : [""" +
				this._LookFor + @"""]
					  } }
					}
				}"
				);

			var facet = queryResults.Facets["followerLastName"];
			this.TestDefaultAssertions(queryResults);

			Assert.IsInstanceOf<TermFacet>(facet);

			var tf = (TermFacet) facet;

			Assert.IsFalse(tf.Items.Any(f => f.Term == this._LookFor));
		}

		[Test]
		public void SimpleTermFacetWithGlobal()
		{
	  this.ResetIndexes();
			QueryResponse<ElasticSearchProject> queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
				@" { ""query"" : {
							""term"" : { ""followers.lastName"" : """ + this._LookFor.ToLower() +
				@""" }
					},
					""facets"" : {
					  ""followerLastName"" : { 
						""terms"" : {""field"" : ""followers.lastName""} },
						""global"" : true
					}
				}");

			var facet = queryResults.Facets["followerLastName"];
			this.TestDefaultAssertions(queryResults);

			Assert.IsInstanceOf<TermFacet>(facet);

			var tf = (TermFacet) facet;
			Assert.IsTrue(tf.Items.Any(f => f.Term == this._LookFor.ToLower()));
		}
	}
}