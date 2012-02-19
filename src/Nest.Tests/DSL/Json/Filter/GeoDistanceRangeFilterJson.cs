using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
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
						.Location(X: 40, Y: -70)
						.Distance(From: 12, To: 200, Unit: GeoUnit.km)
						.Optimize(GeoOptimizeBBox.memory)
					)
				);

			var json = ElasticClient.Serialize(s);
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
