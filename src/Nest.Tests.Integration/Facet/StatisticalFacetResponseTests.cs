using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Facet
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
            var results = this.ConnectedClient.Search<ElasticSearchProject>(s=>s
							.MatchAll()
							.FacetStatistical(fs=>fs
								.OnField(f=>f.LOC)
							));

						var facet = results.Facet<StatisticalFacet>(f => f.LOC);
						this.TestDefaultAssertions(results);

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