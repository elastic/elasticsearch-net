using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class GeoBoundingBoxFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoBoundingBox_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoBoundingBox = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.GeoBoundingBox,
				f=>f.GeoBoundingBox(p=>p.Origin, 0.1, 0.2, 0.3, 0.4, GeoExecution.Memory)
				);
			
			geoBoundingBox.Field.Should().Be("origin");
			geoBoundingBox.GeoExecution.Should().Be(GeoExecution.Memory);
			geoBoundingBox.TopLeft.Should().Be("0.2, 0.1");
			geoBoundingBox.BottomRight.Should().Be("0.4, 0.3");
		}

		[Test]
		public void GeoBoundingBox_Array_Deserializes()
		{
			//nest will always generate top_left: ""; but while parsing it will need to handle top_left: [] as well
			var filter = this.ParseSearchDescriptorFromFile(f => f.GeoBoundingBox, MethodBase.GetCurrentMethod(), @"GeoBoundingBox\Array");
			filter.Field.Should().Be("origin");
			filter.GeoExecution.Should().Be(GeoExecution.Memory);
			filter.TopLeft.Should().Be("0.2, 0.1");
			filter.BottomRight.Should().Be("0.4, 0.3");
		}
		
		[Test]
		public void GeoBoundingBox_Vertices_Serializes()
		{
			//nest will always generate top_left: ""; but while parsing it will need to handle top: left: right: bottom: as well
			var filter = this.ParseSearchDescriptorFromFile(f => f.GeoBoundingBox, MethodBase.GetCurrentMethod(), @"GeoBoundingBox\Vertices");
			filter.Field.Should().Be("origin");
			filter.GeoExecution.Should().Be(GeoExecution.Memory);
			filter.TopLeft.Should().Be("-74.1, 40.73");
			filter.BottomRight.Should().Be("-71.12, 40.01");
		}

		[Test]
		public void GeoBoundingBox_LatLon_Serializes()
		{
			//nest will always generate top_left: ""; but while parsing it will need to handle top: left: right: bottom: as well
			var filter = this.ParseSearchDescriptorFromFile(f => f.GeoBoundingBox, MethodBase.GetCurrentMethod(), @"GeoBoundingBox\LatLon");
			filter.Field.Should().Be("origin");
			filter.GeoExecution.Should().Be(GeoExecution.Memory);
			filter.TopLeft.Should().Be("-74.1, 40.73");
			filter.BottomRight.Should().Be("-71.12, 40.01");
		}
	}
}