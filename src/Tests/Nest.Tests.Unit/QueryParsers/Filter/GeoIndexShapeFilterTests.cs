using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class GeoIndexShapeFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoIndexShape_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoBaseShapeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.GeoShape,
				f=>f.GeoIndexedShape(p=>p.Origin, d=>d
					.Lookup<ElasticsearchProject>(p=>p.Origin, "1337", index: "myindex", type:"mytype")
					)
				);
			geoBaseShapeFilter.Field.Should().Be("origin");
			var geoShapeFilter = geoBaseShapeFilter as IGeoIndexedShapeFilter;
			geoShapeFilter.Should().NotBeNull();
			geoShapeFilter.IndexedShape.Should().NotBeNull();
			geoShapeFilter.IndexedShape.Field.Should().Be("origin");
			geoShapeFilter.IndexedShape.Id.Should().Be("1337");
			geoShapeFilter.IndexedShape.Index.Should().Be("myindex");
			geoShapeFilter.IndexedShape.Type.Should().Be("mytype");

		}
		
	}
}