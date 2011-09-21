using System.Collections.Generic;
using System.Linq;
using ElasticSearch.Client;
using NUnit.Framework;
using Nest.TestData.Domain;

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
            QueryResponse<ElasticSearchProject> queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
                @" { ""query"" : {
						    ""match_all"" : { }
					},
					""facets"" : {
					  ""followersLastName"" : { ""terms"" : {""field"" : ""followers.lastName""} }
					}
				}"
                );

            var facet = queryResults.Facets["followersLastName"];
            this.TestDefaultAssertions(queryResults);

            Assert.IsInstanceOf<TermFacet>(facet);

            var tf = (TermFacet) facet;
            Assert.AreEqual(0, tf.Missing);
            Assert.Greater(tf.Other, 0);
            Assert.Greater(tf.Total, 0);
            Assert.Greater(tf.Terms.Count, 0);

            foreach (TermFacet.TermItem term in tf.Terms)
            {
                Assert.Greater(term.Count, 0);
                Assert.IsNotNullOrEmpty(term.Term);
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

            Assert.IsFalse(tf.Terms.Any(f => f.Term == this._LookFor));
        }

        [Test]
        public void SimpleTermFacetWithGlobal()
        {
            QueryResponse<ElasticSearchProject> queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
                @" { ""query"" : {
						    ""term"" : { ""followers.lastName"" : """ + this._LookFor +
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
            Assert.IsTrue(tf.Terms.Any(f => f.Term == this._LookFor));
        }
    }
}