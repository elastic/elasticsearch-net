using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class GeoDistanceFilterJson
	{
		[Test]
		public void GeoDistanceFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.Cache(true)
					.Name("my_geo_filter")
					.GeoDistance(f=>f.Origin, d=>d
						.Location(Lat: 40, Lon: -70)
						.Distance(12, GeoUnit.Kilometers)
						.Optimize(GeoOptimizeBBox.memory)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_distance: {
						origin: ""40, -70"",
						distance: 12.0,
						unit: ""km"",
						optimize_bbox: ""memory"",
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
	}
}
