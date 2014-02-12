using NUnit.Framework;
using Nest.Tests.MockData.Domain;

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
					.GeoShape(f=>f.Origin, d=>d
						.Type("envelope")
						.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							shape: {
								type: ""envelope"",
								coordinates: [[13.0, 53.0], [14.0, 52.0]]
							}
						},
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
	}
}
