using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class GeoDistanceRangeFilterJson
	{
		[Test]
		public void GeoDistanceRangeFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.Cache(true)
					.Name("my_geo_filter")
					.GeoDistanceRange(f=>f.Origin, d=>d
						.Location(Lat: 40, Lon: -70)
						.Distance(From: 12, To: 200, Unit: GeoUnit.km)
						.Optimize(GeoOptimizeBBox.memory)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_distance_range: {
						from: 12.0,
						to: 200.0,
						distance_type: ""km"",
						optimize_bbox: ""memory"",
						origin: ""40, -70"",
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
	}
}
