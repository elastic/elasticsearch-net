using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class GeoIndexedShapeFilterJson
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
					.GeoIndexedShape(f=>f.Origin, d=>d
						.Lookup<ElasticsearchProject>(p=>p.MyGeoShape, "1")
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_shape: {
						origin: {
							indexed_shape: {
								id: ""1"",
								type: ""elasticsearchprojects"",
								index: ""nest_test_data"",
								shape_field_name: ""myGeoShape""
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
