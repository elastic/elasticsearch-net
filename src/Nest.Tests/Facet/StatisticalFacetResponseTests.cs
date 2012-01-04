using Nest;
using NUnit.Framework;
using Nest.TestData.Domain;

namespace Nest.Tests.FacetResponses
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
            QueryResponse<ElasticSearchProject> queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
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

            Facet facet = queryResults.Facets["loc"];
            this.TestDefaultAssertions(queryResults);

            Assert.IsInstanceOf<StatisticalFacet>(facet);

            var sf = (StatisticalFacet) facet;

            Assert.Greater(sf.Count, 0);
            Assert.Greater(sf.Total, 0);
            Assert.Greater(sf.Min, 0);
            Assert.Greater(sf.Max, 0);
            Assert.Greater(sf.Mean, 0);
            Assert.Greater(sf.SumOfSquares, 0);
            Assert.Greater(sf.Variance, 0);
            Assert.Greater(sf.StandardDeviation, 0);
        }
    }
}