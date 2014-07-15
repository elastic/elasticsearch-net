using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Collections.Generic;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class GeoShapeFilterJson
	{
		[Test]
		public void GeoShapeFilter()
		{
			//[13.0, 53.0], [14.0, 52.0]]
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.Cache(true)
					.Name("my_geo_filter")
					.GeoShapeEnvelope(f => f.Origin, d => d
						.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
				)
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								coordinates: [[13.0, 53.0], [14.0, 52.0]],
								type: ""envelope""
							}
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
		public void GeoShapeCircleFilter()
		{
			
		}
	}
}
