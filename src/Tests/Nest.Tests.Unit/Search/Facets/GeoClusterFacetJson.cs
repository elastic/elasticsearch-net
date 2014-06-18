using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Facets
{

	[TestFixture]
	public class GeoClusterFacetJson
	{
		[Test]
		public void TestGetCluster()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.QueryRaw(@"{ raw : ""query""}")
				.FacetGeoCluster("cluster", gc => gc
					.OnField(f => f.Origin)
					.OnFactor(0.3)
				);

			var json = TestElasticClient.Serialize(s);

			var expected = @"{ from: 0, size: 10, 
			facets :  {
			""cluster"" :  {
				""geo_cluster"" : {
					""field"" : ""origin"",
					""factor"": 0.3
				} 
			}
			}, query : { raw : ""query""}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
