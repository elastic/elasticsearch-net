using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Facets
{
    [TestFixture]
    public class GeoClusterJson
    {
        [Test]
        public void TestGeoCluster()
        {
            var s = new SearchDescriptor<ElasticSearchProject>()
                .From(0)
                .Size(10)
                .GeoClusterFacet("places", x => x.OnField(m => m.Origin).Factor(0.1));

            var json = TestElasticClient.Serialize(s);
            var expected = @"{ from: 0, size: 10, 
          facets :  {
            places :  {
                ""geo_cluster"" : {
                    factor : 0.1,
                    field : ""origin""
                } 
            }
          }}";
            
         Assert.True(json.JsonEquals(expected));
        }
    }
}
